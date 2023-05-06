
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

namespace Stack.ServiceLayer
{
    public class ContactService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ContactService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }



        public async Task<ApiResponse<ContactRequestDTO>> CreateContactRequest(CreateContactRequestModel request)
        {
            ApiResponse<ContactRequestDTO> result = new ApiResponse<ContactRequestDTO>();
            try
            {
                ContactRequest contactRequestToCreate = new ContactRequest();


                contactRequestToCreate.FirstName = request.FirstName;
                contactRequestToCreate.LastName = request.LastName;
                contactRequestToCreate.Email = request.Email;
                contactRequestToCreate.Message = request.Message;
                contactRequestToCreate.Subject = request.Subject;
                contactRequestToCreate.RequestDate = request.RequestDate;
                contactRequestToCreate.PhoneNumber = request.PhoneNumber;



                var createContactRequestResult = await unitOfWork.ContactRequestsManager.CreateAsync(contactRequestToCreate);

                await unitOfWork.SaveChangesAsync();

                if (createContactRequestResult != null)
                {
                    result.Data = mapper.Map<ContactRequestDTO>(contactRequestToCreate);
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


        public async Task<ApiResponse<NewsletterSubscriptionDTO>> AddNewslettersubscriber(NewsletterSubscriptionModel request)
        {
            ApiResponse<NewsletterSubscriptionDTO> result = new ApiResponse<NewsletterSubscriptionDTO>();

            try
            {
                NewsletterSubscription newsletterSubscriberToAdd = new NewsletterSubscription();
                // gets any suscribe with same email
                var NewsManger = await unitOfWork.NewsletterSubscriptionsManager.GetAsync(a => a.Email == request.Email);
                NewsletterSubscription news = NewsManger.FirstOrDefault();

                if (news != null)
                {
                    result.Data = null;
                    result.Succeeded = false;
                    result.Errors.Add("you are already subscribed to our newsletter !");
                    return result;

                }

                newsletterSubscriberToAdd.Email = request.Email;
                newsletterSubscriberToAdd.SubscriptionDate = request.SubscriptionDate;

                var addNewslettersubscriber = await unitOfWork.NewsletterSubscriptionsManager.CreateAsync(newsletterSubscriberToAdd);

                await unitOfWork.SaveChangesAsync();

                if (addNewslettersubscriber != null)
                {
                    result.Data = mapper.Map<NewsletterSubscriptionDTO>(addNewslettersubscriber);
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create newsletter subscription!");
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


        public async Task<ApiResponse<bool>> DeleteContactRequest(DeleteObjectDTO request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var contactRequestResult = await unitOfWork.ContactRequestsManager.GetByIdAsync(request.ID);

                if (contactRequestResult != null)
                {
                    var removeContactRequestResult = await unitOfWork.ContactRequestsManager.RemoveAsync(contactRequestResult);

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
                        result.Errors.Add("Error deleting contact request !");
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


        public async Task<ApiResponse<bool>> DeleteNewsletterSubscription(DeleteObjectDTO request)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var newsletterSubscriptionResult = await unitOfWork.NewsletterSubscriptionsManager.GetByIdAsync(request.ID);

                if (newsletterSubscriptionResult != null)
                {
                    var removeNewsletterSubscriptionResult = await unitOfWork.NewsletterSubscriptionsManager.RemoveAsync(newsletterSubscriptionResult);

                    await unitOfWork.SaveChangesAsync();

                    if (removeNewsletterSubscriptionResult == true)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error deleting newsletter subscription !");
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


        public async Task<ApiResponse<List<ContactRequestDTO>>> GetAllContactRequests()
        {
            ApiResponse<List<ContactRequestDTO>> result = new ApiResponse<List<ContactRequestDTO>>();
            try
            {

                var contactRequestsResult = await unitOfWork.ContactRequestsManager.GetAsync();

                contactRequestsResult = contactRequestsResult.OrderByDescending(a => a.ID);

                if (contactRequestsResult.ToList() != null)
                {
                    result.Data = mapper.Map<List<ContactRequestDTO>>(contactRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Errors.Add("Unable to retreive contact requests !");
                    result.Succeeded = false;
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

        public async Task<ApiResponse<List<NewsletterSubscriptionDTO>>> GetAllNewsletterSubscriptions()
        {
            ApiResponse<List<NewsletterSubscriptionDTO>> result = new ApiResponse<List<NewsletterSubscriptionDTO>>();
            try
            {

                var newsletterSubscriptionsResult = await unitOfWork.NewsletterSubscriptionsManager.GetAsync();

                if (newsletterSubscriptionsResult.ToList() != null)
                {
                    result.Data = mapper.Map<List<NewsletterSubscriptionDTO>>(newsletterSubscriptionsResult.ToList());
                    result.Succeeded = true;
                    return result;
                }
                else
                {
                    result.Errors.Add("Unable to retreive news letter subscribers !");
                    result.Succeeded = false;
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


