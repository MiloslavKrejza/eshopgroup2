using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Context;
using Trainee.User.DAL.Entities;
using Trainee.Core.DAL.Context;

namespace Trainee.Business.DAL.Context
{
    public class BusinessDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        internal DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<CategoryRelationshipBO> CategoryRelationships { get; set; }



        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Review>().ToTable("Reviews");
            

            builder.Entity<CategoryRelationshipBO>().ToTable("CategoryRelationships");
            base.OnModelCreating(builder);
        }
    }
}
