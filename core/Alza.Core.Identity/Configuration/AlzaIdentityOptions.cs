using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Identity.Configuration
{
    public class AlzaIdentityOptions
    {
        public string connectionString { get; set; }

        public string redirectForUnauthorize { get; set; }
    }
}
