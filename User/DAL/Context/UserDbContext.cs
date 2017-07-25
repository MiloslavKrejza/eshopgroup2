
using Microsoft.EntityFrameworkCore;
using Trainee.Core.DAL.Context;
using Trainee.User.DAL.Entities;

namespace Trainee.User.DAL.Context
{
    public class UserDbContext : CountryDbContext
    {
        public UserDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ProfileState> UserStates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserProfile>().Property(p => p.Id).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.Name).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.Surname).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.ProfileStateId).IsRequired();
            builder.Entity<UserProfile>().HasOne(p => p.Country).WithMany();
            builder.Entity<UserProfile>().HasOne(p => p.ProfileState).WithMany();


        }

    }
}
