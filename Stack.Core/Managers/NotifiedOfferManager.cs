﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers
{
    public class NotifiedOfferManager : Repository<NotifiedOffer, ApplicationDbContext>
    {
        public NotifiedOfferManager(ApplicationDbContext _context) : base(_context)
        {


        }

    }
}
