using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Trainee.Core.DAL.Entities;
using Microsoft.Extensions.Options;

namespace Trainee.Core.DAL.Context
{
    public class CountryDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        //public CountryDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        //{

        //}

        public CountryDbContext(DbContextOptions<CountryDbContext> options)
            : base(options)
        {
            //if (options2 == null)
            //{
            //    throw new ArgumentNullException(nameof(options));
            //}
            //_options2 = options2.Value;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().Property(c => c.Id).IsRequired();
            builder.Entity<Country>().HasKey(c => c.Id);
            builder.Entity<Country>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<Country>().Property(c => c.Name).IsRequired();
            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Country>().Property(c => c.Name).HasColumnName("CountryName");
        }
    }
}
