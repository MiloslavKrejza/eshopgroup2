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
using Trainee.User.DAL.Context;
using Trainee.User.Business;
using Trainee.Core.Abstraction;
using Trainee.Core.Business;
using Trainee.User.Abstraction;
using Trainee.Core.DAL.Repositories;
using Trainee.User.DAL.Repositories;
using Trainee.Catalogue.Business;

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

            services.AddAlzaCoreIdentity(o => o.connectionString = Configuration.GetSection("ConnectionStrings:Alza.Core.IdentityConnection").Value, Configuration);

            services.AddTransient<CountryService, CountryService>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<UserService, UserService>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();

            services.AddTransient<IProductRatingRepository, ProductRatingRepository>(sp => { return new ProductRatingRepository(Configuration.GetConnectionString("Trainee.Business")); });
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IFormatRepository, FormatRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderStateRepository, OrderStateRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IShippingRepository, ShippingRepository>();
            services.AddTransient<ICategoryRelationshipRepository, CategoryRelationshipRepository>(sp => { return new CategoryRelationshipRepository(Configuration.GetConnectionString("Trainee.Business")); });
            services.AddTransient<IProductRepository, ProductRepository>(sp => { return new ProductRepository(services.BuildServiceProvider().GetService<CatalogueDbContext>(), Configuration.GetConnectionString("Trainee.Catalogue.Cat")); });
            services.AddTransient<IReviewRepository, ReviewRepository>();

            services.AddTransient<BusinessService, BusinessService>();
            services.AddTransient<CatalogueService, CatalogueService>();


            services.AddDbContext<CountryDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Core.Countries")));
            services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.User.Users")));
            services.AddDbContext<CatalogueDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Catalogue.Cat")));
            services.AddDbContext<BusinessDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Trainee.Business")));


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
