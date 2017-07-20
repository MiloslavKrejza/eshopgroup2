using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Alza.Module.UserProfile.Dal.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.UserProfile.Business
{
    public class UserProfileService
    {
        private ILogger<UserProfileService> _logger;
        private IRepository<Dal.Entities.UserProfile> _userProfileRepo;

        public UserProfileService(IRepository<Dal.Entities.UserProfile> userProfileRepo,
                              ILogger<UserProfileService> logger)
        {
            _userProfileRepo = userProfileRepo;
            _logger = logger;
        }


        /**********************************************/
        /*       GET  COLLECTIONS                     */
        /**********************************************/
        public AlzaAdminDTO GetUserProfiles()
        {
            try
            {
                var result = _userProfileRepo.Query().ToList();

                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
               
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
       



        /**********************************************/
        /*              GET ITEM                      */
        /**********************************************/
        public AlzaAdminDTO GetUserProfile(int id)
        {
            try
            {
                var result = _userProfileRepo.Get(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
               
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        



        /**********************************************/
        /*              ADD ITEM                      */
        /**********************************************/
        public AlzaAdminDTO AddUserProfile(Dal.Entities.UserProfile item)
        {
            try
            {
                _userProfileRepo.Add(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
               
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        
        




        /**********************************************/
        /*              REMOVE ITEM                      */
        /**********************************************/
        public AlzaAdminDTO RemoveUserProfile(int id)
        {
            try
            {
                _userProfileRepo.Remove(id);
                return AlzaAdminDTO.True;
            }
            catch (Exception e)
            {
               
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        



        /**********************************************/
        /*              UPDATE ITEM                      */
        /**********************************************/
        public AlzaAdminDTO UpdateUserProfile(Dal.Entities.UserProfile item)
        {
            try
            {
                _userProfileRepo.Update(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
               
                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }














        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO Error(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogCritical(errNo + " - " + text);
            return AlzaAdminDTO.Error(errNo, "SomeText");
        }
    }
}
