using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.User.DAL.Context;
using User.Abstraction;
using User.DAL.Entities;

namespace Trainee.User.DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserDbContext _context;
        public UserProfileRepository(UserDbContext context)
        {
            _context = context;

        }

        public UserProfile AddUserProfile(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            _context.SaveChanges();
            return profile;
        }

        public void DeleteUserProfile(int id)
        {
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            _context.UserProfiles.Remove(profile);
            _context.SaveChanges();
        }

        public IQueryable<UserProfile> GetAllProfiles()
        {
            return _context.UserProfiles.AsQueryable();
        }

        public UserProfile GetProfile(int id)
        {
            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            return profile;
        }

        public UserProfile UpdateProfile(UserProfile profile)
        {
            var originalProfile = GetProfile(profile.Id);
            _context.Entry(originalProfile).CurrentValues.SetValues(profile);
            _context.SaveChanges();
            return profile;
        }
    }
}
