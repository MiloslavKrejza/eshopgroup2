using System;
using User.Abstraction;
using Alza.Core.Module.Http;
using User.DAL.Entities;

namespace User
{
    class UserService
    {
        private readonly IUserProfileRepository _userRepos;


        public UserService(IUserProfileRepository userRepos)
        {
            _userRepos = userRepos;
        }

        //ToDo validate input data (UserProfile properties, index uniqueness,...)

        /****************************************/
        /*         GET USER PROFILE             */
        /****************************************/

        /// <summary>
        /// Provides a UserProfile in DTO.data whose Id property matches the id parameter.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns matching UserProfile in DTO data</returns>
        public AlzaAdminDTO GetUserProfile(int id)
        {
            try
            {
                var result = _userRepos.GetProfile(id);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        /****************************************/
        /*         ADD USER PROFILE             */
        /****************************************/

        /// <summary>
        /// Adds a UserProfile to the database
        /// </summary>
        /// <param name="userProfile">UserProfile to be added into the database</param>
        /// <returns>A DTO object containing the UserProfile in its Data property</returns>
        public AlzaAdminDTO AddUserProfile(UserProfile userProfile)
        {
            try
            {
                var result = _userRepos.AddUserProfile(userProfile);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /****************************************/
        /*      UPDATE USER PROFILE             */
        /****************************************/

        /// <summary>
        /// Updates a UserProfile
        /// </summary>
        /// <param name="profile">Updated User profile</param>
        /// <returns>Updated profile in DTO.data</returns>
        public AlzaAdminDTO UpdateUserProfile(UserProfile profile)
        {
            try
            {
                var result = _userRepos.UpdateProfile(profile);
                return AlzaAdminDTO.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }





    }
}
