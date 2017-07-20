using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading;

namespace Alza.Core.Identity.Business
{
    public class AlzaUserManager : UserManager<ApplicationUser>
    {
        public AlzaUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            int i = 5;
        }

        
        
        /// <summary>
        /// Touto propertou vypnu kontrolu/zakladani Rolí ... apod. 
        /// </summary>
        //public override bool SupportsUserRole
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        /// Touto propertou vypnu kontrolu/zakladani uzivatelskych claimu ... apod. 
        /// </summary>
        public override bool SupportsUserClaim
        {
            get
            {
                return false;
            }
        }



        //public override Task<string> GetUserIdAsync(ApplicationUser user)
        //{

        //    int i = 5;



        //    return base.GetUserIdAsync(user);
        //}


            
        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            user.NormalizedUserName = base.NormalizeKey(user.UserName);


            return base.CreateAsync(user, password);
        }


        public override Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            return base.ChangePasswordAsync(user, currentPassword, newPassword);
        }



    }

}
