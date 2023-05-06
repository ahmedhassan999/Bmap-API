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
    [Route("api/ServiceTypes")]
    [ApiController]
    // [Authorize] // Require Authorization to access API endpoints . 
    public class ServiceTypesController : BaseResultHandlerController<ServiceTypesService>
    {
        public ServiceTypesController(ServiceTypesService _service) : base(_service)
        {

        }
        [HttpGet("GetAllServiceTypesByServiceID/{ServiceID}")]
        public async Task<IActionResult> GetAllServiceTypesByServiceID(long ServiceID)
        {
            return await GetResponseHandler(async () => await service.GetAllServiceTypesByServiceID(ServiceID));
        }

        [HttpPost("CreateServiceType")]
        public async Task<IActionResult> CreateServiceType(ServiceTypeToCreateModel ServiceTypeToCreateModel)
        {
            return await AddItemResponseHandler(async () => await service.CreateServiceType(ServiceTypeToCreateModel));
        }
        [HttpPost("EditServiceType")]
        public async Task<IActionResult> EditServiceType(ServiceTypeToEditModel ServiceTypeToEditModel)
        {
            return await EditItemResponseHandler(async () => await service.EditServiceType(ServiceTypeToEditModel));
        }

        [HttpPost("EditServiceTypeIcon")]
        public async Task<IActionResult> EditServiceTypeIcon(EditImageModel ServiceTypeToEditModel)
        {
            return await EditItemResponseHandler(async () => await service.EditServiceTypeIcon(ServiceTypeToEditModel));
        }

        [HttpPost("DeleteServiceType")]
        public async Task<IActionResult> DeleteServiceType(DeleteObjectDTO ServiceTypeToDelete)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteServiceType(ServiceTypeToDelete));
        }

        //[HttpPost("EditServicetypeIcon")]
        //public async Task<IActionResult> EditServicetypeIcon(EditImageModel EditImageModel)
        //{
        //    return await AddItemResponseHandler(async () => await service.EditServicetypeIcon(EditImageModel));
        //}
    }
}
