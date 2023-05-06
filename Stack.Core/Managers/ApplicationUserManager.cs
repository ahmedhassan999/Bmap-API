using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public DbSet<ApplicationUser> dbSet;

        public ApplicationUserManager(ApplicationDbContext _context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            dbSet = _context.Set<ApplicationUser>();

        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string username)
        {
            return await FindByNameAsync(username);
        }
        public async Task<ApplicationUser> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await Task.Run(() =>
            {
                var applicationUser = dbSet.Where(a => a.PhoneNumber == phoneNumber).Include("Customer");
                if (applicationUser.ToList().Count != 0)
                    return applicationUser.First();
                else
                    return null;
            });
        }
        public async Task<List<ApplicationUser>> GetCustomersAsync()
        {
            return await Task.Run(() =>
            {
                var applicationUser = dbSet.Include("Customer").Where(a=>a.Customer!=null && a.Customer.IsDeleted==false);
                if (applicationUser.ToList().Count != 0)
                    return applicationUser.ToList();
                else
                    return null;
            });
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string Email)
        {
            return await Task.Run(() =>
            {
                var applicationUser = dbSet.Where(a => a.Email == Email).Include("Customer");
                if (applicationUser.ToList().Count != 0)
                    return applicationUser.First();
                else
                    return null;
            });
        }
        public async Task<ApplicationUser> GetUserByIDAsync(string ID)
        {
            return await Task.Run(() =>
            {
                var applicationUser = dbSet.Where(a => a.Id == ID).Include("Customer");
                if (applicationUser.ToList().Count != 0)
                    return applicationUser.First();
                else
                    return null;
            });
        }
    }
}
