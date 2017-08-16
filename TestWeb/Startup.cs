using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trainee.Core.Business;
using Trainee.Core.Abstraction;
using Trainee.Core.DAL.Repositories;
using Trainee.User.Abstraction;
using Trainee.User.Business;
using Trainee.User.DAL.Repositories;
using Trainee.Core.DAL.Context;
using Microsoft.EntityFrameworkCore;

using Trainee.User.DAL.Context;
using Trainee.Business.Business;

using Trainee.Catalogue.Business;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Repositories;
using Trainee.Catalogue.DAL.Context;
using Trainee.Business.DAL.Context;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Repositories;


namespace TestWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();


            //ALZA CORE - IDENTITY

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


            /*************** POSSIBILITIES ***************/
            //services.AddTransient ... created every time (pretty much)
            //services.AddSingleton ... created only once in the runtime
            //services.AddScoped    ... created only once in a request


            // Add framework services.
            services.AddMvc()
                //.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Environment differents
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



            //ALZA CORE - IDENTITY
            app.UseAlzaCoreIdentity();





            //Routing
            app.UseMvc(routes =>
            {

                //default route
                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index", id = "" }
                );

            });
        }
    }
}
