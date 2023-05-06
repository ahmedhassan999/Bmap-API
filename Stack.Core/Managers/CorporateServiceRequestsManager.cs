﻿using Microsoft.AspNetCore.Identity;
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
    public class CorporateServiceRequestsManager : Repository<CorporateServiceRequest, ApplicationDbContext>
    {
        public CorporateServiceRequestsManager(ApplicationDbContext _context) : base(_context)
        {


        }


    }
}