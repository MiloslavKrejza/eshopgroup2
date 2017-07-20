using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseAlzaCoreIdentity(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }



            //User,Client Identity
            app.UseIdentity();





            return app;
        }
    }
}
