
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
        public DbSet<ProfileState> ProfileStates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //UserProfiles table settings
            builder.Entity<UserProfile>().ToTable("UserProfiles");
            builder.Entity<UserProfile>().Property(p => p.Id).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.Name).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.Surname).IsRequired();
            builder.Entity<UserProfile>().Property(p => p.ProfileStateId).IsRequired();
            builder.Entity<UserProfile>().HasOne(p => p.Country).WithMany();
            builder.Entity<UserProfile>().HasOne(p => p.ProfileState).WithMany();
            //ProfileStates table settings
            builder.Entity<ProfileState>().ToTable("ProfileStates");
            builder.Entity<ProfileState>().Property(s => s.Id).IsRequired();
            builder.Entity<ProfileState>().Property(s => s.StateName).IsRequired();
            builder.Entity<ProfileState>().HasKey(s => s.Id);
            builder.Entity<ProfileState>().Property(s => s.Id).ValueGeneratedOnAdd();

        }

    }
}
