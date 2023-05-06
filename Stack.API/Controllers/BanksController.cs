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
    [Route("api/Banks")]
    [ApiController]
  //  [Authorize] // Require Authorization to access API endpoints . 
    public class BanksController : BaseResultHandlerController<BanksService>
    {
        public BanksController(BanksService _service) : base(_service)
        {

        }
        [HttpGet("GetAllBanks")]
        public async Task<IActionResult> GetAllBanks()
        {
            return await GetResponseHandler(async () => await service.GetAllBanks());
        }
        [HttpPost("CreateBank")]
        public async Task<IActionResult> CreateBank(BankToCreateModel BankToCreateModel)
        {
            return await AddItemResponseHandler(async () => await service.CreateBank(BankToCreateModel));
        }

        [HttpPost("EditBank")]
        public async Task<IActionResult> EditStore(BankToEditModel BankToEditModel)
        {
            return await EditItemResponseHandler(async () => await service.EditBank(BankToEditModel));
        }

        [HttpPost("EditBankImage")]
        public async Task<IActionResult> EditBankImage(EditBankImageModel BankToEditModel)
        {
            return await EditItemResponseHandler(async () => await service.EditBankImage(BankToEditModel));
        }

        [HttpPost("DeleteBank")]
        public async Task<IActionResult> DeleteStore(DeleteObjectDTO bankToDelete)
        {
            return await AddItemResponseHandler(async () => await service.DeleteBank(bankToDelete));
        }



    }
}
