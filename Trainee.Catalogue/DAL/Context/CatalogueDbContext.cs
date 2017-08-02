using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Catalogue.DAL.Context
{
    class CatalogueDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ProductState> ProductStates { get; set; }


        public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().ToTable("Authors");
            builder.Entity<Author>().Property(a => a.Name).IsRequired();
            builder.Entity<Author>().Property(a => a.Surname).IsRequired();
            builder.Entity<Author>().HasKey(s => s.Id);

        }
    }
}
