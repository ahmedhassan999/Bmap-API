
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
    public class ServiceService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public ServiceService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ServicesDTO>>> GetAllServices()
        {
            ApiResponse<List<ServicesDTO>> result = new ApiResponse<List<ServicesDTO>>();
            try
            {
                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync(includeProperties: "ServiceTypes,ServiceTypes.Offers,ServiceTypes.Offers.OfferBenefits");

                var services = ServicesListQuery.ToList();

                for(int i = 0; i < services.Count; i++)
                {
                    var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync( a => a.IsDeleted == false && a.ServicesId == services[i].ID);

                    services[i].ServiceTypes = serviceTypesResult.ToList();

                }

                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ServicesDTO>>(services);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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


        public async Task<ApiResponse<ServicesDTO>> GetCreditCardsService()
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {
                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync( a => a.NameEN == "Credit Cards");

                var serviceToReturn = ServicesListQuery.FirstOrDefault();
                 
                var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync(a => a.IsDeleted == false && a.ServicesId == serviceToReturn.ID , includeProperties:"Offers,Offers.OfferBenefits");

                var serviceTypesList = serviceTypesResult.ToList();

                serviceToReturn.ServiceTypes = serviceTypesList;

                

                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(serviceToReturn);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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

        public async Task<ApiResponse<ServicesDTO>> GetPersonalLoansService()
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {

                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync(a => a.NameEN == "Personal Loans");

                var serviceToReturn = ServicesListQuery.FirstOrDefault();

                var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync(a => a.IsDeleted == false && a.ServicesId == serviceToReturn.ID, includeProperties: "Offers,Offers.OfferBenefits");

                var serviceTypesList = serviceTypesResult.ToList();

                serviceToReturn.ServiceTypes = serviceTypesList;



                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(serviceToReturn);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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

        public async Task<ApiResponse<ServicesDTO>> GetLoansService()
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {

                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync(a => a.NameEN == "Loans");

                var serviceToReturn = ServicesListQuery.FirstOrDefault();

                var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync(a => a.IsDeleted == false && a.ServicesId == serviceToReturn.ID, includeProperties: "Offers,Offers.OfferBenefits");

                var serviceTypesList = serviceTypesResult.ToList();

                serviceToReturn.ServiceTypes = serviceTypesList;



                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(serviceToReturn);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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

        public async Task<ApiResponse<ServicesDTO>> GetAccountsService()
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {

                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync(a => a.NameEN == "Accounts");

                var serviceToReturn = ServicesListQuery.FirstOrDefault();

                var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync(a => a.IsDeleted == false && a.ServicesId == serviceToReturn.ID, includeProperties: "Offers,Offers.OfferBenefits");

                var serviceTypesList = serviceTypesResult.ToList();

                serviceToReturn.ServiceTypes = serviceTypesList;



                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(serviceToReturn);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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

        public async Task<ApiResponse<ServicesDTO>> GetInvestmentsService()
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {

                var ServicesListQuery = await unitOfWork.ServiceManager.GetAsync(a => a.NameEN == "Investments");

                var serviceToReturn = ServicesListQuery.FirstOrDefault();

                var serviceTypesResult = await unitOfWork.ServiceTypesManager.GetAsync(a => a.IsDeleted == false && a.ServicesId == serviceToReturn.ID, includeProperties: "Offers,Offers.OfferBenefits");

                var serviceTypesList = serviceTypesResult.ToList();

                serviceToReturn.ServiceTypes = serviceTypesList;



                if (ServicesListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(serviceToReturn);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Services not found");
                    result.Errors.Add("لم يتم العثور على خدمات");
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

        public async Task<ApiResponse<ServicesDTO>> CreateService(ServiceToCreateModel ServiceToCreateModel)
        {
            ApiResponse<ServicesDTO> result = new ApiResponse<ServicesDTO>();
            try
            {
                    Services service = new Services
                    {
                        NameEN = ServiceToCreateModel.NameEN,
                        NameAR = ServiceToCreateModel.NameAR,
                        Header = ServiceToCreateModel.ImgHeader
                    };

                    var creationResult = await unitOfWork.ServiceManager.CreateAsync(service);
                if (creationResult != null)
                {
                    await unitOfWork.SaveChangesAsync();
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServicesDTO>(creationResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create service !");
                    result.Errors.Add("خطأ في إنشاء الخدمة");
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


        public async Task<ApiResponse<SearchResult>> Search(string keywords)
        {
            ApiResponse<SearchResult> result = new ApiResponse<SearchResult>();
            try
            {
                var offersQuery = await unitOfWork.OffersManager.GetAsync(o => o.NameEN.Contains(keywords) || o.NameAR.Contains(keywords));
                var matchedOffers = offersQuery.ToList();

                var serviceTypesQuery = await unitOfWork.ServiceTypesManager.GetAsync(o => o.NameEN.Contains(keywords) || o.NameAR.Contains(keywords));
                var matchedServiceTypes = serviceTypesQuery.ToList();
                if (matchedOffers != null && matchedOffers.Count > 0 || matchedServiceTypes != null && matchedServiceTypes.Count > 0)
                {
                    var searchResult = new SearchResult
                    {
                        Offers = mapper.Map<List<OfferDTO>>(matchedOffers),
                        ServiceTypes = mapper.Map<List<ServiceTypesDTO>>(matchedServiceTypes),
                    };
                    result.Succeeded = true;
                    result.Data = searchResult;
                    return result;
                }
                else
                {
                    result.Succeeded = true;
                    result.Errors.Add("No results found");
                    result.Errors.Add("لا يوجد بيانات");
                    result.ErrorType = ErrorType.NoResultsFound;
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

        //public async Task<ApiResponse<bool>> EditService(ServiceToEditModel ServiceToEditModel)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        var Services = await unitOfWork.ServiceManager.GetAsync(s => (s.NameAR == ServiceToEditModel.NameAR || s.NameEN == ServiceToEditModel.NameEN) && !s.IsDeleted && s.ID != ServiceToEditModel.ID);
        //        var identicalService = Services.FirstOrDefault();

        //        if (identicalService != null)
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("Service  already exists.");
        //            result.Errors.Add("الخدمة موجود بالفعل");
        //            return result;
        //        }
        //        else
        //        {
        //            var servcieToEdit = await unitOfWork.ServiceManager.GetByIdAsync(ServiceToEditModel.ID);
        //            if (servcieToEdit != null)
        //            {

        //                servcieToEdit.NameAR = ServiceToEditModel.NameAR;
        //                servcieToEdit.DescriptionAR = ServiceToEditModel.DescriptionAR;

        //                servcieToEdit.NameEN= ServiceToEditModel.NameEN;
        //                servcieToEdit.DescriptionEN = ServiceToEditModel.DescriptionEN;

        //                var updateResult = await unitOfWork.ServiceManager.UpdateAsync(servcieToEdit);
        //                if (updateResult)
        //                {
        //                    await unitOfWork.SaveChangesAsync();
        //                    result.Succeeded = true;
        //                    result.Data = true;
        //                    return result;
        //                }
        //                else
        //                {
        //                    result.Succeeded = false;
        //                    result.Errors.Add("Error updating servcie details");
        //                    result.Errors.Add("خطأ في تحديث تفاصيل الخدمة");
        //                    return result;
        //                }
        //            }

        //            else
        //            {
        //                result.Succeeded = false;
        //                result.Errors.Add("Service not found");
        //                result.Errors.Add("لم يتم العثور على الخدمة");
        //                return result;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        return result;
        //    }
        //}
        //public async Task<ApiResponse<bool>> EditServiceHeader(EditServiceImageModel EditImageModel)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {

        //        var servcieToEdit = await unitOfWork.ServiceManager.GetByIdAsync(EditImageModel.ID);
        //        if (servcieToEdit != null)
        //        {

        //            servcieToEdit.Header = EditImageModel.Image;

        //            var updateResult = await unitOfWork.ServiceManager.UpdateAsync(servcieToEdit);
        //            if (updateResult)
        //            {
        //                await unitOfWork.SaveChangesAsync();
        //                result.Succeeded = true;
        //                result.Data = true;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Succeeded = false;
        //                result.Errors.Add("Error updating servcie details");
        //                result.Errors.Add("خطأ في تحديث تفاصيل الخدمة");
        //                return result;
        //            }
        //        }

        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("Service not found");
        //            result.Errors.Add("لم يتم العثور على الخدمة");
        //            return result;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        return result;
        //    }
        //}
        //public async Task<ApiResponse<bool>> EditServiceIcon(EditServiceImageModel EditImageModel)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {

        //        var servcieToEdit = await unitOfWork.ServiceManager.GetByIdAsync(EditImageModel.ID);
        //        if (servcieToEdit != null)
        //        {

        //            servcieToEdit.Icon = EditImageModel.Image;

        //            var updateResult = await unitOfWork.ServiceManager.UpdateAsync(servcieToEdit);
        //            if (updateResult)
        //            {
        //                await unitOfWork.SaveChangesAsync();
        //                result.Succeeded = true;
        //                result.Data = true;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Succeeded = false;
        //                result.Errors.Add("Error updating servcie details");
        //                result.Errors.Add("خطأ في تحديث تفاصيل الخدمة");
        //                return result;
        //            }
        //        }

        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("Service not found");
        //            result.Errors.Add("لم يتم العثور على الخدمة");
        //            return result;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        return result;
        //    }
        //}
        //public async Task<ApiResponse<bool>> DeleteService(DeleteObjectDTO serviceToDeleteModel)
        //{
        //    ApiResponse<bool> result = new ApiResponse<bool>();
        //    try
        //    {
        //        var servcieToDelete = await unitOfWork.ServiceManager.GetByIdAsync(serviceToDeleteModel.ID);
        //        if (servcieToDelete != null)
        //        {
        //            servcieToDelete.IsDeleted = true;
        //            var updateResult = await unitOfWork.ServiceManager.UpdateAsync(servcieToDelete);
        //            if (updateResult)
        //            {
        //                await unitOfWork.SaveChangesAsync();
        //                result.Succeeded = true;
        //                result.Data = true;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Succeeded = false;
        //                result.Errors.Add("Error deleting service");
        //                result.Errors.Add("خطأ في مسح الخدمة");
        //                return result;
        //            }
        //        }
        //        else
        //        {
        //            result.Succeeded = false;
        //            result.Errors.Add("service not found");
        //            result.Errors.Add("لم يتم العثور على الخدمة");
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Succeeded = false;
        //        result.Errors.Add(ex.Message);
        //        return result;
        //    }

        //}

    }

}


