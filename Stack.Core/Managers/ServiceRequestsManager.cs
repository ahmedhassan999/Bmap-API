using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.DTOs.Models;
using Stack.Entities.Models;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers
{
    public class ServiceRequestsManager : Repository<ServiceRequests, ApplicationDbContext>
    {
        public ServiceRequestsManager(ApplicationDbContext _context) : base(_context)
        {


        }


        //public async Task<List<ServiceRequestsDTO>> GetPendingServiceRequests()
        //{
        //    return await Task.Run(() =>
        //    {
        //        var PendingServiceRequests = context.ServiceRequests.Where(a => a.Status == "Pending")
        //                                        .Select(ServiceRequest => new ServiceRequestsDTO
        //                                        {
        //                                            ID = ServiceRequest.ID,
        //                                            Date = ServiceRequest.Date,
        //                                            Status = ServiceRequest.Status,
        //                                            Note = ServiceRequest.Note,
        //                                            BanksId = ServiceRequest.BanksId,

        //                                            Services = new ServicesDTO
        //                                            {
        //                                                ID = ServiceRequest.Services.ID,
        //                                                NameAR = ServiceRequest.Services.NameAR,
        //                                                NameEN = ServiceRequest.Services.NameEN,
        //                                                DescriptionAR = ServiceRequest.Services.DescriptionAR,
        //                                                DescriptionEN = ServiceRequest.Services.DescriptionEN,
        //                                            },

        //                                            Banks = new BanksDTO
        //                                            {
        //                                                ID = ServiceRequest.Banks.ID,
        //                                                Name = ServiceRequest.Banks.Name
        //                                            },

        //                                            Customer = new CustomerDTO
        //                                            {
        //                                               ID = ServiceRequest.Customer.ID,
        //                                               NationalID = ServiceRequest.Customer.NationalID ,
        //                                               Type = ServiceRequest.Customer.Type ,
        //                                               City = ServiceRequest.Customer.City ,
        //                                               Province = ServiceRequest.Customer.Province ,
        //                                               Country = ServiceRequest.Customer.Country ,
        //                                               Street = ServiceRequest.Customer.Street,
        //                                               First = ServiceRequest.Customer.First,
        //                                               FirstMiddle = ServiceRequest.Customer.FirstMiddle,
        //                                               SecondMiddle = ServiceRequest.Customer.SecondMiddle,
        //                                               Last = ServiceRequest.Customer.Last,
        //                                               UserId = ServiceRequest.Customer.UserId,
        //                                               Gender = ServiceRequest.Customer.Gender,
        //                                            },

        //                                            ServiceTypes = new ServiceTypesDTO
        //                                            {
        //                                                ID = ServiceRequest.ServiceTypes.ID,
        //                                                NameAR = ServiceRequest.ServiceTypes.NameAR,
        //                                                NameEN = ServiceRequest.ServiceTypes.NameEN,
        //                                            }


        //                                        }).OrderByDescending(x => x.Date).ToList();

        //        return PendingServiceRequests;
        //    });
        //}

        //public async Task<List<ServiceRequestsDTO>> GetAllApprovedServiceRequests()
        //{
        //    return await Task.Run(() =>
        //    {
        //        var ApprovedServiceRequests = context.ServiceRequests.Where(a => a.Status == "Approved")
        //                                        .Select(ServiceRequest => new ServiceRequestsDTO
        //                                        {
        //                                            ID = ServiceRequest.ID,
        //                                            Date = ServiceRequest.Date,
        //                                            Status = ServiceRequest.Status,
        //                                            Note = ServiceRequest.Note,
        //                                            BanksId = ServiceRequest.BanksId,
        //                                            Services = new ServicesDTO
        //                                            {
        //                                                ID = ServiceRequest.Services.ID,
        //                                                NameAR = ServiceRequest.Services.NameAR,
        //                                                NameEN = ServiceRequest.Services.NameEN,
        //                                                DescriptionAR = ServiceRequest.Services.DescriptionAR,
        //                                                DescriptionEN = ServiceRequest.Services.DescriptionEN,
        //                                            },
        //                                            Banks = new BanksDTO
        //                                            {
        //                                                ID = ServiceRequest.Banks.ID,
        //                                                Name = ServiceRequest.Banks.Name
        //                                            },

        //                                            Customer = new CustomerDTO
        //                                            {
        //                                                ID = ServiceRequest.Customer.ID,
        //                                                NationalID = ServiceRequest.Customer.NationalID,
        //                                                Type = ServiceRequest.Customer.Type,
        //                                                City = ServiceRequest.Customer.City,
        //                                                Province = ServiceRequest.Customer.Province,
        //                                                Country = ServiceRequest.Customer.Country,
        //                                                Street = ServiceRequest.Customer.Street,
        //                                                First = ServiceRequest.Customer.First,
        //                                                FirstMiddle = ServiceRequest.Customer.FirstMiddle,
        //                                                SecondMiddle = ServiceRequest.Customer.SecondMiddle,
        //                                                Last = ServiceRequest.Customer.Last,
        //                                                UserId = ServiceRequest.Customer.UserId,
        //                                                Gender = ServiceRequest.Customer.Gender
        //                                            },

        //                                            ServiceTypes = new ServiceTypesDTO
        //                                            {
        //                                                ID = ServiceRequest.ServiceTypes.ID,
        //                                                NameAR = ServiceRequest.ServiceTypes.NameAR,
        //                                                NameEN = ServiceRequest.ServiceTypes.NameEN,
        //                                            }


        //                                        }).OrderByDescending(x => x.Date).ToList();

        //        return ApprovedServiceRequests;
        //    });
        //}

        //public async Task<List<ServiceRequestsDTO>> GetAllRejectedServiceRequests()
        //{
        //    return await Task.Run(() =>
        //    {
        //        var RejecetedServiceRequests = context.ServiceRequests.Where(a => a.Status == "Rejected")
        //                                        .Select(ServiceRequest => new ServiceRequestsDTO
        //                                        {
        //                                            ID = ServiceRequest.ID,
        //                                            Date = ServiceRequest.Date,
        //                                            Status = ServiceRequest.Status,
        //                                            Note = ServiceRequest.Note,
        //                                            RejectionReason = ServiceRequest.RejectionReason,
        //                                            BanksId = ServiceRequest.BanksId,
        //                                            Services = new ServicesDTO
        //                                            {
        //                                                ID = ServiceRequest.Services.ID,
        //                                                NameAR = ServiceRequest.Services.NameAR,
        //                                                NameEN = ServiceRequest.Services.NameEN,
        //                                                DescriptionAR = ServiceRequest.Services.DescriptionAR,
        //                                                DescriptionEN = ServiceRequest.Services.DescriptionEN,
        //                                            },
        //                                            Banks = new BanksDTO
        //                                            {
        //                                                ID = ServiceRequest.Banks.ID,
        //                                                Name = ServiceRequest.Banks.Name
        //                                            },

        //                                            Customer = new CustomerDTO
        //                                            {
        //                                                ID = ServiceRequest.Customer.ID,
        //                                                NationalID = ServiceRequest.Customer.NationalID,
        //                                                Type = ServiceRequest.Customer.Type,
        //                                                City = ServiceRequest.Customer.City,
        //                                                Province = ServiceRequest.Customer.Province,
        //                                                Country = ServiceRequest.Customer.Country,
        //                                                Street = ServiceRequest.Customer.Street,
        //                                                First = ServiceRequest.Customer.First,
        //                                                FirstMiddle = ServiceRequest.Customer.FirstMiddle,
        //                                                SecondMiddle = ServiceRequest.Customer.SecondMiddle,
        //                                                Last = ServiceRequest.Customer.Last,
        //                                                UserId = ServiceRequest.Customer.UserId,
        //                                                Gender = ServiceRequest.Customer.Gender,
        //                                            },

        //                                            ServiceTypes = new ServiceTypesDTO
        //                                            {
        //                                                ID = ServiceRequest.ServiceTypes.ID,
        //                                                NameAR = ServiceRequest.ServiceTypes.NameAR,
        //                                                NameEN = ServiceRequest.ServiceTypes.NameEN,
        //                                            }


        //                                        }).OrderByDescending(x => x.Date).ToList();

        //        return RejecetedServiceRequests;
        //    });
        //}

        //public async Task<List<ServiceRequestsDTO>> GetCustomerServiceRequests(long ID)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var PendingServiceRequests = context.ServiceRequests.Where(a => a.CustomerId == ID)
        //                                        .Select(ServiceRequest => new ServiceRequestsDTO
        //                                        {
        //                                            ID = ServiceRequest.ID,
        //                                            Date = ServiceRequest.Date,
        //                                            Status = ServiceRequest.Status,
        //                                            Note = ServiceRequest.Note,
        //                                            BanksId = ServiceRequest.BanksId,
        //                                            RejectionReason = ServiceRequest.RejectionReason,
        //                                            Services = new ServicesDTO
        //                                            {
        //                                                ID = ServiceRequest.Services.ID,
        //                                                NameAR = ServiceRequest.Services.NameAR,
        //                                                NameEN = ServiceRequest.Services.NameEN,
        //                                                DescriptionAR = ServiceRequest.Services.DescriptionAR,
        //                                                DescriptionEN = ServiceRequest.Services.DescriptionEN,
        //                                            },
        //                                            Banks = new BanksDTO
        //                                            {
        //                                                ID = ServiceRequest.Banks.ID,
        //                                                Name = ServiceRequest.Banks.Name
        //                                            },
        //                                            Customer = new CustomerDTO
        //                                            {
        //                                                ID = ServiceRequest.Customer.ID,
        //                                                NationalID = ServiceRequest.Customer.NationalID,
        //                                                Type = ServiceRequest.Customer.Type,
        //                                                City = ServiceRequest.Customer.City,
        //                                                Province = ServiceRequest.Customer.Province,
        //                                                Country = ServiceRequest.Customer.Country,
        //                                                Street = ServiceRequest.Customer.Street,
        //                                                First = ServiceRequest.Customer.First,
        //                                                FirstMiddle = ServiceRequest.Customer.FirstMiddle,
        //                                                SecondMiddle = ServiceRequest.Customer.SecondMiddle,
        //                                                Last = ServiceRequest.Customer.Last,
        //                                                UserId = ServiceRequest.Customer.UserId,
        //                                                Gender = ServiceRequest.Customer.Gender,
        //                                            },

        //                                            ServiceTypes = new ServiceTypesDTO
        //                                            {
        //                                                ID = ServiceRequest.ServiceTypes.ID,
        //                                                NameAR = ServiceRequest.ServiceTypes.NameAR,
        //                                                NameEN = ServiceRequest.ServiceTypes.NameEN,
        //                                            }


        //                                        }).OrderByDescending(x => x.Date).ToList();

        //        return PendingServiceRequests;
        //    });
        //}



    }
}
