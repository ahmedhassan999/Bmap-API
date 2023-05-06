
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
    public class CustomerService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public IConfiguration Configuration { get; }

        public CustomerService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
            Configuration = configuration;

        }

        public async Task<ApiResponse<List<ApplicationUserDTO>>> GetAllCustomers()
        {
            ApiResponse<List<ApplicationUserDTO>> result = new ApiResponse<List<ApplicationUserDTO>>();
            try
            {
                var CustomersListQuery = await unitOfWork.UserManager.GetCustomersAsync();
                var Customers = CustomersListQuery;

                if (CustomersListQuery != null)
                {
                    result.Succeeded = true;
                    result.Data = mapper.Map<List<ApplicationUserDTO>>(CustomersListQuery);
                    return result;
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customers not found");
                    result.Errors.Add("لم يتم العثور على مستخدمين");
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

        public async Task<ApiResponse<CustomerDTO>> Register(CustomerToCreateModel model)
        {
            ApiResponse<CustomerDTO> result = new ApiResponse<CustomerDTO>();
            try
            {
                var currentUser = await unitOfWork.UserManager.GetUserByEmailAsync(model.Email);
                if (currentUser == null)
                {
                    if (await unitOfWork.RoleManager.RoleExistsAsync("Customer"))
                    {
                        ApplicationUser user = new ApplicationUser
                        {
                            Email = model.Email,
                            UserName = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            Image = Configuration.GetSection("defaultImage").Value,
                        };

                        var createUserResult = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                        await unitOfWork.SaveChangesAsync();

                        if (createUserResult.Succeeded)
                        {

                            var roleresult = await unitOfWork.UserManager.AddToRoleAsync(user, "Customer");

                            var addToRoleResult = await unitOfWork.SaveChangesAsync();

                            if (roleresult.Succeeded == true)
                            {
                                var createdUser = await unitOfWork.UserManager.GetUserByEmailAsync(model.Email);

                                Customer customer = new Customer
                                {
                                    AccountStatus = accountStatus.Pending.ToString(),
                                    UserId = createdUser.Id,
                                    City = model.City,
                                    Country = model.Country,
                                    Province = model.Province,
                                    Street = model.Street,
                                    First = model.First,
                                    FirstMiddle = model.FirstMiddle,
                                    SecondMiddle = model.SecondMiddle,
                                    Last = model.Last,
                                    DateOfBirth = model.DateOfBirth,
                                    Gender = model.Gender,
                                    IsDeleted = false,
                                    JobTitle = model.JobTitle,
                                    NationalID = model.NationalID,
                                    Type = model.Type,
                                    NationalIdBack = Configuration.GetSection("defaultImage").Value,
                                    NationalIdFront = Configuration.GetSection("defaultImage").Value,
                                    ProfilePicture = Configuration.GetSection("defaultImage").Value,
                                };
                                var customerToCreate = await unitOfWork.CustomerManager.CreateAsync(customer);

                                if (customerToCreate != null)
                                {
                                    await unitOfWork.SaveChangesAsync();

                                    result.Data = mapper.Map<CustomerDTO>(customerToCreate);
                                    result.Succeeded = true;
                                    return result;
                                }
                                else
                                {
                                    result.Succeeded = false;
                                    result.Errors.Add("Failed To Create Customer");
                                     result.Errors.Add("فشل في التسجيل");
                                    result.ErrorType = ErrorType.LogicalError;
                                    return result;
                                }
                            }
                            else
                            {
                                result.Succeeded = false;
                                result.Errors.Add("Failed To Create Customer");
                                result.Errors.Add("فشل في التسجيل");
                                result.ErrorType = ErrorType.LogicalError;
                                return result;
                            }
                        }
                        else
                        {
                            result.Succeeded = false;
                            foreach (var error in createUserResult.Errors)
                            {
                                result.Errors.Add(error.Description);
                            }
                            result.ErrorType = ErrorType.LogicalError;
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Customer role doesn't exist !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Email already exist !");
                    result.Errors.Add("هذا البريد الالكتروني بالفعل موجود ");
                    return result;
                }


            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        public async Task<ApiResponse<bool>> EditCustomer(CustomerToEditModel CustomerToEditModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var customer = await unitOfWork.UserManager.GetUserByEmailAsync( CustomerToEditModel.Email);
             
                if (customer != null && customer.Id != CustomerToEditModel.ID)
                {
      
                    result.Succeeded = false;
                    result.Errors.Add("Customer  already exists.");
                    result.Errors.Add("المستخدم موجود بالفعل");
                    return result;
                }
                else
                {
                    var customerToEdit = await unitOfWork.UserManager.GetUserByIDAsync(CustomerToEditModel.ID);
                    if (customerToEdit != null)
                    {
                        customerToEdit.Customer.Gender = CustomerToEditModel.Gender;
                        customerToEdit.Customer.JobTitle = CustomerToEditModel.JobTitle;
                        customerToEdit.Customer.First = CustomerToEditModel.First;
                        customerToEdit.Customer.FirstMiddle = CustomerToEditModel.FirstMiddle;
                        customerToEdit.Customer.SecondMiddle = CustomerToEditModel.SecondMiddle;
                        customerToEdit.Customer.Last = CustomerToEditModel.Last;
                        customerToEdit.Customer.NationalID = CustomerToEditModel.NationalID;
                        //customerToEdit.Customer.NationalIdBack = CustomerToEditModel.NationalIdBack;
                        //customerToEdit.Customer.NationalIdFront = CustomerToEditModel.NationalIdFront;
                        //customerToEdit.Customer.ProfilePicture = CustomerToEditModel.ProfilePicture;
                        //customerToEdit.Image = CustomerToEditModel.ProfilePicture;
                        customerToEdit.Customer.AccountStatus = accountStatus.Pending.ToString();
                        customerToEdit.Customer.Street = CustomerToEditModel.Street;
                        customerToEdit.Customer.Province = CustomerToEditModel.Province;
                        customerToEdit.Customer.City = CustomerToEditModel.City;
                        customerToEdit.Customer.Country = CustomerToEditModel.Country;
                        customerToEdit.Email = CustomerToEditModel.Email;
                        customerToEdit.UserName = CustomerToEditModel.Email;
                        customerToEdit.PhoneNumber = CustomerToEditModel.PhoneNumber;

                        var updateResult = await unitOfWork.UserManager.UpdateAsync(customerToEdit);
                        if (updateResult != null)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Error updating customer details");
                            result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                            return result;
                        }
                    }

                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Customer not found");
                        result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> EditprofilePicture(EditImageModel EditImageModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var customerToEdit = await unitOfWork.UserManager.GetUserByIDAsync(EditImageModel.ID);
                if (customerToEdit != null)
                {

                    customerToEdit.Customer.ProfilePicture = EditImageModel.Image;
                    customerToEdit.Image = EditImageModel.Image;

                    var updateResult = await unitOfWork.UserManager.UpdateAsync(customerToEdit);
                    if (updateResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating customer details");
                        result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customer not found");
                    result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> EditNationalIdFront(EditImageModel EditImageModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var customerToEdit = await unitOfWork.UserManager.GetUserByIDAsync(EditImageModel.ID);
                if (customerToEdit != null)
                {

                    customerToEdit.Customer.NationalIdFront = EditImageModel.Image;

                    var updateResult = await unitOfWork.UserManager.UpdateAsync(customerToEdit);
                    if (updateResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating customer details");
                        result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customer not found");
                    result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> EditNationalIdBack(EditImageModel EditImageModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {

                var customerToEdit = await unitOfWork.UserManager.GetUserByIDAsync(EditImageModel.ID);
                if (customerToEdit != null)
                {

                    customerToEdit.Customer.NationalIdBack = EditImageModel.Image;

                    var updateResult = await unitOfWork.UserManager.UpdateAsync(customerToEdit);
                    if (updateResult != null)
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating customer details");
                        result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customer not found");
                    result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> ApproveCustomer( long customerID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var customerToEdit = await unitOfWork.CustomerManager.GetByIdAsync(customerID);
                if (customerToEdit != null)
                {
                    customerToEdit.AccountStatus = accountStatus.Approved.ToString();

                    var updateResult = await unitOfWork.CustomerManager.UpdateAsync(customerToEdit);
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
                        result.Errors.Add("Error updating customer details");
                        result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customer not found");
                    result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> RejectCustomer(long customerID)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var customerToEdit = await unitOfWork.CustomerManager.GetByIdAsync(customerID);
                if (customerToEdit != null)
                {
                    customerToEdit.AccountStatus = accountStatus.Rejected.ToString();

                    var updateResult = await unitOfWork.CustomerManager.UpdateAsync(customerToEdit);
                    if (updateResult )
                    {
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Error updating customer details");
                        result.Errors.Add("خطأ في تحديث تفاصيل المستخدم");
                        return result;
                    }
                }

                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Customer not found");
                    result.Errors.Add("لم يتم العثور على المستخدم");
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

        public async Task<ApiResponse<bool>> DeleteCustomer(DeleteObjectDTO CustomerToDeleteModel)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                var CustomerToDelete = await unitOfWork.CustomerManager.GetByIdAsync(CustomerToDeleteModel.ID);
                if (CustomerToDelete != null)
                {
                    CustomerToDelete.IsDeleted = true;
                    var updateResult = await unitOfWork.CustomerManager.UpdateAsync(CustomerToDelete);
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


