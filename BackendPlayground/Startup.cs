using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Repositories;
using Trainee.Catalogue.DAL.Context;
using Trainee.Core.DAL.Context;

using Microsoft.EntityFrameworkCore;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Repositories;
using Trainee.Business.Business;
using Trainee.Business.DAL.Context;

namespace BackendPlayground
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddDbContext<CountryDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Core.Countries")));
            services.AddDbContext<CatalogueDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Catalogue")));
            services.AddTransient<ICategoryRelationshipRepository, CategoryRelationshipRepository>(sp => { return new CategoryRelationshipRepository(Configuration.GetConnectionString("Trainee.Business")); });
            services.AddTransient<IProductRatingRepository, ProductRatingRepository>(sp => { return new ProductRatingRepository(Configuration.GetConnectionString("Trainee.Business")); });
            services.AddTransient<BusinessService, BusinessService>();
            services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Business")));
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
