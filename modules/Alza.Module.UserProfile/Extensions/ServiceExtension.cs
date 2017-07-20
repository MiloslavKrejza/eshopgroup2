using Alza.Core.Module.Abstraction;
using Alza.Module.UserProfile.Business;
using Alza.Module.UserProfile.Configuration;
using Alza.Module.UserProfile.Dal.Entities;
using Alza.Module.UserProfile.Dal.Repository;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAlzaModuleUserProfile(this IServiceCollection services, Action<AlzaUserProfileOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            
            //registruje nastaveni modulu
            services.Configure(setupAction);

            //connectionString si vezme sam DbContext z IOptions<>
            //services.AddDbContext<GamingDbContext>();

            //REPOSITORY - Mozne ADO nebo EF
            services.AddScoped<IRepository<UserProfile>, UserProfileRepository>();


            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<UserProfileService, UserProfileService>();



            return services;
        }
    }
}
