using Alza.Core.Module.Abstraction;
using Alza.Module.Catalog.Business;
using Alza.Module.Catalog.Configuration;
using Alza.Module.Catalog.Dal.Context;
using Alza.Module.Catalog.Dal.Entities;
using Alza.Module.Catalog.Dal.Repository.Abstraction;
using Alza.Module.Catalog.Dal.Repository.Implementation.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAlzaModuleCatalog(this IServiceCollection services, Action<AlzaCatalogOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }



            //string connectionString = @"Server=DESKTOP-LCV6O88\SQLEXPRESS;Database=AlzaLegoDatabase;User Id=sa;Password=master";
            //services.AddDbContext<EFLocalizationDbContext>(options => options.UseSqlServer(connectionString));




            //registruje nastaveni modulu
            services.Configure(setupAction);

            //connectionString si vezme sam DbContext z IOptions<>
            services.AddDbContext<CatalogDbContext>();


            //REPOSITORY - Mozne ADO nebo EF
            services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IMediaRepository, MediaRepository>();


            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<CatalogService, CatalogService>();




            return services;
        }
    }
}
