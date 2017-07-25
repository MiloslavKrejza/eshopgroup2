using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using User.DAL.Entities;

namespace Trainee.Core.DAL.Context
{
    class CountryDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public CountryDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().Property(c => c.Id).IsRequired();
            builder.Entity<Country>().HasKey(c => c.Id);
            builder.Entity<Country>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<Country>().Property(c => c.Name).IsRequired();
        }
    }
}
