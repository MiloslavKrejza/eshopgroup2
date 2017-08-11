using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Trainee.User.Abstraction
{
    /// <summary>
    /// This repository provides CRUD operations for profiles
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Gets user with specified ID
        /// </summary>
        /// <param name="id">ID of wanted user</param>
        /// <returns>UserProfile with specified id if it exsits</returns>
        UserProfile GetProfile(int id);
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Queryable object of all UserProfiles</returns>
        IQueryable<UserProfile> GetAllProfiles();
        /// <summary>
        /// Adds a new UserProfile to database
        /// </summary>
        /// <param name="profile">UserProfile to be added</param>
        /// <returns>Added UserProfile</returns>
        UserProfile AddUserProfile(UserProfile profile);
        /// <summary>
        /// Deletes UserProfile with specified ID from Database
        /// </summary>
        /// <param name="id"></param>
        
        void DeleteUserProfile(int id);
        /// <summary>
        /// Deletes UserProfile with specified ID.
        /// </summary>
        /// <param name="profile">Profile to be updated</param>
        /// <returns>Updated UserProfile</returns>
        UserProfile UpdateProfile(UserProfile profile);
    }
}
