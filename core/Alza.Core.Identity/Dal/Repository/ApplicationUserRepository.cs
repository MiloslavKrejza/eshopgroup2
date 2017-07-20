using Alza.Core.Identity.Configuration;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Identity.Dal.Repository
{
    public class ApplicationUserRepository
    {
        private readonly AlzaIdentityOptions _options;
        private ILogger<ApplicationUserRepository> _logger;

        public ApplicationUserRepository(IOptions<AlzaIdentityOptions> options, ILogger<ApplicationUserRepository> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
            _logger = logger;
        }


        /*********************************************/
        /*                                           */
        /*********************************************/

        public ApplicationUser Add(ApplicationUser entity)
        {
            using (SqlConnection conn = new SqlConnection(_options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("lego.ap_lego_InsApplicationUser", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    //PARAMETRY
                    var par0 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                    par0.Direction = System.Data.ParameterDirection.Output;

                    var par1 = new SqlParameter("AccessFailedCount", System.Data.SqlDbType.Int);
                    par1.Value = ToParameter(entity.AccessFailedCount);
                    var par2 = new SqlParameter("ConcurrencyStamp", System.Data.SqlDbType.NVarChar, -1);
                    par2.Value = ToParameter(entity.ConcurrencyStamp);
                    var par3 = new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 256);
                    par3.Value = ToParameter(entity.Email);
                    var par4 = new SqlParameter("EmailConfirmed", System.Data.SqlDbType.Bit);
                    par4.Value = ToParameter(entity.EmailConfirmed);
                    var par5 = new SqlParameter("LockoutEnabled", System.Data.SqlDbType.Bit);
                    par5.Value = ToParameter(entity.LockoutEnabled);
                    var par6 = new SqlParameter("LockoutEnd", System.Data.SqlDbType.DateTimeOffset, 7);
                    par6.Value = DateTimeOffset.MinValue;
                    var par7 = new SqlParameter("NormalizedEmail", System.Data.SqlDbType.NVarChar, 256);
                    par7.Value = ToParameter(entity.NormalizedEmail);
                    var par8 = new SqlParameter("NormalizedUserName", System.Data.SqlDbType.NVarChar, 256);
                    par8.Value = ToParameter(entity.NormalizedUserName);
                    var par9 = new SqlParameter("PasswordHash", System.Data.SqlDbType.NVarChar, -1);
                    par9.Value = ToParameter(entity.PasswordHash);
                    var par10 = new SqlParameter("PhoneNumber", System.Data.SqlDbType.NVarChar, -1);
                    par10.Value = "unknown";
                    var par11 = new SqlParameter("PhoneNumberConfirmed", System.Data.SqlDbType.Bit);
                    par11.Value = ToParameter(entity.PhoneNumberConfirmed);
                    var par12 = new SqlParameter("SecurityStamp", System.Data.SqlDbType.NVarChar, -1);
                    par12.Value = ToParameter(entity.SecurityStamp);
                    var par13 = new SqlParameter("TwoFactorEnabled", System.Data.SqlDbType.Bit);
                    par13.Value = ToParameter(entity.TwoFactorEnabled);
                    var par14 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
                    par14.Value = ToParameter(entity.UserName);

                    command.Parameters.Add(par0);
                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);
                    command.Parameters.Add(par7);
                    command.Parameters.Add(par8);
                    command.Parameters.Add(par9);
                    command.Parameters.Add(par10);
                    command.Parameters.Add(par11);

                    conn.Open();

                    // Zapiš data do databáze
                    command.ExecuteNonQuery();

                    // Zjisti Id nového objektu
                    entity.Id = (int)par0.Value;
                }
            }

            return entity;
        }



        /*********************************************/
        /*                                           */
        /*********************************************/
        public ApplicationUser Get(int id)
        {
            ApplicationUser result = null;

            using (SqlConnection conn = new SqlConnection(_options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("lego.ap_lego_GetApplicationUser", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    //PARAMETRY
                    var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                    par1.Value = id;

                    var par2 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
                    par2.Value = DBNull.Value;
                    
                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);


                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            //Media
                            ApplicationUser item = new ApplicationUser();
                            item.Id = (int)reader["Id"];
                            item.AccessFailedCount = (int)reader["AccessFailedCount"];
                            item.ConcurrencyStamp = reader["ConcurrencyStamp"] as string;
                            item.Email = reader["Email"] as string;
                            item.EmailConfirmed = (bool)reader["EmailConfirmed"];
                            item.LockoutEnabled = (bool)reader["LockoutEnabled"];
                            item.LockoutEnd = (DateTime)reader["LockoutEnd"];
                            item.NormalizedEmail = reader["NormalizedEmail"] as string;
                            item.NormalizedUserName = reader["NormalizedUserName"] as string;
                            item.PasswordHash = reader["PasswordHash"] as string;
                            item.PhoneNumber = reader["PhoneNumber"] as string;
                            item.PhoneNumberConfirmed = (bool)reader["PhoneNumberConfirmed"];
                            item.SecurityStamp = reader["SecurityStamp"] as string;
                            item.TwoFactorEnabled = (bool)reader["TwoFactorEnabled"];
                            item.UserName = reader["UserName"] as string;
                            
                            //RESULT
                            result = item;
                        }
                    }
                }
            }

            return result;
        }

        public ApplicationUser Get(string name)
        {
            ApplicationUser result = null;

            using (SqlConnection conn = new SqlConnection(_options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("lego.ap_lego_GetApplicationUser", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    //PARAMETRY
                    var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                    par1.Value = DBNull.Value;

                    var par2 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
                    par2.Value = name;

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);


                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            //Media
                            ApplicationUser item = new ApplicationUser();
                            item.Id = (int)reader["Id"];
                            item.AccessFailedCount = (int)reader["AccessFailedCount"];
                            item.ConcurrencyStamp = reader["ConcurrencyStamp"] as string;
                            item.Email = reader["Email"] as string;
                            item.EmailConfirmed = (bool)reader["EmailConfirmed"];
                            item.LockoutEnabled = (bool)reader["LockoutEnabled"];
                            item.LockoutEnd = (DateTime)reader["LockoutEnd"];
                            item.NormalizedEmail = reader["NormalizedEmail"] as string;
                            item.NormalizedUserName = reader["NormalizedUserName"] as string;
                            item.PasswordHash = reader["PasswordHash"] as string;
                            item.PhoneNumber = reader["PhoneNumber"] as string;
                            item.PhoneNumberConfirmed = (bool)reader["PhoneNumberConfirmed"];
                            item.SecurityStamp = reader["SecurityStamp"] as string;
                            item.TwoFactorEnabled = (bool)reader["TwoFactorEnabled"];
                            item.UserName = reader["UserName"] as string;

                            //RESULT
                            result = item;
                        }
                    }
                }
            }

            return result;
        }


        /*********************************************/
        /*               HELPERS                     */
        /*********************************************/

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public Object ToParameter(object something)
        {
            if (something == null)
            {
                return DBNull.Value;
            }

            return something;
        }

    }
}
