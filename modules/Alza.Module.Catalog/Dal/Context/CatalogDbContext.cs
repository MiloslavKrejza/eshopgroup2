using Alza.Module.Catalog.Configuration;
using Alza.Module.Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Context
{
    public class CatalogDbContext : DbContext
    {

        /***************************************************************/
        /*      NASTAVENI DB - KONFIGURACE, OPTIONS */
        /***************************************************************/

        private readonly AlzaCatalogOptions _options2;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<AlzaCatalogOptions> options2) :base(options)
        {
            if (options2 == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options2 = options2.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options2.connectionString);
        }


        /***************************************************************/
        /*      ENTITY */
        /***************************************************************/
        public DbSet<Product> Products { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Category> Categories { get; set; }





        /***************************************************************/
        /*      VAZBY ENTIT */
        /***************************************************************/
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.HasDefaultSchema("lego");


            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(m => m.Id);


            builder.Entity<Media>().HasKey(m => m.Id);

            builder.Entity<Category>().HasKey(m => m.Id);




            builder.Entity<MediaType>().HasKey(m => m.Id);




            builder.Entity<ProductCategory>().HasKey(m => new { m.ProductId, m.CategoryId });

            builder.Entity<ProductMedia>().HasKey(m => new { m.ProductId, m.MediaId });


            base.OnModelCreating(builder);
        }




        /***************************************************************/
        /*    OVERRIDE  METHODS */
        /***************************************************************/
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
