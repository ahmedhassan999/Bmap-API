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
    [Route("api/Contact")]
    [ApiController]
    //[Authorize] // Require Authorization to access API endpoints . 
    public class ContactController : BaseResultHandlerController<ContactService>
    {
        public ContactController(ContactService _service) : base(_service)
        {

        }

        [AllowAnonymous]
        [HttpPost("CreateContactRequest")]
        public async Task<IActionResult> CreateContactRequest(CreateContactRequestModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateContactRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteContactRequest")]
        public async Task<IActionResult> DeleteContactRequest(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteContactRequest(model));
        }

        [AllowAnonymous]
        [HttpPost("DeleteNewsletterSubscription")]
        public async Task<IActionResult> DeleteNewsletterSubscription(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteNewsletterSubscription(model));
        }

        [AllowAnonymous]
        [HttpPost("AddNewslettersubscriber")]
        public async Task<IActionResult> AddNewslettersubscriber(NewsletterSubscriptionModel model)
        {
            return await AddItemResponseHandler(async () => await service.AddNewslettersubscriber(model));
        }

        [AllowAnonymous]
        [HttpGet("GetAllContactRequests")]
        public async Task<IActionResult> GetAllContactRequests()
        {
            return await GetResponseHandler(async () => await service.GetAllContactRequests());
        }


        [AllowAnonymous]
        [HttpGet("GetAllNewsletterSubscriptions")]
        public async Task<IActionResult> GetAllNewsletterSubscriptions()
        {
            return await GetResponseHandler(async () => await service.GetAllNewsletterSubscriptions());
        }



    }
}
