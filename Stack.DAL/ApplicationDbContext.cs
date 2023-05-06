
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stack.DAL
{
    //ApplicationDbContext inherits from IdentityDbContext to implement Identity Tables . 
    //Reference the user class that inherits from IdentityUser class, Ex below : "ApplicationUser" .
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<ServiceRequests> ServiceRequests { get; set; }
        public virtual DbSet<ServiceTypes> ServiceTypes { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<ContactRequest> ConatctRequests { get; set; }
        public virtual DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferBenefit> OfferBenefits { get; set; }
        public virtual DbSet<ServiceRequestComment> ServiceRequestComments { get; set; }
        public virtual DbSet<IslamicServiceRequest> IslamicServiceRequests { get; set; }

        public virtual DbSet<CorporateServiceRequest> CorporateServiceRequests{ get; set; }
        public virtual DbSet<CorporateRequestComment> CorporateRequestComments { get; set; }

        public virtual DbSet<IslamicRequestComment> IslamicRequestComments { get; set; }
        public virtual DbSet<NotifiedOffer> NotifiedOffer { get; set; }
        public virtual DbSet<NotifiedOfferRequest> NotifiedOfferRequest { get; set; }
        public virtual DbSet<NotificationToken> NotificationTokens { get; set; }
    }
}
