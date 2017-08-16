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
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }




        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Review>().ToTable("Reviews");
            builder.Entity<Review>().HasKey(r => new { r.ProductId, r.UserId });

            builder.Entity<ProductRating>().ToTable("dbo.ProductRatings");
            builder.Entity<ProductRating>().HasKey(pr => pr.ProductId);

            builder.Entity<CartItem>().ToTable("Carts");
            builder.Entity<CartItem>().HasKey(c => new { c.VisitorId, c.ProductId });
            builder.Entity<CartItem>().Property(c => c.Amount).IsRequired();

            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(o => o.Id);
            builder.Entity<Order>()
                .HasOne(o => o.Country)
                .WithMany()
                .HasForeignKey(o => o.CountryId);
            builder.Entity<Order>()
                .HasOne(o => o.OrderState)
                .WithMany()
                .HasForeignKey(o => o.StateId);
            builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithMany()
                .HasForeignKey(o => o.PaymentId);
            builder.Entity<Order>()
                .HasOne(o => o.Shipping)
                .WithMany()
                .HasForeignKey(o => o.ShippingId);


            builder.Entity<OrderItem>().ToTable("OrderItems");
            builder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId });

            builder.Entity<OrderState>().ToTable("OrderStates");
            builder.Entity<OrderState>().HasKey(os => os.Id);

            builder.Entity<Payment>().ToTable("Payments");
            builder.Entity<Payment>().HasKey(p => p.Id);

            builder.Entity<Shipping>().ToTable("Shippings");
            builder.Entity<Shipping>().HasKey(s => s.Id);


            base.OnModelCreating(builder);
        }
    }
}
