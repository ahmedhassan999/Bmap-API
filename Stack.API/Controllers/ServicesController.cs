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
    [Route("api/Services")]
    [ApiController]
    // [Authorize] // Require Authorization to access API endpoints . 
    public class ServicesController : BaseResultHandlerController<ServiceService>
    {
        public ServicesController(ServiceService _service) : base(_service)
        {

        }

        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            return await GetResponseHandler(async () => await service.GetAllServices());
        }

        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService( [FromForm] ServiceToCreateModel ServiceToCreateModel)
        {
            return await AddItemResponseHandler(async () => await service.CreateService(ServiceToCreateModel));
        }

        [HttpGet("GetCreditCardsService")]
        public async Task<IActionResult> GetCreditCardsService()
        {
            return await GetResponseHandler(async () => await service.GetCreditCardsService());
        }

        [HttpGet("GetPersonalLoansService")]
        public async Task<IActionResult> GetPersonalLoansService()
        {
            return await GetResponseHandler(async () => await service.GetPersonalLoansService());
        }

        [HttpGet("GetLoansService")]
        public async Task<IActionResult> GetLoansService()
        {
            return await GetResponseHandler(async () => await service.GetLoansService());
        }

        [HttpGet("GetAccountsService")]
        public async Task<IActionResult> GetAccountsService()
        {
            return await GetResponseHandler(async () => await service.GetAccountsService());
        }

        [HttpGet("GetInvestmentsService")]
        public async Task<IActionResult> GetInvestmentsService()
        {
            return await GetResponseHandler(async () => await service.GetInvestmentsService());
        }

        [HttpGet("Search/{keywords}")]
        public async Task<IActionResult> Search(string keywords)
        {
            return await GetResponseHandler(async () => await service.Search(keywords));
        }


        //[HttpPost("EditService")]
        //public async Task<IActionResult> EditService(ServiceToEditModel ServiceToEditModel)
        //{
        //    return await AddItemResponseHandler(async () => await service.EditService(ServiceToEditModel));
        //}
        //[HttpPost("EditServiceHeader")]
        //public async Task<IActionResult> EditServiceHeader(EditServiceImageModel EditImageModel)
        //{
        //    return await AddItemResponseHandler(async () => await service.EditServiceHeader(EditImageModel));
        //}
        //[HttpPost("EditServiceIcon")]
        //public async Task<IActionResult> EditServiceIcon(EditServiceImageModel EditImageModel)
        //{
        //    return await AddItemResponseHandler(async () => await service.EditServiceIcon(EditImageModel));
        //}

        //[HttpPost("DeleteService")]
        //public async Task<IActionResult> DeleteService(DeleteObjectDTO serviceToDelete)
        //{
        //    return await AddItemResponseHandler(async () => await service.DeleteService(serviceToDelete));
        //}

    }
}
