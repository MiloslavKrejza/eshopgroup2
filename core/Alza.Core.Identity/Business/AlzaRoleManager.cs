using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Alza.Core.Identity.Business
{
    public class AlzaRoleManager : RoleManager<ApplicationRole>
    {
        public AlzaRoleManager(IRoleStore<ApplicationRole> store, 
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, 
            ILookupNormalizer keyNormalizer, 
            IdentityErrorDescriber errors, 
            ILogger<RoleManager<ApplicationRole>> logger, 
            IHttpContextAccessor contextAccessor) : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
        }



        /// <summary>
        /// Touto propertou vypnu kontrolu/zakladani role claimu ... apod. 
        /// </summary>
        public override bool SupportsRoleClaims
        {
            get
            {
                return false;
            }
        }




    }
}
