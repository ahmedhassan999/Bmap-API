
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Stack.Core;
using Stack.Core.Managers;
using Stack.DTOs;
using Stack.DTOs.Enums;
using Stack.Entities.Models;
using Stack.ServiceLayer;
using Stack.DTOs.Models;
using Stack.Repository.Common;
using AutoMapper;
using Stack.DTOs.Requests;
using Stack.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Hangfire;

namespace Stack.ServiceLayer
{

    public class NotifiedOfferService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public NotifiedOfferService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        //Create push notification offer (schedules/informative)
        public async Task<ApiResponse<List<NotifiedOfferDTO>>> GetAllNotifiedOffer()
        {
            ApiResponse<List<NotifiedOfferDTO>> result = new ApiResponse<List<NotifiedOfferDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.NotifiedOfferManager.GetAsync();

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<NotifiedOfferDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }


        public async Task<ApiResponse<NotifiedOfferDTO>> GetNotifiedOffer(long id)
        {
            ApiResponse<NotifiedOfferDTO> result = new ApiResponse<NotifiedOfferDTO>();
            try
            {
                var serviceRequestsResult = await unitOfWork.NotifiedOfferManager.GetByIdAsync(id);

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<NotifiedOfferDTO>(serviceRequestsResult);
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }


        public async Task<ApiResponse<List<NotifiedOfferDTO>>> GetAllNotifiedOfferForUser(DateModel model)
        {
            ApiResponse<List<NotifiedOfferDTO>> result = new ApiResponse<List<NotifiedOfferDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.NotifiedOfferManager.GetAsync(a => (a.EndDate >= model.Date && !a.IsInformative) || a.IsInformative);

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<NotifiedOfferDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<bool>> CreateNotifiedOffer(CreateNotifiedOfferModel request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                NotifiedOffer NotifiedOfferToCreate = new NotifiedOffer();

                if (!request.IsInformative)
                {
                    NotifiedOfferToCreate.EndDate = request.EndDate;
                    NotifiedOfferToCreate.StartDate = request.StartDate;
                }
                NotifiedOfferToCreate.DescriptionAR = request.DescriptionAR;
                NotifiedOfferToCreate.DescriptionEN = request.DescriptionEN;
                NotifiedOfferToCreate.TitleAR = request.TitleAR;
                NotifiedOfferToCreate.TitleEN = request.TitleEN;
                NotifiedOfferToCreate.Image = request.Image;
                NotifiedOfferToCreate.IsInformative = request.IsInformative;


                var createPushOfferResult = await unitOfWork.NotifiedOfferManager.CreateAsync(NotifiedOfferToCreate);

                await unitOfWork.SaveChangesAsync();

                if (createPushOfferResult != null)
                {
                    //Schedule hangfire job for non informative type notification
                    if (!createPushOfferResult.IsInformative)
                    {
                        TimeSpan timespanDifference;
                        var currentTime = await HelperFunctions.GetEgyptsCurrentLocalTime();
                        timespanDifference = createPushOfferResult.StartDate - currentTime;

                        var jobId = BackgroundJob.Schedule(() => SendPushNotificationAsync(createPushOfferResult.TitleEN, createPushOfferResult.DescriptionEN, createPushOfferResult.ID), timespanDifference);
                        createPushOfferResult.JobID = jobId;
                        var updateRes = await unitOfWork.NotifiedOfferManager.UpdateAsync(createPushOfferResult);
                        if (updateRes)
                        {
                            await unitOfWork.SaveChangesAsync();
                        }
                        else
                        {
                            //Delete offer and scheduled job
                            await unitOfWork.NotifiedOfferManager.RemoveAsync(createPushOfferResult);
                            BackgroundJob.Delete(jobId);
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = false;
                            result.Errors.Add("Unable to schedule offer");
                            return result;
                        }
                    }
                    else
                    {
                        BackgroundJob.Schedule(() => SendPushNotificationAsync(createPushOfferResult.TitleEN, createPushOfferResult.DescriptionEN, createPushOfferResult.ID), TimeSpan.FromMinutes(1));

                    }
                    result.Data = true;
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Error createing contact request !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<NotifiedOfferDTO>> EditNotifiedOffer(EditNotifiedOfferModel model)
        {
            ApiResponse<NotifiedOfferDTO> result = new ApiResponse<NotifiedOfferDTO>();
            try
            {

                var offerToEdit = await unitOfWork.NotifiedOfferManager.GetByIdAsync(model.ID);

                if (offerToEdit != null)
                {
                    if (!offerToEdit.IsInformative && offerToEdit.JobID != null)
                    {
                        var jobDeletionRes = BackgroundJob.Delete(offerToEdit.JobID);
                        if (!jobDeletionRes)
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to edit offer");
                            result.Errors.Add("تعذر تعديل العرض");
                            return result;
                        }

                        TimeSpan timespanDifference;
                        var currentTime = await HelperFunctions.GetEgyptsCurrentLocalTime();
                        timespanDifference = model.StartDate - currentTime;

                       var jobId = BackgroundJob.Schedule(() => SendPushNotificationAsync(model.TitleEN, model.DescriptionEN, model.ID), timespanDifference); //A.Osama
                        offerToEdit.JobID = jobId;
                   
                       
                    }
                    offerToEdit.StartDate = model.StartDate;
                    offerToEdit.EndDate = model.EndDate;
                    offerToEdit.DescriptionAR = model.DescriptionAR;
                    offerToEdit.DescriptionEN = model.DescriptionEN;
                    offerToEdit.TitleEN = model.TitleEN;
                    offerToEdit.TitleAR = model.TitleAR;

                    var updateOfferResult = await unitOfWork.NotifiedOfferManager.UpdateAsync(offerToEdit);


                    await unitOfWork.SaveChangesAsync();

                    if (updateOfferResult == true)
                    {
                        result.Succeeded = true;
                        result.Data = mapper.Map<NotifiedOfferDTO>(offerToEdit);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to edit offer , Please try again !");
                        result.Errors.Add("فشل تعديل العرض ، يرجى المحاولة مرة أخرى!");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to edit offer , Please try again !");
                    result.Errors.Add("فشل تعديل العرض ، يرجى المحاولة مرة أخرى!");
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<bool>> DeleteNotifiedOffer(DeleteObjectDTO request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var contactRequestResult = await unitOfWork.NotifiedOfferManager.GetByIdAsync(request.ID);

                if (contactRequestResult != null)
                {
                    if (!contactRequestResult.IsInformative && contactRequestResult.JobID != null)
                    {
                        var jobDeletionRes = BackgroundJob.Delete(contactRequestResult.JobID);
                        if (!jobDeletionRes)
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Unable to remove offer");
                            return result;
                        }
                    }

                    var removeContactRequestResult = await unitOfWork.NotifiedOfferManager.RemoveAsync(contactRequestResult);

                    await unitOfWork.SaveChangesAsync();

                    if (removeContactRequestResult == true)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error deleting Notified Offer  !");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unable to find a contact request with the specified ID !");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        //Offer requests
        public async Task<ApiResponse<List<NotifiedOfferRequestDTO>>> GetAllNotifiedOfferRequests(long NotifiedOfferID)
        {
            ApiResponse<List<NotifiedOfferRequestDTO>> result = new ApiResponse<List<NotifiedOfferRequestDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.NotifiedOfferRequestManager.GetAsync(a => a.NotifiedOfferId == NotifiedOfferID);

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<NotifiedOfferRequestDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch Notified Offer request, please try again !");
                    result.Errors.Add("Failed to fetch Notified Offer, please try again !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<bool>> CreateNotifiedOfferRequest(CreateNotifiedOfferRequestModel request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                NotifiedOfferRequest NotifiedOfferRequestToCreate = new NotifiedOfferRequest();


                NotifiedOfferRequestToCreate.FirstName = request.FirstName;
                NotifiedOfferRequestToCreate.LastName = request.LastName;
                NotifiedOfferRequestToCreate.Email = request.Email;
                NotifiedOfferRequestToCreate.RequestDate = request.RequestDate;
                NotifiedOfferRequestToCreate.PhoneNumber = request.PhoneNumber;
                NotifiedOfferRequestToCreate.NotifiedOfferId = request.NotifiedOfferId;



                var createContactRequestResult = await unitOfWork.NotifiedOfferRequestManager.CreateAsync(NotifiedOfferRequestToCreate);

                await unitOfWork.SaveChangesAsync();

                if (createContactRequestResult != null)
                {
                    result.Data = true;
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Error createing notified offer request !");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<bool>> DeleteNotifiedOfferRequest(DeleteObjectDTO request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var contactRequestResult = await unitOfWork.NotifiedOfferRequestManager.GetByIdAsync(request.ID);

                if (contactRequestResult != null)
                {
                    var removeContactRequestResult = await unitOfWork.NotifiedOfferRequestManager.RemoveAsync(contactRequestResult);

                    await unitOfWork.SaveChangesAsync();

                    if (removeContactRequestResult == true)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error deleting Notified Offer Request  !");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Unable to find a contact request with the specified ID !");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<ApiResponse<bool>> EditOfferImage(EditImageModel EditImageModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var notifiedOffer = await unitOfWork.NotifiedOfferManager.GetByIdAsync(long.Parse(EditImageModel.ID));

                if (notifiedOffer != null)
                {

                    notifiedOffer.Image = EditImageModel.Image;

                    var updateResult = await unitOfWork.NotifiedOfferManager.UpdateAsync(notifiedOffer);

                    if (updateResult)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating offer image");
                        result.Errors.Add("خطأ في تحديث تفاصيل الخدمة");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Service type not found");
                    result.Errors.Add("لم يتم العثور على الخدمة");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        //Firebase cloud messaging related methods

        //Client side token registration/check
        public async Task<ApiResponse<bool>> RegisterDeviceToken(string Token)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var tokenResQ = await unitOfWork.NotificationTokenManager.GetAsync(t => t.Token == Token);
                var token = tokenResQ.FirstOrDefault();
                if (token == null)
                {
                    NotificationToken notificationToken = new NotificationToken
                    {
                        Token = Token,
                        Related_Device = null
                    };

                    var creationRes = await unitOfWork.NotificationTokenManager.CreateAsync(notificationToken);
                    if (creationRes != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Unable to store token");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Token already stored");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        //Send push notification to all existing devices
        public async Task<ApiResponse<bool>> SendPushNotificationAsync(string title, string description, long offerID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                //Fetch tokens
                var deviceTokensQ = await unitOfWork.NotificationTokenManager.GetAsync();
                var deviceTokens = deviceTokensQ.ToList();
                if (deviceTokens != null && deviceTokens.Count > 0)
                {

                    foreach (var token in deviceTokens)
                    {
                        var message = new Message()
                        {
                            Android = new AndroidConfig
                            {
                                Notification = new AndroidNotification
                                {
                                    Title = title,
                                    Body = description,
                                    ClickAction = "FCM_PLUGIN_ACTIVITY"
                                },
                            },
                            Notification = new Notification
                            {
                                Title = title,
                                Body = description,
                            },
                            Data = new Dictionary<string, string>()
                            {
                                ["OfferID"] = offerID.ToString()
                            },
                            Token = token.Token,
                        };

                        var messaging = FirebaseMessaging.DefaultInstance;
                        var notificationResult = await messaging.SendAsync(message);
                    }

                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("No active devices found");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }
    }
}

public class FCMNotification
{
    public virtual string Title { get; set; }
    public virtual string Body { get; set; }
    public virtual string Click_action { get; set; }
}

//Firebase code archive

//WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
//tRequest.Method = "post";
//tRequest.ContentType = "application/json";
//var objNotification = new
//{
//    to = "Token the device you want to push notification to",
//    data = new
//    {
//        title = "title",
//        body = "body",
//        icon = "/firebase-logo.png"
//    }
//};

//string jsonNotificationFormat = JsonConvert.SerializeObject(objNotification);
//Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
//tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAL8gQsTo:APA91bF7E4PK2ga_wOkxu4sqUSILOiGBb080JCsjz5pdDV-6ECFYib8Vmeuk1JjOOgG4WDmn6ZMsWWWGFfVbH40ntw4Lny_GvM2EU2Wzd91RYQDSpgPdc6ye5sJ4arkqJXEH5_8CKRmR"));
//tRequest.Headers.Add(string.Format("Sender: id={0}", "205220000058"));
//tRequest.ContentLength = byteArray.Length;
//tRequest.ContentType = "application/json";
//using (Stream dataStream = tRequest.GetRequestStream())
//{
//    dataStream.Write(byteArray, 0, byteArray.Length);

//    using (WebResponse tResponse = tRequest.GetResponse())
//    {
//        using (Stream dataStreamResponse = tResponse.GetResponseStream())
//        {
//            using (StreamReader tReader = new StreamReader(dataStreamResponse))
//            {
//                String responseFromFirebaseServer = tReader.ReadToEnd();

//                FCMResponse response = JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
//                if (response.success == 1)
//                {

//                    Console.WriteLine("succeeded");
//                }
//                else if (response.failure == 1)
//                {
//                    Console.WriteLine("failed");

//                }

//            }
//        }

//    }
//}
//var messageInformation = new Message()
//{
//    notification = new Notification()
//    {
//        title = createContactRequestResult.TitleEN,
//        text = createContactRequestResult.DescriptionEN
//    },
//    data = null,
//};
////Object to JSON STRUCTURE => using Newtonsoft.Json;
//string jsonMessage = JsonConvert.SerializeObject(messageInformation);

//var ServerKey = "AAAAL8gQsTo:APA91bF7E4PK2ga_wOkxu4sqUSILOiGBb080JCsjz5pdDV-6ECFYib8Vmeuk1JjOOgG4WDmn6ZMsWWWGFfVbH40ntw4Lny_GvM2EU2Wzd91RYQDSpgPdc6ye5sJ4arkqJXEH5_8CKRmR";
//// Create request to Firebase API
//var notificationRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
//notificationRequest.Headers.TryAddWithoutValidation("Authorization", "key =" + ServerKey);
//notificationRequest.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
//HttpResponseMessage res;
//using (var client = new HttpClient())
//{
//    res = await client.SendAsync(notificationRequest);
//}