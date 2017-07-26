using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.User.Abstraction;
using Trainee.User.DAL.Context;
using Trainee.User.DAL.Entities;

namespace Trainee.User.DAL.Repositories
{
    public class ProfileStateRepository : IProfileStateRepository
    {
        UserDbContext _context;
        public ProfileStateRepository(UserDbContext context)
        {
            _context = context;
        }

        public ProfileState AddState(ProfileState state)
        {
            _context.ProfileStates.Add(state);
            return state;
        }

        public void DeleteState(int Id)
        {
            var state = _context.ProfileStates.FirstOrDefault();
            _context.ProfileStates.Remove(state);
            _context.SaveChanges();
        }

        public IQueryable<ProfileState> GetAllStates()
        {
            return _context.ProfileStates.AsQueryable();
        }

        public ProfileState GetState(int id)
        {
            return _context.ProfileStates.FirstOrDefault(s => s.Id == id);
        }

        public ProfileState UpdateState(ProfileState state)
        {
            throw new NotImplementedException();
        }
    }
}
