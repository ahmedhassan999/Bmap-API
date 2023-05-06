using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stack.API.Controllers.Common;
using Stack.DTOs.Models;
using Stack.DTOs.Requests;
using Stack.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Stack.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class UsersController : BaseResultHandlerController<UsersService>
    {
        public UsersController(UsersService _service) : base(_service)
        {

        }

        [AllowAnonymous] // Allow anonymous calls without authorization to this specific endpoint . 
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            return await AddItemResponseHandler(async () => await service.LoginAsync(model));
        }

        [AllowAnonymous] // Allow anonymous calls without authorization to this specific endpoint . 
        [HttpPost("CreateAdminAccount")]
        public async Task<IActionResult> CreateAdminAccount(RegisterModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateAdminAccount(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateRole")] // Allow anonymous calls without authorization to this specific endpoint . 
        public async Task<IActionResult> CreateRole(string roleName)
        {
            return await AddItemResponseHandler(async () => await service.CreateRole(roleName));
        }

        [AllowAnonymous]
        [HttpPost("ChangePasswordAccount")]
        public async Task<IActionResult> ChangePasswordAccount(changePasswordModel model)
        {
            return await EditItemResponseHandler(async () => await service.ChangePasswordAccount(model));
        }
        [AllowAnonymous]
        [HttpPost("EditAdminAccountDetails")]
        public async Task<IActionResult> EditAdminAccountDetails(EditAdminAccountModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditAdminAccountDetails(model));
        }

        [HttpPost("EditAdminPicture")]
        public async Task<IActionResult> EditAdminPicture(EditAdminImageModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditAdminPicture(model));
        }


        [AllowAnonymous]
        [HttpGet("GetUserAccountByToken")]
        public async Task<IActionResult> GetUserAccountByToken(string token)
        {
            return await GetResponseHandler(async () => await service.GetUserAccountByToken(token));
        }

    }
}
