using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trainee.Core.Business;
using Trainee.Core.DAL.Abstraction;
using Trainee.Core.DAL.Repositories;
using Trainee.User.Abstraction;
using Trainee.User.Business;



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
            services.AddAlzaCoreIdentity(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Core.IdentityConnection").Value, Configuration);



            services.AddTransient<CountryService, CountryService>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddTransient<UserService, UserService>();


            /*************** POSSIBILITIES ***************/
            //services.AddTransient ... created every time (pretty much)
            //services.AddSingleton ... created only once in the runtime
            //services.AddScoped    ... created only once in a request


            // Add framework services.
            services.AddMvc()
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
