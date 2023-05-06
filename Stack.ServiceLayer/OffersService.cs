
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
    public class OffersService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public OffersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<OfferDTO>> CreateOffer(CreateOfferModel model)
        {
            ApiResponse<OfferDTO> result = new ApiResponse<OfferDTO>();
            try
            {

                Offer offerToCreate = new Offer();

                offerToCreate.NameAR = model.NameAR;
                offerToCreate.NameEN = model.NameEN;
                offerToCreate.Rate = model.Rate;
                offerToCreate.MinimumSalary = model.MinimumSalary;
                offerToCreate.BankName = model.BankName;
                offerToCreate.Icon = model.Icon;
                offerToCreate.AnnualFee = model.AnnualFee;
                offerToCreate.ServiceTypesId = model.ServiceTypeId;

                var createOfferResult = await unitOfWork.OffersManager.CreateAsync(offerToCreate);

                await unitOfWork.SaveChangesAsync();

                if (createOfferResult != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<OfferDTO>(createOfferResult);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to create offer , Please try again !");
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

        public async Task<ApiResponse<OfferDTO>> EditOffer(EditOfferModel model)
        {
            ApiResponse<OfferDTO> result = new ApiResponse<OfferDTO>();
            try
            {

                var offerToEdit = await unitOfWork.OffersManager.GetByIdAsync(model.ID);

                if(offerToEdit != null)
                {
                    offerToEdit.NameAR = model.NameAR;
                    offerToEdit.NameEN = model.NameEN;
                    offerToEdit.Rate = model.Rate;
                    offerToEdit.MinimumSalary = model.MinimumSalary;
                    offerToEdit.BankName = model.BankName;
                    offerToEdit.AnnualFee = model.AnnualFee;

                    var updateOfferResult = await unitOfWork.OffersManager.UpdateAsync(offerToEdit);


                    await unitOfWork.SaveChangesAsync();

                    if(updateOfferResult == true)
                    {
                        result.Succeeded = true;
                        result.Data = mapper.Map<OfferDTO>(offerToEdit);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to edit offer , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to edit offer , Please try again !");
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

        public async Task<ApiResponse<OfferDTO>> EditOfferQuickView(EditOfferQuickViewModel model)
        {
            ApiResponse<OfferDTO> result = new ApiResponse<OfferDTO>();
            try
            {

                var offerToEdit = await unitOfWork.OffersManager.GetByIdAsync(model.ID);

                if (offerToEdit != null)
                {
                    offerToEdit.JoiningOffersEN = model.JoiningOffersEN;
                    offerToEdit.JoiningOffersAR = model.JoiningOffersAR;
                    offerToEdit.KeyFeaturesAR = model.KeyFeaturesAR;
                    offerToEdit.KeyFeaturesEN = model.KeyFeaturesEN;
                    offerToEdit.RewardFeaturesAR = model.RewardFeaturesAR;
                    offerToEdit.RewardFeaturesEN = model.RewardFeaturesEN;
                    offerToEdit.ThingsToBeAwareOfAR = model.ThingsToBeAwareOfAR;
                    offerToEdit.ThingsToBeAwareOfEN = model.ThingsToBeAwareOfEN;


                    var updateOfferResult = await unitOfWork.OffersManager.UpdateAsync(offerToEdit);


                    await unitOfWork.SaveChangesAsync();

                    if (updateOfferResult == true)
                    {
                        result.Succeeded = true;
                        result.Data = mapper.Map<OfferDTO>(offerToEdit);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to edit offer quick view , Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to edit offer quick view , Please try again !");
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


        public async Task<ApiResponse<OfferDTO>> EditOfferIcon(EditImageModel model)
        {
            ApiResponse<OfferDTO> result = new ApiResponse<OfferDTO>();
            try
            {

                var offerToEdit = await unitOfWork.OffersManager.GetByIdAsync(long.Parse(model.ID));

                if (offerToEdit != null)
                {

                    offerToEdit.Icon = model.Image;

                    var updateOfferResult = await unitOfWork.OffersManager.UpdateAsync(offerToEdit);

                    if (updateOfferResult)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<OfferDTO>(offerToEdit);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating offer image");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("offer not found");
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

        public async Task<ApiResponse<bool>> DeleteOffer(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var offerToDelete = await unitOfWork.OffersManager.GetByIdAsync(model.ID);

                if (offerToDelete != null)
                {


                    var removeResult = await unitOfWork.OffersManager.RemoveAsync(offerToDelete);


                    if (removeResult)
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

        public async Task<ApiResponse<OfferBenefitDTO>> CreateOfferBenefit(CreateOfferBenefitModel model)
        {
            ApiResponse<OfferBenefitDTO> result = new ApiResponse<OfferBenefitDTO>();
            try
            {

                var offerBenefitsResult = await unitOfWork.OfferBenefitsManager.GetAsync( a => a.ID == model.OfferId);

                List<OfferBenefit> offerBenefitsList = offerBenefitsResult.ToList();

                if (offerBenefitsList.Count >= 8)
                {

                    result.Succeeded = false;
                    result.Errors.Add("Failed to create offer benefit , offer has 8 or more benefist already !");
                    return result;

                }
                else
                {

                    OfferBenefit offerBenefitToCreate = new OfferBenefit();
                    offerBenefitToCreate.OfferId = model.OfferId;
                    offerBenefitToCreate.DescriptionAR = model.DescriptionAR;
                    offerBenefitToCreate.DescriptionEN = model.DescriptionEN;

                    var createOfferBenefitResult = await unitOfWork.OfferBenefitsManager.CreateAsync(offerBenefitToCreate);

                    await unitOfWork.SaveChangesAsync();

                    if (createOfferBenefitResult != null)
                    {
                        result.Succeeded = true;
                        result.Data = mapper.Map<OfferBenefitDTO>(createOfferBenefitResult);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create offer benefit , Please try again !");
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

        public async Task<ApiResponse<bool>> DeleteOfferBenefit(DeleteObjectDTO model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var offerBenefitToDelete = await unitOfWork.OfferBenefitsManager.GetByIdAsync(model.ID);

                if (offerBenefitToDelete != null)
                {


                    var removeResult = await unitOfWork.OfferBenefitsManager.RemoveAsync(offerBenefitToDelete);


                    if (removeResult)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error deleting offer benefit");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("offer benefit not found");
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

        public async Task<ApiResponse<List<OfferDTO>>> GetOffersByServiceTypeId(long Id)
        {
            ApiResponse<List<OfferDTO>> result = new ApiResponse<List<OfferDTO>>();
            try
            {
                var offersResult = await unitOfWork.OffersManager.GetAsync(s => s.ServiceTypesId == Id, includeProperties: "OfferBenefits");

                var offersList = offersResult.ToList();

                if (offersList != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<OfferDTO>>(offersList);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("offers not found");
                    result.Errors.Add("لم يتم العثور على الخدمات");
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



        public async Task<ApiResponse<ServiceTypesDTO>> GetServiceTypeAndOffersById(long Id)
        {
            ApiResponse<ServiceTypesDTO> result = new ApiResponse<ServiceTypesDTO>();
            try
            {
                var serviceTypeResult = await unitOfWork.ServiceTypesManager.GetAsync(s => s.ID == Id, includeProperties: "Offers,Offers.OfferBenefits");

                var serviceType = serviceTypeResult.FirstOrDefault();

                if (serviceType != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<ServiceTypesDTO>(serviceType);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Service type not found");
                    result.Errors.Add("لم يتم العثور على الخدمات");
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


