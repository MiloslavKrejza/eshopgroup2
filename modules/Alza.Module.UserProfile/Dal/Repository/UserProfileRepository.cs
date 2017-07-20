using Alza.Core.Module.Abstraction;
using Alza.Module.UserProfile.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Alza.Module.UserProfile.Dal.Entities;

namespace Alza.Module.UserProfile.Dal.Repository
{
    public class UserProfileRepository : IRepository<Entities.UserProfile>
    {

        private readonly AlzaUserProfileOptions _options;
        private ILogger<UserProfileRepository> _logger;

        public UserProfileRepository(IOptions<AlzaUserProfileOptions> options, ILogger<UserProfileRepository> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
        }


        /*********************************************/
        /*                                           */
        /*********************************************/

        public Entities.UserProfile Add(Entities.UserProfile entity)
        {
            Entities.UserProfile en = new Entities.UserProfile();

            return en;
        }

        public Entities.UserProfile Update(Entities.UserProfile entity)
        {
            Entities.UserProfile en = new Entities.UserProfile();

            return en;
        }

        public void Remove(int id)
        {
            
        }

        public Entities.UserProfile Get(int Id)
        {
            Entities.UserProfile en = new Entities.UserProfile();

            return en;
        }




        /*********************************************/
        /*           MAIN QUERY                      */
        /*********************************************/
        public IQueryable<Entities.UserProfile> Query()
        {
            throw new NotImplementedException();
        }
        




        /*********************************************/
        /*               HELPERS                     */
        /*********************************************/

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public Object ToParameter(object something)
        {
            if (something == null)
            {
                return DBNull.Value;
            }

            return something;
        }

    }
}
