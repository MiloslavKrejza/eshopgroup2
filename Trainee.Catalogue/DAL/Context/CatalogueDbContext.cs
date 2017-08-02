using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Catalogue.DAL.Entities;
using Trainee.Core.DAL.Context;

namespace Trainee.Catalogue.DAL.Context
{
    class CatalogueDbContext : CountryDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ProductState> ProductStates { get; set; }


        public CatalogueDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Authors

            builder.Entity<Author>().ToTable("Authors");
            builder.Entity<Author>().Property(a => a.Name).IsRequired();
            builder.Entity<Author>().Property(a => a.Surname).IsRequired();
            builder.Entity<Author>().HasKey(a => a.Id);
            builder.Entity<Author>()
                .HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId);
            //Books

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Book>().HasKey(b => b.Id);
            builder.Entity<Book>().Property(b => b.Name).IsRequired();
            //AuthorsBooks

            builder.Entity<AuthorBook>().ToTable("AuthorsBooks");
            builder.Entity<AuthorBook>().HasKey(ab => new { ab.AuthorId, ab.BookId });
            builder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorsBooks)
                .HasForeignKey(ab => ab.AuthorId);
            builder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorsBooks)
                .HasForeignKey(ab => ab.BookId);
            //Categories

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().Property(c => c.Name).IsRequired();
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            //Formats
            builder.Entity<Format>().ToTable("Formats");
            builder.Entity<Format>().Property(f => f.Name).IsRequired();
            builder.Entity<Format>().HasKey(f => f.Id);

            //Language
            builder.Entity<Language>().ToTable("Languages");
            builder.Entity<Language>().Property(l => l.Name).IsRequired();
            builder.Entity<Language>().HasKey(l => l.Id);

            //Products
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().Property(p => p.Name).IsRequired();
            builder.Entity<Product>().Property(p => p.Price).IsRequired();
            builder.Entity<Product>().Property(p => p.FormatId).IsRequired();
            builder.Entity<Product>().Property(p => p.StateId).IsRequired();
            builder.Entity<Product>().Property(p => p.LanguageId).IsRequired();
            builder.Entity<Product>().Property(p => p.PublisherId).IsRequired();
            builder.Entity<Product>().Property(p => p.CategoryId).IsRequired();
            builder.Entity<Product>().HasKey(p => p.Id);

            builder.Entity<Product>()
                .HasOne(p => p.Format)
                .WithMany()
                .HasForeignKey(p => p.FormatId);
            builder.Entity<Product>()
                .HasOne(p => p.State)
                .WithMany()
                .HasForeignKey(p => p.StateId);
            builder.Entity<Product>()
                .HasOne(p => p.Language)
                .WithMany()
                .HasForeignKey(p => p.LanguageId);
            builder.Entity<Product>()
                .HasOne(p => p.Publisher)
                .WithMany()
                .HasForeignKey(p => p.PublisherId);
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);



            //ProductStates
            builder.Entity<ProductState>().ToTable("ProductStates");
            builder.Entity<ProductState>().Property(p => p.Name).IsRequired();
            builder.Entity<ProductState>().HasKey(p => p.Id);

            //Publishers
            builder.Entity<Publisher>().ToTable("Publishers");
            builder.Entity<Publisher>().Property(p => p.Name).IsRequired();
            builder.Entity<Publisher>().HasKey(p => p.Id);

        }
    }
}
