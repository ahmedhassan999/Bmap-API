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
    [Route("api/NotifiedOffer")]
    [ApiController]
    //[Authorize] // Require Authorization to access API endpoints . 
    public class NotifiedOfferController : BaseResultHandlerController<NotifiedOfferService>
    {
        public NotifiedOfferController(NotifiedOfferService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateNotifiedOffer")]
        public async Task<IActionResult> CreateNotifiedOffer(CreateNotifiedOfferModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNotifiedOffer(model));
        }
        
        [AllowAnonymous]
        [HttpPost("CreateNotifiedOfferRequest")]
        public async Task<IActionResult> CreateNotifiedOfferRequest(CreateNotifiedOfferRequestModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateNotifiedOfferRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteNotifiedOffer")]
        public async Task<IActionResult> DeleteNotifiedOffer(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteNotifiedOffer(model));
        }
        
        [AllowAnonymous]
        [HttpPost("DeleteNotifiedOfferRequest")]
        public async Task<IActionResult> DeleteNotifiedOfferRequest(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteNotifiedOfferRequest(model));
        }



        [AllowAnonymous]
        [HttpPost("EditNotifiedOffer")]
        public async Task<IActionResult> EditNotifiedOffer(EditNotifiedOfferModel model)
        {
            return await AddItemResponseHandler(async () => await service.EditNotifiedOffer(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAllNotifiedOffer")]
        public async Task<IActionResult> GetAllNotifiedOffer()
        {
            return await GetResponseHandler(async () => await service.GetAllNotifiedOffer());
        }


        [AllowAnonymous]
        [HttpGet("GetAllNotifiedOfferRequests/{ID}")]
        public async Task<IActionResult> GetAllNotifiedOfferRequests(long ID)
        {
            return await GetResponseHandler(async () => await service.GetAllNotifiedOfferRequests(ID));
        }
        
        [AllowAnonymous]
        [HttpGet("RegisterDeviceToken/{token}")]
        public async Task<IActionResult> RegisterDeviceToken(string token)
        {
            return await GetResponseHandler(async () => await service.RegisterDeviceToken(token));
        }    
        
        [AllowAnonymous]
        [HttpGet("GetNotifiedOffer/{id}")]
        public async Task<IActionResult> GetNotifiedOffer(long id)
        {
            return await GetResponseHandler(async () => await service.GetNotifiedOffer(id));
        }
        
        [AllowAnonymous]
        [HttpPost("GetAllNotifiedOfferForUser")]
        public async Task<IActionResult> GetAllNotifiedOfferForUser(DateModel model)
        {
            return await AddItemResponseHandler(async () => await service.GetAllNotifiedOfferForUser(model));
        }
               
        [AllowAnonymous]
        [HttpPost("EditOfferImage")]
        public async Task<IActionResult> EditOfferImage(EditImageModel model)
        {
            return await AddItemResponseHandler(async () => await service.EditOfferImage(model));
        }



    }
}
