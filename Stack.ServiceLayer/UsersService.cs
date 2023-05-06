
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
    public class UsersService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IConfiguration config;
        private readonly IMapper mapper;

        public UsersService(UnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        //Function for creating a user role . 
        public async Task<ApiResponse<bool>> CreateRole(string roleName)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                bool x = await unitOfWork.RoleManager.RoleExistsAsync(roleName);
                if (!x)
                {
                    var role = new IdentityRole();
                    role.Name = roleName;

                    var res = await unitOfWork.RoleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    result.Succeeded = false;
                    foreach (var error in res.Errors)
                    {
                        result.Errors.Add(error.Description);
                    }
                    return result;
                }
                result.Succeeded = false;
                result.Errors.Add("Unable to create role !");
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                return result;
            }

        }

        //Function for creating a user account with the role of Administrator . 
        public async Task<ApiResponse<bool>> CreateAdminAccount(RegisterModel model)
        {
            ApiResponse<bool> result = new ApiResponse<bool>();
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Image = model.Image
                };

                var createUserResult = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                await unitOfWork.SaveChangesAsync();

                if (createUserResult.Succeeded)
                {

                    var roleresult = await unitOfWork.UserManager.AddToRoleAsync(user, "Administrator");

                    var addToRoleResult = await unitOfWork.SaveChangesAsync();

                    if (roleresult.Succeeded == true)
                    {
                        result.Data = true;
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed To Create Adminstrator");
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
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }

        }

        //Login function that returns a JWT Bearer Token . 
        public async Task<ApiResponse<JwtAccessToken>> LoginAsync(LoginModel model)
        {
            ApiResponse<JwtAccessToken> result = new ApiResponse<JwtAccessToken>();
            try
            {

                //Find user by email . 
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                   
                        //Check user password . 
                        bool res = await unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);

                        if (res)
                        {

                            // Creating JWT Bearer Token . 
                            ClaimsIdentity claims = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.NameIdentifier, user.Id)
                            });

                            IList<string> userRoles = await unitOfWork.UserManager.GetRolesAsync(user);

                            if (userRoles != null && userRoles.Count() > 0)
                            {
                                foreach (string role in userRoles)
                                {
                                    claims.AddClaim(new Claim(ClaimTypes.Role, role));
                                }
                            }

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Token:Key").Value));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(claims),
                                Expires = DateTime.UtcNow.AddDays(0.25), // Set Token Validity Period . 
                                SigningCredentials = creds
                            };

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var token = tokenHandler.CreateToken(tokenDescriptor);

                            result.Data = new JwtAccessToken();
                            result.Data.Token = tokenHandler.WriteToken(token);
                            result.Data.Expiration = token.ValidTo;

                            result.Succeeded = true;
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Invalid login attempt.");
                            result.ErrorType = ErrorType.LogicalError;
                            return result;
                        }

                    }          
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Invalid login attempt.");
                        result.ErrorType = ErrorType.LogicalError;
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

        public async Task<ApiResponse<ApplicationUserDTO>> ChangePasswordAccount(changePasswordModel model)
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                //get admin account by id. 
                var AccountResult = await unitOfWork.UserManager.FindByIdAsync(model.AccountID);

                if (AccountResult != null)
                {
                    var emailExistsResult = await unitOfWork.UserManager.FindByIdAsync(model.AccountID);
                    var checkOldPassword = await unitOfWork.UserManager.CheckPasswordAsync(AccountResult, model.OldPassword);
                    if (checkOldPassword)
                    {

                        var changePasswordResult = await unitOfWork.UserManager.ChangePasswordAsync(AccountResult, model.OldPassword, model.NewPassword);

                        if (changePasswordResult.Succeeded == true)
                        {
                            await unitOfWork.SaveChangesAsync();
                            result.Succeeded = true;
                            result.Data = mapper.Map<ApplicationUserDTO>(AccountResult);
                            return result;
                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("Failed to update password !");
                            return result;
                        }
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update admin account details, Please try again !");
                        return result;
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update admin account details, Please try again !");
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
        public async Task<ApiResponse<ApplicationUserDTO>> GetUserAccountByToken(string Token)
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                var stream = Token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                var userID = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var userAccount = await unitOfWork.UserManager.FindByIdAsync(userID);

                if (userAccount != null)
                {

                    var customersResult = await unitOfWork.CustomerManager.GetAsync(a => a.UserId == userAccount.Id);

                    if (customersResult != null)
                    {
                        userAccount.Customer = customersResult.FirstOrDefault();
                        result.Data = mapper.Map<ApplicationUserDTO>(userAccount);
                        result.Succeeded = true;
                        return result;
                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to get user account details, Please try again !");
                        return result;
                    }
                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to get user account details, Please try again !");
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

        public async Task<ApiResponse<ApplicationUserDTO>> EditAdminAccountDetails(EditAdminAccountModel model)
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                //get admin account by id. 
                var adminAccountResult = await unitOfWork.UserManager.FindByIdAsync(model.ID);

                if (adminAccountResult != null)
                {
                    var emailExistsResult = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                    if (emailExistsResult == null)
                    {
                        //var changePasswordResult = await unitOfWork.UserManager.ChangePasswordAsync(adminAccountResult, model.OldPassword, model.NewPassword);
                        adminAccountResult.Email = model.Email;
                        adminAccountResult.UserName = model.Email;
                        adminAccountResult.PhoneNumber = model.PhoneNumber;
         
                        await unitOfWork.SaveChangesAsync();
                        result.Succeeded = true;
                        result.Data = mapper.Map<ApplicationUserDTO>(adminAccountResult);
                        return result;

                    }
                    else
                    {

                        if (emailExistsResult.Id == adminAccountResult.Id)
                        {
                            //var changePasswordResult = await unitOfWork.UserManager.ChangePasswordAsync(adminAccountResult, model.OldPassword, model.NewPassword);
                            adminAccountResult.Email = model.Email;
                            adminAccountResult.PhoneNumber = model.PhoneNumber;
                            adminAccountResult.UserName = model.Email;

                            await unitOfWork.SaveChangesAsync();

                            result.Succeeded = true;
                            result.Data = mapper.Map<ApplicationUserDTO>(adminAccountResult);
                            return result;

                        }
                        else
                        {
                            result.Succeeded = false;
                            result.Errors.Add("An account with a similar email address already exists, Please choose a different email address !");
                            return result;
                        }
                    }

                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update admin account details, Please try again !");
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

        public async Task<ApiResponse<ApplicationUserDTO>> EditAdminPicture(EditAdminImageModel model)
        {
            ApiResponse<ApplicationUserDTO> result = new ApiResponse<ApplicationUserDTO>();
            try
            {
                //get admin account by id. 
                var adminAccountResult = await unitOfWork.UserManager.FindByIdAsync(model.ID);

                if (adminAccountResult != null)
                {


                    adminAccountResult.Image = model.Image;

                    var updateAdminImageResult = await unitOfWork.UserManager.UpdateAsync(adminAccountResult);


                    await unitOfWork.SaveChangesAsync();


                    if(updateAdminImageResult.Succeeded == true)
                    {
                        result.Data = mapper.Map<ApplicationUserDTO>(adminAccountResult);
                        result.Succeeded = true;
                        return result;

                    }
                    else
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Failed to update admin account details, Please try again !");
                        return result;
                    }



                }
                else
                {
                    result.Succeeded = false;
                    result.Errors.Add("Failed to update admin account details, Please try again !");
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


