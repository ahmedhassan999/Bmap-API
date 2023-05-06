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
    [Route("api/ServiceRequests")]
    [ApiController]
    [Authorize] // Require Authorization to access API endpoints . 
    public class ServiceRequestsController : BaseResultHandlerController<ServiceRequestsService>
    {
        public ServiceRequestsController(ServiceRequestsService _service) : base(_service)
        {

        }

        [AllowAnonymous] 
        [HttpPost("CreateServiceRequest")]
        public async Task<IActionResult> CreateServiceRequest(ServiceRequestToCreateModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateServiceRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateIslamicServiceRequest")]
        public async Task<IActionResult> CreateIslamicServiceRequest(CreateIslamicRequestModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateIslamicServiceRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("CreateCorporateServiceRequest")]
        public async Task<IActionResult> CreateCorporateServiceRequest(CreateCorporateRequestModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateCorporateServiceRequest(model));
        }


        [AllowAnonymous]
        [HttpPost("SetServiceRequestToProcessed")]
        public async Task<IActionResult> SetServiceRequestToProcessed(ServiceRequestToEdit model)
        {
            return await EditItemResponseHandler(async () => await service.SetServiceRequestToProcessed(model));
        }



        [AllowAnonymous]
        [HttpPost("SetIslamicRequestToProcessed")]
        public async Task<IActionResult> SetIslamicRequestToProcessed(ServiceRequestToEdit model)
        {
            return await EditItemResponseHandler(async () => await service.SetIslamicRequestToProcessed(model));
        }




        [AllowAnonymous]
        [HttpPost("SetCorporateRequestToProcessed")]
        public async Task<IActionResult> SetCorporateRequestToProcessed(ServiceRequestToEdit model)
        {
            return await EditItemResponseHandler(async () => await service.SetCorporateRequestToProcessed(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteServiceRequest")]
        public async Task<IActionResult> DeleteServiceRequest(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteServiceRequest(model));
        }



        [AllowAnonymous]
        [HttpPost("DeleteIslamicServiceRequest")]
        public async Task<IActionResult> DeleteIslamicServiceRequest(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteIslamicServiceRequest(model));
        }


        [AllowAnonymous]
        [HttpPost("DeleteCorporateServiceRequest")]
        public async Task<IActionResult> DeleteCorporateServiceRequest(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteCorporateServiceRequest(model));
        }




        [AllowAnonymous]
        [HttpGet("GetAllPendingServiceRequests")]
        public async Task<IActionResult> GetAllPendingServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllPendingServiceRequests());
        }


        [AllowAnonymous]
        [HttpGet("GetAllProcessedServiceRequests")]
        public async Task<IActionResult> GetAllProcessedServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllProcessedServiceRequests());
        }



        [AllowAnonymous]
        [HttpGet("GetAllProcessedIslamicServiceRequests")]
        public async Task<IActionResult> GetAllProcessedIslamicServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllProcessedIslamicServiceRequests());
        }


        [AllowAnonymous]
        [HttpGet("GetAllProcessedCorporateServiceRequests")]
        public async Task<IActionResult> GetAllProcessedCorporateServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllProcessedCorporateServiceRequests());
        }




        [AllowAnonymous]
        [HttpGet("GetAllPendingIslamicServiceRequests")]
        public async Task<IActionResult> GetAllPendingIslamicServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllPendingIslamicServiceRequests());
        }


        [AllowAnonymous]
        [HttpGet("GetAllPendingCorporateServiceRequests")]
        public async Task<IActionResult> GetAllPendingCorporateServiceRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllPendingCorporateServiceRequests());
        }


        [AllowAnonymous]
        [HttpPost("CreateServiceRequestComment")]
        public async Task<IActionResult> CreateServiceRequestComment(CreateCommentModel model)
        {
            return await EditItemResponseHandler(async () => await service.CreateServiceRequestComment(model));
        }


        [AllowAnonymous]
        [HttpPost("DeleteServiceRequestComment")]
        public async Task<IActionResult> DeleteServiceRequestComment(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteServiceRequestComment(model));
        }



        [AllowAnonymous]
        [HttpPost("CreateIslamicComment")]
        public async Task<IActionResult> CreateIslamicComment(CreateCommentModel model)
        {
            return await EditItemResponseHandler(async () => await service.CreateIslamicComment(model));
        }


        [AllowAnonymous]
        [HttpPost("CreateCorporateRequestComment")]
        public async Task<IActionResult> CreateCorporateRequestComment(CreateCommentModel model)
        {
            return await EditItemResponseHandler(async () => await service.CreateCorporateRequestComment(model));
        }



        [AllowAnonymous]
        [HttpPost("DeleteIslamicServiceRequestComment")]
        public async Task<IActionResult> DeleteIslamicServiceRequestComment(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteIslamicServiceRequestComment(model));
        }




        [AllowAnonymous]
        [HttpPost("DeleteCorporateServiceRequestComment")]
        public async Task<IActionResult> DeleteCorporateServiceRequestComment(DeleteObjectDTO model)
        {
            return await EditItemResponseHandler(async () => await service.DeleteCorporateServiceRequestComment(model));
        }

    }
}
