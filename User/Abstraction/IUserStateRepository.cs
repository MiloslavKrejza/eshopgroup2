using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Traineee.User.Abstraction
{
    interface IUserStateRepository
    {
        /// <summary>
        /// Gets all possible user states
        /// </summary>
        /// <returns>A Queryable object of UserStates</returns>
        IQueryable<UserState> GetAllStates();
        /// <summary>
        /// Gets UserState with specified ID
        /// </summary>
        /// <param name="id">ID of wanted UserState</param>
        /// <returns>UserState with specified ID if it exists</returns>
        UserState GetState(int id);
        /// <summary>
        /// Adds new UserState
        /// </summary>
        /// <param name="state">UserState to be added</param>
        /// <returns>Added UserState</returns>
        UserState AddState(UserState state);
        /// <summary>
        /// Updates specified UserState
        /// </summary>
        /// <param name="state">UserState to be updated</param>
        /// <returns>Updated UserState</returns>
        UserState UpdateState(UserState state);
        /// <summary>
        /// Deletes UserState with specified ID
        /// </summary>
        /// <param name="Id">ID of UserState to be deleted</param>
        void DeleteState(int Id);


    }
}
