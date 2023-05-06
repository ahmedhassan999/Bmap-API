using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stack.Core.Managers;
using Stack.DAL;
using Stack.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context, ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // log message and enteries
            }
            catch (DbUpdateException ex)
            {
                // log message and enteries
            }
            catch (Exception ex)
            {
                // Log here.
            }
            return false;
        }
        public ApplicationUserManager UserManager { get; private set; } //Manager for application users table . 
        public RoleManager<IdentityRole> RoleManager { get; private set; } //Manager for application user roles table . 

        private CustomerManager customerManager;
        public CustomerManager CustomerManager
        {
            get
            {
                if (customerManager == null)
                {
                    customerManager = new CustomerManager(context);
                }
                return customerManager;
            }
        }

        private BanksManager banksManager;
        public BanksManager BanksManager
        {
            get
            {
                if (banksManager == null)
                {
                    banksManager = new BanksManager(context);
                }
                return banksManager;
            }
        }

        private ServiceManager serviceManager;
        public ServiceManager ServiceManager
        {
            get
            {
                if (serviceManager == null)
                {
                    serviceManager = new ServiceManager(context);
                }
                return serviceManager;
            }
        } 

        private ServiceTypesManager serviceTypesManager;
        public ServiceTypesManager ServiceTypesManager
        {
            get
            {
                if (serviceTypesManager == null)
                {
                    serviceTypesManager = new ServiceTypesManager(context);
                }
                return serviceTypesManager;
            }
        }

        private ServiceRequestsManager serviceRequestsManager;
        public ServiceRequestsManager ServiceRequestsManager
        {
            get
            {
                if (serviceRequestsManager == null)
                {
                    serviceRequestsManager = new ServiceRequestsManager(context);
                }
                return serviceRequestsManager;
            }
        }

        private NewsletterSubscriptionsManager newsletterSubscriptionsManager;
        public NewsletterSubscriptionsManager NewsletterSubscriptionsManager
        {
            get
            {
                if (newsletterSubscriptionsManager == null)
                {
                    newsletterSubscriptionsManager = new NewsletterSubscriptionsManager(context);
                }
                return newsletterSubscriptionsManager;
            }
        }

        private ContactRequestsManager contactRequestsManager;
        public ContactRequestsManager ContactRequestsManager
        {
            get
            {
                if (contactRequestsManager == null)
                {
                    contactRequestsManager = new ContactRequestsManager(context);
                }
                return contactRequestsManager;
            }
        }

        private OffersManager offersManager;
        public OffersManager OffersManager
        {
            get
            {
                if (offersManager == null)
                {
                    offersManager = new OffersManager(context);
                }
                return offersManager;
            }
        }

        private OfferBenefitsManager offerBenefitsManager;
        public OfferBenefitsManager OfferBenefitsManager
        {
            get
            {
                if (offerBenefitsManager == null)
                {
                    offerBenefitsManager = new OfferBenefitsManager(context);
                }
                return offerBenefitsManager;
            }
        }


        private ServiceRequestsCommentsManager serviceRequestsCommentsManager;
        public ServiceRequestsCommentsManager ServiceRequestsCommentsManager
        {
            get
            {
                if (serviceRequestsCommentsManager == null)
                {
                    serviceRequestsCommentsManager = new ServiceRequestsCommentsManager(context);
                }
                return serviceRequestsCommentsManager;
            }
        }

        private CorporateServiceRequestsManager corporateServiceRequestsManager;
        public CorporateServiceRequestsManager CorporateServiceRequestsManager
        {
            get
            {
                if (corporateServiceRequestsManager == null)
                {
                    corporateServiceRequestsManager = new CorporateServiceRequestsManager(context);
                }
                return corporateServiceRequestsManager;
            }
        }

        private CorporateCommentsManager corporateCommentsManager;
        public CorporateCommentsManager CorporateCommentsManager
        {
            get
            {
                if (corporateCommentsManager == null)
                {
                    corporateCommentsManager = new CorporateCommentsManager(context);
                }
                return corporateCommentsManager;
            }
        }

        private IslamicServiceRequestsManager islamicServiceRequestsManager;
        public IslamicServiceRequestsManager IslamicServiceRequestsManager
        {
            get
            {
                if (islamicServiceRequestsManager == null)
                {
                    islamicServiceRequestsManager = new IslamicServiceRequestsManager(context);
                }
                return islamicServiceRequestsManager;
            }
        }

        private IslamicCommentsManager islamicCommentsManager;
        public IslamicCommentsManager IslamicCommentsManager
        {
            get
            {
                if (islamicCommentsManager == null)
                {
                    islamicCommentsManager = new IslamicCommentsManager(context);
                }
                return islamicCommentsManager;
            }
        }
        
        private NotifiedOfferManager notifiedOfferManager;
        public NotifiedOfferManager NotifiedOfferManager
        {
            get
            {
                if (notifiedOfferManager == null)
                {
                    notifiedOfferManager = new NotifiedOfferManager(context);
                }
                return notifiedOfferManager;
            }
        }   

        private NotifiedOfferRequestManager notifiedOfferRequestManager;
        public NotifiedOfferRequestManager NotifiedOfferRequestManager
        {
            get
            {
                if (notifiedOfferRequestManager == null)
                {
                    notifiedOfferRequestManager = new NotifiedOfferRequestManager(context);
                }
                return notifiedOfferRequestManager;
            }
        }
        
        private NotificationTokenManager notificationTokenManager;
        public NotificationTokenManager NotificationTokenManager
        {
            get
            {
                if (notificationTokenManager == null)
                {
                    notificationTokenManager = new NotificationTokenManager(context);
                }
                return notificationTokenManager;
            }
        }


    }
}
