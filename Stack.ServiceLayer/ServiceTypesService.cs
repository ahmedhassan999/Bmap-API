
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
    public class ServiceTypesService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ServiceTypesService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<ServiceTypesDTO>>> GetAllServiceTypesByServiceID(long ServiceID)
        {
            ApiResponse<List<ServiceTypesDTO>> result = new ApiResponse<List<ServiceTypesDTO>>();
            try
            {
                var ServiceTypesListQuery = await unitOfWork.ServiceTypesManager.GetAsync(s => s.ServicesId == ServiceID , includeProperties: "Offers.OfferBenefits");
                var ServiceTypes = ServiceTypesListQuery.ToList();

                if (ServiceTypes != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ServiceTypesDTO>>(ServiceTypes);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Service types not found");
                    result.Errors.Add("لم يتم العثور على انواع الخدمات");
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
        public async Task<ApiResponse<ServiceTypesDTO>> CreateServiceType(ServiceTypeToCreateModel ServiceTypeToCreateModel)
        {
            ApiResponse<ServiceTypesDTO> result = new ApiResponse<ServiceTypesDTO>();
            try
            {
                var services = await unitOfWork.ServiceManager.GetAsync(s => s.ID == ServiceTypeToCreateModel.ServicesId);
                if (services.FirstOrDefault() != null)
                {

                    var serviceTypes = await unitOfWork.ServiceTypesManager.GetAsync(s => (s.NameAR == ServiceTypeToCreateModel.NameAR   || s.NameEN == ServiceTypeToCreateModel.NameEN ) && !s.IsDeleted && s.ServicesId == ServiceTypeToCreateModel.ServicesId);
                    var identicalserviceType = serviceTypes.FirstOrDefault();

                    if (identicalserviceType != null)
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Service type  already exists.");
                        result.Errors.Add("لخدمة موجود بالفعل");
                        return result;
                    }
                    else
                    {


                        ServiceTypes ServiceTypes = new ServiceTypes
                        {
                            NameAR = ServiceTypeToCreateModel.NameAR,
                            NameEN = ServiceTypeToCreateModel.NameEN,
                            Icon = ServiceTypeToCreateModel.Icon,
                            ServicesId = ServiceTypeToCreateModel.ServicesId,
                            IsDeleted = false,
                        };

                        var creationResult = await unitOfWork.ServiceTypesManager.CreateAsync(ServiceTypes);
                        if (creationResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<ServiceTypesDTO>(creationResult);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to create service type !");
                            result.Errors.Add("خطأ في إنشاء الخدمة");
                            return result;

                        }

                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to fetch service  !");
                    result.Errors.Add("خطأ في استرجاع الخدمة");
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
        public async Task<ApiResponse<bool>> EditServiceType(ServiceTypeToEditModel ServiceTypeToEditModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var serviceTypes = await unitOfWork.ServiceTypesManager.GetAsync(s => (s.NameAR == ServiceTypeToEditModel.NameAR || s.NameEN == ServiceTypeToEditModel.NameEN) && !s.IsDeleted && s.ID != ServiceTypeToEditModel.ID && s.ServicesId==ServiceTypeToEditModel.ServiceID);
                var identicalServiceType = serviceTypes.FirstOrDefault();

                if (identicalServiceType != null)
                {
                    result.Succeeded = false;
                    result.Errors.Add("Service type  already exists.");
                    result.Errors.Add("الخدمة موجود بالفعل");
                    return result;
                }
                else
                {
                    var serviceTypeToEdit = await unitOfWork.ServiceTypesManager.GetByIdAsync(ServiceTypeToEditModel.ID);
                    if (serviceTypeToEdit != null)
                    {

                        serviceTypeToEdit.NameAR = ServiceTypeToEditModel.NameAR;
                        serviceTypeToEdit.NameEN = ServiceTypeToEditModel.NameEN;
                        var updateResult = await unitOfWork.ServiceTypesManager.UpdateAsync(serviceTypeToEdit);
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
                            result.Errors.Add("Error updating service type details");
                            result.Errors.Add("خطأ في تحديث تفاصيل الخدمة");
                            return result;
                        }
                    }

                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add(" service type not found");
                        result.Errors.Add("لم يتم العثور على الخدمة");
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }
        }
        public async Task<ApiResponse<bool>> EditServiceTypeIcon(EditImageModel EditImageModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var serviceTypeToEdit = await unitOfWork.ServiceTypesManager.GetByIdAsync(long.Parse(EditImageModel.ID));

                if (serviceTypeToEdit != null)
                {

                    serviceTypeToEdit.Icon = EditImageModel.Image;

                    var updateResult = await unitOfWork.ServiceTypesManager.UpdateAsync(serviceTypeToEdit);

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
                        result.Errors.Add("Error updating servcie type icon");
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
        public async Task<ApiResponse<bool>> DeleteServiceType(DeleteObjectDTO ServiceTypeToDeleteModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var ServiceTypeToDelete = await unitOfWork.ServiceTypesManager.GetByIdAsync(ServiceTypeToDeleteModel.ID);
                if (ServiceTypeToDelete != null)
                {
                    ServiceTypeToDelete.IsDeleted = true;
                    var updateResult = await unitOfWork.ServiceTypesManager.UpdateAsync(ServiceTypeToDelete);
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
                        result.Errors.Add("Error deleting service type");
                        result.Errors.Add("خطأ في مسح الخدمة");
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

    }

}


