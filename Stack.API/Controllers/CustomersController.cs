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
    [Route("api/Customers")]
    [ApiController]
    //[Authorize] // Require Authorization to access API endpoints . 
    public class CustomersController : BaseResultHandlerController<CustomerService>
    {
        public CustomersController(CustomerService _service) : base(_service)
        {

        }
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            return await GetResponseHandler(async () => await service.GetAllCustomers());
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CustomerToCreateModel CustomerToCreateModel)
        {
            return await AddItemResponseHandler(async () => await service.Register(CustomerToCreateModel));
        }
        [HttpPost("EditCustomer")]
        public async Task<IActionResult> EditCustomer(CustomerToEditModel CustomerToEditModel)
        {
            return await AddItemResponseHandler(async () => await service.EditCustomer(CustomerToEditModel));
        }

        [HttpPost("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(DeleteObjectDTO CustomerToDelete)
        {
            return await AddItemResponseHandler(async () => await service.DeleteCustomer(CustomerToDelete));
        }
        [HttpPost("EditprofilePicture")]
        public async Task<IActionResult> EditprofilePicture(EditImageModel EditImageModel)
        {
            return await AddItemResponseHandler(async () => await service.EditprofilePicture(EditImageModel));
        }

        [HttpPost("EditNationalIdFront")]
        public async Task<IActionResult> EditNationalIdFront(EditImageModel EditImageModel)
        {
            return await AddItemResponseHandler(async () => await service.EditNationalIdFront(EditImageModel));
        }

        [HttpPost("EditNationalIdBack")]
        public async Task<IActionResult> EditNationalIdBack(EditImageModel EditImageModel)
        {
            return await AddItemResponseHandler(async () => await service.EditNationalIdBack(EditImageModel));
        }


        [HttpGet("ApproveCustomer/{customerID}")]
        public async Task<IActionResult> ApproveCustomer(long customerID )
        {
            return await AddItemResponseHandler(async () => await service.ApproveCustomer(customerID));
        }

        [HttpGet("RejectCustomer/{customerID}")]
        public async Task<IActionResult> RejectCustomer(long customerID)
        {
            return await AddItemResponseHandler(async () => await service.RejectCustomer(customerID));
        }

    }
}
