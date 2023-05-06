
using Microsoft.Extensions.DependencyInjection;
using Stack.DTOs.Models;
using Stack.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stack.API.Extensions
{
    public static class ServiceExtensions
    {
        //Referencing service files to be injected into the API Controllers . 
        public static void AddBusinessServices(this IServiceCollection caller)
        {

            //Examples for referencing service files . 
            caller.AddScoped<CustomerService>();
            caller.AddScoped<ServiceService>();
            caller.AddScoped<UsersService>();
            caller.AddScoped<CustomerService>();
            caller.AddScoped<ServiceTypesService>();
            caller.AddScoped<BanksService>();
            caller.AddScoped<ServiceRequestsService>();
            caller.AddScoped<ContactService>();
            caller.AddScoped<OffersService>();
            caller.AddScoped<NotifiedOfferService>();

        }

    }
}
