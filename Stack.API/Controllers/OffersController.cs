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
    public class OffersController : BaseResultHandlerController<OffersService>
    {
        public OffersController(OffersService _service) : base(_service)
        {

        }

        [HttpPost("CreateOffer")]
        public async Task<IActionResult> CreateOffer(CreateOfferModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateOffer(model));
        }


        [HttpPost("EditOffer")]
        public async Task<IActionResult> EditOffer(EditOfferModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditOffer(model));
        }

        [HttpPost("EditOfferQuickView")]
        public async Task<IActionResult> EditOfferQuickView(EditOfferQuickViewModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditOfferQuickView(model));
        }

        [HttpPost("EditOfferIcon")]
        public async Task<IActionResult> EditOfferIcon(EditImageModel model)
        {
            return await EditItemResponseHandler(async () => await service.EditOfferIcon(model));
        }

        [HttpPost("DeleteOffer")]
        public async Task<IActionResult> DeleteOffer(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteOffer(model));
        }

        [HttpPost("CreateOfferBenefit")]
        public async Task<IActionResult> CreateOfferBenefit(CreateOfferBenefitModel model)
        {
            return await AddItemResponseHandler(async () => await service.CreateOfferBenefit(model));
        }

        [HttpPost("DeleteOfferBenefit")]
        public async Task<IActionResult> DeleteOfferBenefit(DeleteObjectDTO model)
        {
            return await RemoveItemResponseHandler(async () => await service.DeleteOfferBenefit(model));
        }

        [HttpGet("GetOffersByServiceTypeId/{id}")]
        public async Task<IActionResult> GetOffersByServiceTypeId(long id)
        {
            return await GetResponseHandler(async () => await service.GetOffersByServiceTypeId(id));
        }

        [HttpGet("GetServiceTypeAndOffersById/{id}")]
        public async Task<IActionResult> GetServiceTypeAndOffersById(long id)
        {
            return await GetResponseHandler(async () => await service.GetServiceTypeAndOffersById(id));
        }

    }
}
