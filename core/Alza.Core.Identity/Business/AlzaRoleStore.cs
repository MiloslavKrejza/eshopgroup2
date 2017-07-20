using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Alza.Core.Identity.Dal.Context;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Alza.Core.Identity.Business
{
    public class AlzaRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, int>
    {
        private ILogger<AlzaRoleStore> _logger;
        private ApplicationDbContext _context;

        public AlzaRoleStore(ILogger<AlzaRoleStore> logger,
            ApplicationDbContext context,
            IdentityErrorDescriber describer = null) : base(context, describer)
        {
            _context = context;
            _logger = logger;
        }



        public override Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //------------------------------------------
                //EF CORE -----------------------------------
                //------------------------------------------

                _context.Roles.Add(role);
                _context.SaveChanges();


                //------------------------------------------
                //ADO -----------------------------------
                //------------------------------------------


                //AspNet user
                //var par0 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //par0.Direction = System.Data.ParameterDirection.Output;

                //var par1 = new SqlParameter("ConcurrencyStamp", System.Data.SqlDbType.NVarChar, -1);
                //par1.Value = ToParameter(role.ConcurrencyStamp);
                //var par2 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 256);
                //par2.Value = ToParameter(role.Name);
                //var par3 = new SqlParameter("NormalizedName", System.Data.SqlDbType.NVarChar, 256);
                //par3.Value = ToParameter(role.NormalizedName);


                //_context.Database.ExecuteSqlCommand(@"[lego].[ap_lego_InsApplicationRole] 
                //                @Id
                //                ,@ConcurrencyStamp
                //                ,@Name
                //                ,@NormalizedName"
                //                ,
                //                  par0,
                //                  par1,
                //                  par2,
                //                  par3);


                return Task.FromResult<IdentityResult>(IdentityResult.Success);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<IdentityResult>(IdentityResult.Failed());
            }
        }





        public override Task<ApplicationRole> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //1 - EF core
                //var AppRole = _context.Roles.FirstOrDefault(u => u.Id == Int32.Parse(id));

                //2 - ADO
                var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                par1.Value = Int32.Parse(id);
                var par2 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 256);
                par2.Value = DBNull.Value;
                var AppRole = _context.Roles.FromSql("[lego].[ap_lego_GetApplicationRole] @Id, @Name", par1, par2).FirstOrDefault();

                return Task.FromResult<ApplicationRole>(AppRole);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<ApplicationRole>(null);
            }
        }


        public override Task<ApplicationRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //1 - EF core
                //var AppRole = _context.Roles.FirstOrDefault(u => u.Id == Int32.Parse(id));

                //2 - ADO
                var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                par1.Value = DBNull.Value;
                var par2 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 256);
                par2.Value = normalizedName;
                var AppRole = _context.Roles.FromSql("[lego].[ap_lego_GetApplicationRole] @Id, @Name", par1, par2).FirstOrDefault();

                return Task.FromResult<ApplicationRole>(AppRole);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<ApplicationRole>(null);
            }
        }



        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public Object ToParameter(object something)
        {
            //if (something == null)
            //{
            //    return DBNull.Value;
            //}

            //return something;
            return something == null ? DBNull.Value : something;
        }
    }
}
