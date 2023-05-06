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
    public class ServiceRequestsService
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ServiceRequestsService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ServiceRequestsDTO>>> GetAllPendingServiceRequests()
        {
            ApiResponse<List<ServiceRequestsDTO>> result = new ApiResponse<List<ServiceRequestsDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.ServiceRequestsManager.GetAsync(a => a.Status == "Pending" , includeProperties:"Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<ServiceRequestsDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch pending service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة المعلقة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<ServiceRequestsDTO>>> GetAllProcessedServiceRequests()
        {
            ApiResponse<List<ServiceRequestsDTO>> result = new ApiResponse<List<ServiceRequestsDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.ServiceRequestsManager.GetAsync(a => a.Status == "Processed", includeProperties: "Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<ServiceRequestsDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch processed service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<ServiceRequestsDTO>> CreateServiceRequest(ServiceRequestToCreateModel model)
        {
            ApiResponse<ServiceRequestsDTO> result = new ApiResponse<ServiceRequestsDTO>();
            try
            {

                ServiceRequests serviceRequestToCreate = new ServiceRequests();

                serviceRequestToCreate.FirstName = model.FirstName;
                serviceRequestToCreate.LastName = model.LastName;
                serviceRequestToCreate.Date = model.Date;
                serviceRequestToCreate.BankName = model.BankName;
                serviceRequestToCreate.Status = "Pending";
                serviceRequestToCreate.OfferTitle = model.OfferTitle;
                serviceRequestToCreate.Email = model.Email;
                serviceRequestToCreate.PhoneNumber = model.PhoneNumber;

                var createServiceRequestResult = await unitOfWork.ServiceRequestsManager.CreateAsync(serviceRequestToCreate);

                if(createServiceRequestResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<ServiceRequestsDTO>(createServiceRequestResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create service request , Please try again !");
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

        public async Task<ApiResponse<ServiceRequestsDTO>> SetServiceRequestToProcessed(ServiceRequestToEdit model)
        {
            ApiResponse<ServiceRequestsDTO> result = new ApiResponse<ServiceRequestsDTO>();
            try
            {
         
                var serviceRequestToUpdate = await unitOfWork.ServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {

                    serviceRequestToUpdate.Status = "Processed";

                    var updateServiceRequestResult = await unitOfWork.ServiceRequestsManager.UpdateAsync(serviceRequestToUpdate);

                    if (updateServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = mapper.Map<ServiceRequestsDTO>(serviceRequestToUpdate);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create service request , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create service request , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteServiceRequest(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var serviceRequestToUpdate = await unitOfWork.ServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {


                    var deleteServiceRequestResult = await unitOfWork.ServiceRequestsManager.RemoveAsync(serviceRequestToUpdate);

                    if (deleteServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete service request , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete service request , Please try again !");
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

        public async Task<ApiResponse<ServiceRequestCommentDTO>> CreateServiceRequestComment(CreateCommentModel model)
        {
            ApiResponse<ServiceRequestCommentDTO> result = new ApiResponse<ServiceRequestCommentDTO>();
            try
            {

                ServiceRequestComment serviceRequestCommentToCreate = new ServiceRequestComment();

                serviceRequestCommentToCreate.Date = model.Date;
                serviceRequestCommentToCreate.Comment = model.Comment;
                serviceRequestCommentToCreate.ServiceRequestsId = model.ServiceRequestId;

                var createServiceRequestCommentResult = await unitOfWork.ServiceRequestsCommentsManager.CreateAsync(serviceRequestCommentToCreate);

                if (createServiceRequestCommentResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<ServiceRequestCommentDTO>(createServiceRequestCommentResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create comment , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteServiceRequestComment(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var commentToDelete = await unitOfWork.ServiceRequestsCommentsManager.GetByIdAsync(model.ID);

                if (commentToDelete != null)
                {


                    var deleteCommentResult = await unitOfWork.ServiceRequestsCommentsManager.RemoveAsync(commentToDelete);

                    if (deleteCommentResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete comment  , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete comment  , Please try again !");
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


        //Islamic Requests .
        public async Task<ApiResponse<IslamicServiceRequestDTO>> CreateIslamicServiceRequest(CreateIslamicRequestModel model)
        {
            ApiResponse<IslamicServiceRequestDTO> result = new ApiResponse<IslamicServiceRequestDTO>();
            try
            {

                IslamicServiceRequest serviceRequestToCreate = new IslamicServiceRequest();

                serviceRequestToCreate.FirstName = model.FirstName;
                serviceRequestToCreate.LastName = model.LastName;
                serviceRequestToCreate.Date = model.Date;
                serviceRequestToCreate.Status = "Pending";
                serviceRequestToCreate.Email = model.Email;
                serviceRequestToCreate.PhoneNumber = model.PhoneNumber;
                serviceRequestToCreate.TimeToCall = model.TimeToCall;
                serviceRequestToCreate.Nationality = model.Nationality;
                serviceRequestToCreate.MonthlySalary = model.MonthlySalary;
                serviceRequestToCreate.Type = model.Type;



                var createServiceRequestResult = await unitOfWork.IslamicServiceRequestsManager.CreateAsync(serviceRequestToCreate);

                if (createServiceRequestResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<IslamicServiceRequestDTO>(createServiceRequestResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create service request , Please try again !");
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

        public async Task<ApiResponse<IslamicRequestCommmentDTO>> CreateIslamicComment(CreateCommentModel model)
        {
            ApiResponse<IslamicRequestCommmentDTO> result = new ApiResponse<IslamicRequestCommmentDTO>();
            try
            {

                IslamicRequestComment serviceRequestCommentToCreate = new IslamicRequestComment();

                serviceRequestCommentToCreate.Date = model.Date;
                serviceRequestCommentToCreate.Comment = model.Comment;
                serviceRequestCommentToCreate.IslamicServiceRequestId = model.ServiceRequestId;

                var createServiceRequestCommentResult = await unitOfWork.IslamicCommentsManager.CreateAsync(serviceRequestCommentToCreate);

                if (createServiceRequestCommentResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<IslamicRequestCommmentDTO>(createServiceRequestCommentResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create comment , Please try again !");
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

        public async Task<ApiResponse<IslamicServiceRequestDTO>> SetIslamicRequestToProcessed(ServiceRequestToEdit model)
        {
            ApiResponse<IslamicServiceRequestDTO> result = new ApiResponse<IslamicServiceRequestDTO>();
            try
            {

                var serviceRequestToUpdate = await unitOfWork.IslamicServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {

                    serviceRequestToUpdate.Status = "Processed";

                    var updateServiceRequestResult = await unitOfWork.IslamicServiceRequestsManager.UpdateAsync(serviceRequestToUpdate);

                    if (updateServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = mapper.Map<IslamicServiceRequestDTO>(serviceRequestToUpdate);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to set request to processed, Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to set request to processed, Please try again !");
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

  
        public async Task<ApiResponse<List<IslamicServiceRequestDTO>>> GetAllPendingIslamicServiceRequests()
        {
            ApiResponse<List<IslamicServiceRequestDTO>> result = new ApiResponse<List<IslamicServiceRequestDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.IslamicServiceRequestsManager.GetAsync(a => a.Status == "Pending", includeProperties: "Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<IslamicServiceRequestDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch pending service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة المعلقة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<IslamicServiceRequestDTO>>> GetAllProcessedIslamicServiceRequests()
        {
            ApiResponse<List<IslamicServiceRequestDTO>> result = new ApiResponse<List<IslamicServiceRequestDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.IslamicServiceRequestsManager.GetAsync(a => a.Status == "Processed", includeProperties: "Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<IslamicServiceRequestDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch processed service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<bool>> DeleteIslamicServiceRequest(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var serviceRequestToUpdate = await unitOfWork.IslamicServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {


                    var deleteServiceRequestResult = await unitOfWork.IslamicServiceRequestsManager.RemoveAsync(serviceRequestToUpdate);

                    if (deleteServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete service request , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete service request , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteIslamicServiceRequestComment(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var commentToDelete = await unitOfWork.IslamicCommentsManager.GetByIdAsync(model.ID);

                if (commentToDelete != null)
                {


                    var deleteCommentResult = await unitOfWork.IslamicCommentsManager.RemoveAsync(commentToDelete);

                    if (deleteCommentResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete comment  , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete comment  , Please try again !");
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


        //Corporate


        public async Task<ApiResponse<CorporateServiceRequestDTO>> CreateCorporateServiceRequest(CreateCorporateRequestModel model)
        {
            ApiResponse<CorporateServiceRequestDTO> result = new ApiResponse<CorporateServiceRequestDTO>();
            try
            {

                CorporateServiceRequest serviceRequestToCreate = new CorporateServiceRequest();

                serviceRequestToCreate.FirstName = model.FirstName;
                serviceRequestToCreate.LastName = model.LastName;
                serviceRequestToCreate.Date = model.Date;
                serviceRequestToCreate.Status = "Pending";
                serviceRequestToCreate.Email = model.Email;
                serviceRequestToCreate.PhoneNumber = model.PhoneNumber;
                serviceRequestToCreate.TimeToCall = model.TimeToCall;
                serviceRequestToCreate.CompanyName = model.CompanyName;
                serviceRequestToCreate.Comment = model.Comment;
                serviceRequestToCreate.Type = model.Type;


                var createServiceRequestResult = await unitOfWork.CorporateServiceRequestsManager.CreateAsync(serviceRequestToCreate);

                if (createServiceRequestResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<CorporateServiceRequestDTO>(createServiceRequestResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create service request , Please try again !");
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

        public async Task<ApiResponse<CorporateRequestCommentDTO>> CreateCorporateRequestComment(CreateCommentModel model)
        {
            ApiResponse<CorporateRequestCommentDTO> result = new ApiResponse<CorporateRequestCommentDTO>();
            try
            {

                CorporateRequestComment serviceRequestCommentToCreate = new CorporateRequestComment();

                serviceRequestCommentToCreate.Date = model.Date;
                serviceRequestCommentToCreate.Comment = model.Comment;
                serviceRequestCommentToCreate.CorporateServiceRequestId = model.ServiceRequestId;

                var createServiceRequestCommentResult = await unitOfWork.CorporateCommentsManager.CreateAsync(serviceRequestCommentToCreate);

                if (createServiceRequestCommentResult != null)
                {
                    await unitOfWork.SaveChangesAsync();

                    result.Succeeded = true;
                    result.Data = mapper.Map<CorporateRequestCommentDTO>(createServiceRequestCommentResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create comment , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteCorporateServiceRequestComment(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var commentToDelete = await unitOfWork.CorporateServiceRequestsManager.GetByIdAsync(model.ID);

                if (commentToDelete != null)
                {


                    var deleteCommentResult = await unitOfWork.CorporateServiceRequestsManager.RemoveAsync(commentToDelete);

                    if (deleteCommentResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete comment  , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete comment  , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteCorporateServiceRequest(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var serviceRequestToUpdate = await unitOfWork.CorporateServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {


                    var deleteServiceRequestResult = await unitOfWork.CorporateServiceRequestsManager.RemoveAsync(serviceRequestToUpdate);

                    if (deleteServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to delete service request , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to delete service request , Please try again !");
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

        public async Task<ApiResponse<List<CorporateServiceRequestDTO>>> GetAllProcessedCorporateServiceRequests()
        {
            ApiResponse<List<CorporateServiceRequestDTO>> result = new ApiResponse<List<CorporateServiceRequestDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.CorporateServiceRequestsManager.GetAsync(a => a.Status == "Processed", includeProperties: "Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<CorporateServiceRequestDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch processed service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<List<CorporateServiceRequestDTO>>> GetAllPendingCorporateServiceRequests()
        {
            ApiResponse<List<CorporateServiceRequestDTO>> result = new ApiResponse<List<CorporateServiceRequestDTO>>();
            try
            {
                var serviceRequestsResult = await unitOfWork.CorporateServiceRequestsManager.GetAsync(a => a.Status == "Pending", includeProperties: "Comments");

                if (serviceRequestsResult != null)
                {

                    result.Data = mapper.Map<List<CorporateServiceRequestDTO>>(serviceRequestsResult.ToList());
                    result.Succeeded = true;
                    return result;

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch pending service requests, please try again !");
                    result.Errors.Add("فشل إحضار طلبات الخدمة المعلقة ، يرجى المحاولة مرة أخرى!");
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

        public async Task<ApiResponse<CorporateServiceRequestDTO>> SetCorporateRequestToProcessed(ServiceRequestToEdit model)
        {
            ApiResponse<CorporateServiceRequestDTO> result = new ApiResponse<CorporateServiceRequestDTO>();
            try
            {

                var serviceRequestToUpdate = await unitOfWork.CorporateServiceRequestsManager.GetByIdAsync(model.ID);

                if (serviceRequestToUpdate != null)
                {

                    serviceRequestToUpdate.Status = "Processed";

                    var updateServiceRequestResult = await unitOfWork.CorporateServiceRequestsManager.UpdateAsync(serviceRequestToUpdate);

                    if (updateServiceRequestResult == true)
                    {

                        await unitOfWork.SaveChangesAsync();

                        result.Succeeded = true;
                        result.Data = mapper.Map<CorporateServiceRequestDTO>(serviceRequestToUpdate);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to set request to processed, Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to set request to processed, Please try again !");
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


