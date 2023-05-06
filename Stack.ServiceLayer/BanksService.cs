
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
    public class BanksService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public BanksService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }
        //Get all available banks
        public async Task<ApiResponse<List<BanksDTO>>> GetAllBanks()
        {
            ApiResponse<List<BanksDTO>> result = new ApiResponse<List<BanksDTO>>();
            try
            {
                var BanksListQuery = await unitOfWork.BanksManager.GetAsync(s => !s.IsDeleted);
                var banks = BanksListQuery.ToList();

                if (BanksListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<BanksDTO>>(banks);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Banks not found");
                    result.Errors.Add("لم يتم العثور على بنوك");
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

        //create bank 
        public async Task<ApiResponse<BanksDTO>> CreateBank(BankToCreateModel BankToCreateModel)
        {
            ApiResponse<BanksDTO> result = new ApiResponse<BanksDTO>();
            try
            {
                var banks = await unitOfWork.BanksManager.GetAsync(s => s.Name == BankToCreateModel.Name && !s.IsDeleted);
                var identicalBank = banks.FirstOrDefault();

                if (identicalBank != null)
                {
                    result.Succeeded = false;
                    result.Errors.Add("Bank  already exists.");
                    result.Errors.Add("البنك موجود بالفعل");
                    return result;
                }
                else
                {

                    Banks bank = new Banks
                    {
                        Name = BankToCreateModel.Name,
                        Image = BankToCreateModel.Image,
                        IsDeleted = false,
                    };

                    var creationResult = await unitOfWork.BanksManager.CreateAsync(bank);
                    if (creationResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<BanksDTO>(creationResult);
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to create bank !");
                        result.Errors.Add("خطأ في إنشاء البنك");
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

        public async Task<ApiResponse<bool>> EditBank(BankToEditModel BankToEditModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var banks = await unitOfWork.BanksManager.GetAsync(s => s.Name == BankToEditModel.Name && !s.IsDeleted&& s.ID!= BankToEditModel.ID);
                var identicalBank = banks.FirstOrDefault();

                if (identicalBank != null)
                {
                    result.Succeeded = false;
                    result.Errors.Add("Bank  already exists.");
                    result.Errors.Add("البنك موجود بالفعل");
                    return result;
                }
                else
                {
                    var bankToEdit = await unitOfWork.BanksManager.GetByIdAsync(BankToEditModel.ID);
                    if (bankToEdit != null)
                    {

                        bankToEdit.Name = BankToEditModel.Name;

                        var updateResult = await unitOfWork.BanksManager.UpdateAsync(bankToEdit);
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
                            result.Errors.Add("Error updating bank details");
                            result.Errors.Add("خطأ في تحديث تفاصيل البنك");
                            return result;
                        }
                    }

                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Bank not found");
                        result.Errors.Add("لم يتم العثور على البنك");
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

        public async Task<ApiResponse<bool>> EditBankImage(EditBankImageModel BankToEditModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var bankToEdit = await unitOfWork.BanksManager.GetByIdAsync(BankToEditModel.ID);
                if (bankToEdit != null)
                {

                    bankToEdit.Image = BankToEditModel.Image;

                    var updateResult = await unitOfWork.BanksManager.UpdateAsync(bankToEdit);
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
                        result.Errors.Add("Error updating bank details");
                        result.Errors.Add("خطأ في تحديث تفاصيل البنك");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Bank not found");
                    result.Errors.Add("لم يتم العثور على البنك");
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

        public async Task<ApiResponse<bool>> DeleteBank(DeleteObjectDTO bankToDeleteModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var bankToDelete = await unitOfWork.BanksManager.GetByIdAsync(bankToDeleteModel.ID);
                if (bankToDelete != null)
                {
                    bankToDelete.IsDeleted = true;
                    var updateResult = await unitOfWork.BanksManager.UpdateAsync(bankToDelete);
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
                        result.Errors.Add("Error deleting bank");
                        result.Errors.Add("خطأ في مسح البنك");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Bank not found");
                    result.Errors.Add("لم يتم العثور على البنك");
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


