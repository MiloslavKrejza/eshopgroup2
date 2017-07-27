using Alza.Core.Identity.Business;
using Alza.Core.Identity.Configuration;
using Alza.Core.Identity.Dal.Context;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAlzaCoreIdentity(this IServiceCollection services, Action<AlzaIdentityOptions> setupAction, IConfigurationRoot config)
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

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(ConfigurationManager.ConnectionStrings["BloggingDatabase"].ConnectionString);


            //registruje nastaveni modulu
            services.Configure(setupAction);



            //connectionString si vezme sam DbContext z IOptions<>
            services.AddDbContext<ApplicationDbContext>();


            //Nejde jit cestou repositories, protoze ASP NET Identity vevnitr pouziva EntityFramework
            //takze bych musel prepsat VSECHNO na ADO, z toho duvodu znasilnim jenom dve metody 
            //pres EF DbContext
            //services.AddScoped<ApplicationUserRepository, ApplicationUserRepository>();



            //kam presmerovat neprihlaseneho uzivatele?
            var redir = config.GetSection("Identity:UnAuthorizeRedirectTo").Value;


            services.AddIdentity<ApplicationUser, ApplicationRole>(o => {
                // nastavení hesla
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 5;

                // nastavení pro zablokování účtu
                o.Lockout.AllowedForNewUsers = false;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                o.Lockout.MaxFailedAccessAttempts = 100;

                o.SignIn.RequireConfirmedEmail = false; // tato funkcionalita je přepsána pomocí IsRegisteredAndEmailConfirmedHandler
                o.SignIn.RequireConfirmedPhoneNumber = false;

                o.Cookies.ApplicationCookie.LoginPath = new PathString(redir);
                o.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/Account/Forbidden/");
                o.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(14); //cookie expiruje za 14 dní


                o.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.1234567890";
                o.User.RequireUniqueEmail = false;
            })
               .AddEntityFrameworkStores<ApplicationDbContext, int>()
               .AddUserStore<AlzaUserStore>()
               .AddRoleStore<AlzaRoleStore>()
               .AddUserManager<AlzaUserManager>()
               .AddRoleManager<AlzaRoleManager>()
               .AddDefaultTokenProviders();




            return services;
        }
    }
}
