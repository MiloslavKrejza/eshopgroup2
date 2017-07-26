using Alza.Core.Identity.Dal.Context;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Alza.Core.Identity.Business
{
    public class AlzaUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>
    {
        private ILogger<AlzaUserStore> _logger;
        private ApplicationDbContext _context;

        public AlzaUserStore(ILogger<AlzaUserStore> logger,
            ApplicationDbContext context,
            IdentityErrorDescriber describer = null) : base(context, describer)
        {
            _context = context;
            _logger = logger;
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                //------------------------------------------
                //EF CORE -----------------------------------
                //------------------------------------------

                _context.Users.Add(user);
                _context.SaveChanges();


                //------------------------------------------
                //ADO -----------------------------------
                //------------------------------------------


                //AspNet user
    //            var par0 = new SqlParameter("Id", System.Data.SqlDbType.Int);
    //            par0.Direction = System.Data.ParameterDirection.Output;

    //            var par1 = new SqlParameter("AccessFailedCount", System.Data.SqlDbType.Int);
    //            par1.Value = ToParameter(user.AccessFailedCount);
    //            var par2 = new SqlParameter("ConcurrencyStamp", System.Data.SqlDbType.NVarChar, -1);
    //            par2.Value = ToParameter(user.ConcurrencyStamp);
    //            var par3 = new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 256);
    //            par3.Value = ToParameter(user.Email);
    //            var par4 = new SqlParameter("EmailConfirmed", System.Data.SqlDbType.Bit);
    //            par4.Value = ToParameter(user.EmailConfirmed);
    //            var par5 = new SqlParameter("LockoutEnabled", System.Data.SqlDbType.Bit);
    //            par5.Value = ToParameter(user.LockoutEnabled);
    //            var par6 = new SqlParameter("LockoutEnd", System.Data.SqlDbType.DateTimeOffset, 7);
    //            par6.Value = DateTimeOffset.MinValue;
    //            var par7 = new SqlParameter("NormalizedEmail", System.Data.SqlDbType.NVarChar, 256);
    //            par7.Value = ToParameter(user.NormalizedEmail);
    //            var par8 = new SqlParameter("NormalizedUserName", System.Data.SqlDbType.NVarChar, 256);
    //            par8.Value = ToParameter(user.NormalizedUserName);
    //            var par9 = new SqlParameter("PasswordHash", System.Data.SqlDbType.NVarChar, -1);
    //            par9.Value = ToParameter(user.PasswordHash);
    //            var par10 = new SqlParameter("PhoneNumber", System.Data.SqlDbType.NVarChar, -1);
    //            par10.Value = "unknown";
    //            var par11 = new SqlParameter("PhoneNumberConfirmed", System.Data.SqlDbType.Bit);
    //            par11.Value = ToParameter(user.PhoneNumberConfirmed);
    //            var par12 = new SqlParameter("SecurityStamp", System.Data.SqlDbType.NVarChar, -1);
    //            par12.Value = ToParameter(user.SecurityStamp);
    //            var par13 = new SqlParameter("TwoFactorEnabled", System.Data.SqlDbType.Bit);
    //            par13.Value = ToParameter(user.TwoFactorEnabled);
    //            var par14 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
    //            par14.Value = ToParameter(user.UserName);


    //            _context.Database.ExecuteSqlCommand(@"[lego].[ap_lego_InsApplicationUser] 
    //                            @Id
    //                            ,@AccessFailedCount
    //                            ,@ConcurrencyStamp
    //                            ,@Email
    //                            ,@EmailConfirmed
    //                            ,@LockoutEnabled
    //                            ,@LockoutEnd
    //                            ,@NormalizedEmail
    //                            ,@NormalizedUserName
    //                            ,@PasswordHash
    //                            ,@PhoneNumber
    //                            ,@PhoneNumberConfirmed
    //                            ,@SecurityStamp
    //                            ,@TwoFactorEnabled
    //                            ,@UserName"
    //,
    //  par0,
    //  par1,
    //  par2,
    //  par3,
    //  par4,
    //  par5,
    //  par6,
    //  par7,
    //  par8,
    //  par9,
    //  par10,
    //  par11,
    //  par12,
    //  par13,
    //  par14);



                //using (SqlConnection conn = new SqlConnection(_options.connectionString))
                //{
                //    using (SqlCommand command = new SqlCommand("lego.ap_lego_InsApplicationUser", conn))
                //    {
                //        command.CommandType = System.Data.CommandType.StoredProcedure;


                //        //PARAMETRY
                //        var par0 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //        par0.Direction = System.Data.ParameterDirection.Output;

                //        var par1 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 200);
                //        par1.Value = ToParameter(entity.Name);

                //        var par2 = new SqlParameter("ImageUrl", System.Data.SqlDbType.NVarChar, -1);
                //        par2.Value = ToParameter(entity.ImageUrl);

                //        var par3 = new SqlParameter("Url", System.Data.SqlDbType.NVarChar, -1);
                //        par3.Value = ToParameter(entity.Url);

                //        var par4 = new SqlParameter("Description", System.Data.SqlDbType.NVarChar, -1);
                //        par4.Value = ToParameter(entity.Description);

                //        var par5 = new SqlParameter("MediaTypeId", System.Data.SqlDbType.Int);
                //        par5.Value = ToParameter(entity.MediaTypeId);

                //        command.Parameters.Add(par0);
                //        command.Parameters.Add(par1);
                //        command.Parameters.Add(par2);
                //        command.Parameters.Add(par3);
                //        command.Parameters.Add(par4);
                //        command.Parameters.Add(par5);


                //        conn.Open();

                //        // Zapiš data do databáze
                //        command.ExecuteNonQuery();

                //        // Zjisti Id nového objektu
                //        entity.Id = (int)par0.Value;
                //    }
                //}





                return Task.FromResult<IdentityResult>(IdentityResult.Success);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<IdentityResult>(IdentityResult.Failed());
            }
        }

        public override Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                //1 - EF core
                var AppUser = _context.Users.FirstOrDefault(u => u.Id == Int32.Parse(userId));

                //2 - ADO
                //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //par1.Value = Int32.Parse(userId);
                //var par2 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
                //par2.Value = DBNull.Value;
                //var AppUserList = _context.Users
                //                .AsNoTracking() //velmi dulezite, jinak si DBContext nacachuje hodnoty z DB a pri zmene udaju o uzivateli vrati "stare" hondoty
                //                .FromSql("[lego].[ap_lego_GetApplicationUser] @Id, @UserName", par1, par2)
                //                .ToList();

                //var AppUser = AppUserList.FirstOrDefault();

                return Task.FromResult<ApplicationUser>(AppUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<ApplicationUser>(null);
            }
        }

        public override Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {

                string userNameTemp = normalizedUserName.Split('@')[0];

                //1 - EF core
                var AppUser = _context.Users.FirstOrDefault(u => u.NormalizedUserName == userNameTemp);

                //2 - ADO
                //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //par1.Value = DBNull.Value;
                //var par2 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
                //par2.Value = normalizedUserName;

                //var AppUserList = _context.Users
                //                .AsNoTracking() //velmi dulezite, jinak si DBContext nacachuje hodnoty z DB a pri zmene udaju o uzivateli vrati "stare" hondoty
                //                .FromSql("[lego].[ap_lego_GetApplicationUser] @Id, @UserName", par1, par2)
                //                .ToList();

                //var AppUser = AppUserList.FirstOrDefault();

                return Task.FromResult<ApplicationUser>(AppUser);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<ApplicationUser>(null);
            }
        }





        public override Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //------------------------------------------
            //EF CORE -----------------------------------
            //------------------------------------------

            _context.Users.Update(user);
            _context.SaveChanges();


            //------------------------------------------
            //ADO -----------------------------------
            //------------------------------------------


//            //AspNet user
//            var par0 = new SqlParameter("Id", System.Data.SqlDbType.Int);
//            par0.Value = ToParameter(user.Id);
//            var par1 = new SqlParameter("AccessFailedCount", System.Data.SqlDbType.Int);
//            par1.Value = ToParameter(user.AccessFailedCount);
//            var par2 = new SqlParameter("ConcurrencyStamp", System.Data.SqlDbType.NVarChar, -1);
//            par2.Value = ToParameter(user.ConcurrencyStamp);
//            var par3 = new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 256);
//            par3.Value = ToParameter(user.Email);
//            var par4 = new SqlParameter("EmailConfirmed", System.Data.SqlDbType.Bit);
//            par4.Value = ToParameter(user.EmailConfirmed);
//            var par5 = new SqlParameter("LockoutEnabled", System.Data.SqlDbType.Bit);
//            par5.Value = ToParameter(user.LockoutEnabled);
//            var par6 = new SqlParameter("LockoutEnd", System.Data.SqlDbType.DateTimeOffset, 7);
//            par6.Value = DateTimeOffset.MinValue;
//            var par7 = new SqlParameter("NormalizedEmail", System.Data.SqlDbType.NVarChar, 256);
//            par7.Value = ToParameter(user.NormalizedEmail);
//            var par8 = new SqlParameter("NormalizedUserName", System.Data.SqlDbType.NVarChar, 256);
//            par8.Value = ToParameter(user.NormalizedUserName);
//            var par9 = new SqlParameter("PasswordHash", System.Data.SqlDbType.NVarChar, -1);
//            par9.Value = ToParameter(user.PasswordHash);
//            var par10 = new SqlParameter("PhoneNumber", System.Data.SqlDbType.NVarChar, -1);
//            par10.Value = ToParameter(user.PhoneNumber);
//            var par11 = new SqlParameter("PhoneNumberConfirmed", System.Data.SqlDbType.Bit);
//            par11.Value = ToParameter(user.PhoneNumberConfirmed);
//            var par12 = new SqlParameter("SecurityStamp", System.Data.SqlDbType.NVarChar, -1);
//            par12.Value = ToParameter(user.SecurityStamp);
//            var par13 = new SqlParameter("TwoFactorEnabled", System.Data.SqlDbType.Bit);
//            par13.Value = ToParameter(user.TwoFactorEnabled);
//            var par14 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
//            par14.Value = ToParameter(user.UserName);


//            _context.Database.ExecuteSqlCommand(@"[lego].[ap_lego_UpdApplicationUser] 
//                                @Id
//                                ,@AccessFailedCount
//                                ,@ConcurrencyStamp
//                                ,@Email
//                                ,@EmailConfirmed
//                                ,@LockoutEnabled
//                                ,@LockoutEnd
//                                ,@NormalizedEmail
//                                ,@NormalizedUserName
//                                ,@PasswordHash
//                                ,@PhoneNumber
//                                ,@PhoneNumberConfirmed
//                                ,@SecurityStamp
//                                ,@TwoFactorEnabled
//                                ,@UserName"
//,
//  par0,
//  par1,
//  par2,
//  par3,
//  par4,
//  par5,
//  par6,
//  par7,
//  par8,
//  par9,
//  par10,
//  par11,
//  par12,
//  par13,
//  par14);




            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }





        /*************************************************************/
        /*************************************************************/
        /*************************************************************/
        // UZIVATELSKE ROLE
        /*************************************************************/
        /*************************************************************/
        /*************************************************************/

        public async override Task AddToRoleAsync(ApplicationUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //ApplicationRole AppRole = null;

                //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //par1.Value = DBNull.Value;
                //var par2 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 256);
                //par2.Value = normalizedRoleName;

                //var AppRoleList = _context.Roles
                //                    .FromSql("[lego].[ap_lego_GetApplicationRole] @Id, @Name", par1, par2)
                //                    .ToList();


                //AppRole = AppRoleList.FirstOrDefault();
                //if (AppRole == null)
                //{
                //    throw new Exception("AppRole == null");
                //}



                //------------------------------------------
                //EF CORE -----------------------------------
                //------------------------------------------
                //DODELAT

                ApplicationRole role = _context.Roles.Where(r => r.Name == "User").First();


                IdentityUserRole<int> asdf = new IdentityUserRole<int>();

                asdf.UserId = user.Id;
                asdf.RoleId = role.Id;

                _context.UserRoles.Add(asdf);

                await _context.SaveChangesAsync();

                //------------------------------------------
                //ADO -----------------------------------
                //------------------------------------------


                //znovu si vytahnu uzivatele z DB, abych ziskal jeho Id
                //var userFromDB = FindByNameAsync(user.NormalizedUserName, cancellationToken).Result;


                ////AspNet user
                //var par3 = new SqlParameter("UserId", System.Data.SqlDbType.Int);
                //par3.Value = ToParameter(userFromDB.Id);
                //var par4 = new SqlParameter("RoleId", System.Data.SqlDbType.Int);
                //par4.Value = ToParameter(AppRole.Id);

                //var cislo = _context.Database.ExecuteSqlCommand(@"[lego].[ap_lego_InsApplicationUserRole] 
                //                @UserId
                //                ,@RoleId"
                //                ,
                //                  par3,
                //                  par4);



                //int i = 5;


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public override Task RemoveFromRoleAsync(ApplicationUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.RemoveFromRoleAsync(user, normalizedRoleName, cancellationToken);
        }

        public override Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                //1 - EF core
                //DOPLNIT


                var aaa = _context.UserRoles.Where(ur => ur.UserId == user.Id).ToList();

                List<ApplicationRole> roles = new List<ApplicationRole>();
                foreach (var item in aaa)
                {
                    var bbb = _context.Roles.Where(r => r.Id == item.RoleId).First();
                    roles.Add(bbb);
                }


                List<string> rolesNames = roles.Select(r => r.NormalizedName).ToList();


                


                //2 - ADO
                //var par1 = new SqlParameter("UserId", System.Data.SqlDbType.Int);
                //par1.Value = user.Id;

                //var AppUserRoles = _context.UserRoles.FromSql("[lego].[ap_lego_GetApplicationUserRoles] @UserId", par1).ToList();

                //List<string> roleNames = new List<string>();
                //foreach (var item in AppUserRoles)
                //{
                //    var rolepar1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
                //    rolepar1.Value = item.RoleId;
                //    var rolepar2 = new SqlParameter("Name", System.Data.SqlDbType.NVarChar, 256);
                //    rolepar2.Value = DBNull.Value;

                    

                //    var roleList = _context.Roles
                //                    .AsNoTracking() //velmi dulezite, jinak si DBContext nacachuje hodnoty z DB a pri zmene udaju o uzivateli vrati "stare" hondoty
                //                    .FromSql("[lego].[ap_lego_GetApplicationRole] @Id, @Name", rolepar1, rolepar2)
                //                    .ToList();

                //    var role = roleList.FirstOrDefault();

                //    roleNames.Add(role.NormalizedName);
                //}

                return Task.FromResult<IList<string>>(rolesNames);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<IList<string>>(null);
            }
        }

        public override Task<bool> IsInRoleAsync(ApplicationUser user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var userRoles = GetRolesAsync(user).Result;

                if (userRoles.Contains(normalizedRoleName))
                {
                    return Task.FromResult<bool>(true);
                }

                return Task.FromResult<bool>(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromResult<bool>(false);
            }
        }

        public override Task<IList<ApplicationUser>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.GetUsersInRoleAsync(normalizedRoleName, cancellationToken);
        }







        /*************************************************************/
        /*************************************************************/
        /*************************************************************/
        // Getovani slouzi k ziskani hodnot z objektu, nikoliv z DB !!!!!

        //kdyz getovani zakomentuju tak nefunguje prihlasovani

        /*************************************************************/
        /*************************************************************/
        /*************************************************************/


        public override Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            string mail = user.Email;

            return Task.FromResult<string>(mail);

        }

        public override Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {

            //DODELAT

            bool emailconf = user.EmailConfirmed;

            emailconf = true;

            return Task.FromResult<bool>(emailconf);

        }

        public override Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            throw new NotImplementedException();
        }

        public override Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            throw new NotImplementedException();
        }

        public override Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            string psswd = user.PasswordHash;

            return Task.FromResult<string>(psswd);
        }

        public override Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            string result = "";

            int id = user.Id;
            if (id == 0)
            {
                var userFromDB = FindByNameAsync(user.NormalizedUserName, cancellationToken).Result;
                if (userFromDB != null)
                    result = userFromDB.Id.ToString();
            }
            else
            {
                result = id.ToString();
            }

            return Task.FromResult<string>(result);

        }

        public override Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            string username = user.UserName;

            return Task.FromResult<string>(username);

        }

        public override Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            //DODELAT
            return Task.FromResult<bool>(true);
        }




        /*************************************************************/
        /*************************************************************/
        /*************************************************************/
        // Setovani slouzi k nasetovani hodnot do objektu, nikoliv do DB !!!!!

        //kdyz setovani zakomentuju tak nefunguje zakladani uzivatelu

        /*************************************************************/
        /*************************************************************/
        /*************************************************************/

        public override Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {

            //1 - EF core
            //user.Email = email;
            //_context.Update(user);
            //_context.SaveChanges();

            //2 - ADO
            //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //par1.Value = user.Id;
            //var par2 = new SqlParameter("Email", System.Data.SqlDbType.NVarChar, 256);
            //par2.Value = email;

            //_context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_Email 
            //        @Id
            //        ,@Email"
            //        ,
            //         par1,
            //         par2);

            //3 - set to user
            user.Email = email;

            return Task.CompletedTask;
        }

        public override Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {

            //1 - EF core
            //user.EmailConfirmed = confirmed;
            //_context.Update(user);
            //_context.SaveChanges();

            //2 - ADO
            //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //par1.Value = user.Id;
            //var par2 = new SqlParameter("EmailConfirmed", System.Data.SqlDbType.Bit);
            //par2.Value = confirmed;

            //_context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_EmailConfirmed
            //        @Id
            //        ,@EmailConfirmed"
            //        ,
            //         par1,
            //         par2);

            //3 - set to user
            user.EmailConfirmed = confirmed;

            return Task.CompletedTask;
        }

        public override Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {

            //1 - EF core
            //user.NormalizedEmail = normalizedEmail;
            //_context.Update(user);
            ////tady se nema ukladat - proc ? tak co se s tim ma delat ?
            ////_context.SaveChanges();

            //2 - ADO
            //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //par1.Value = user.Id;
            //var par2 = new SqlParameter("NormalizedEmail", System.Data.SqlDbType.NVarChar, 256);
            //par2.Value = normalizedEmail;

            //_context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_NormalizedEmail
            //        @Id
            //        ,@NormalizedEmail"
            //        ,
            //         par1,
            //         par2);

            //3 - set to user
            user.NormalizedEmail = normalizedEmail;

            return Task.CompletedTask;
        }

        public override Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {

            //1 - EF core
            //user.NormalizedUserName = normalizedName;
            //_context.Update(user);
            ////tady se nema ukladat - proc ? tak co se s tim ma delat ?
            ////_context.SaveChanges();

            //2 - ADO
            //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //par1.Value = user.Id;
            //var par2 = new SqlParameter("NormalizedUserName", System.Data.SqlDbType.NVarChar, 256);
            //par2.Value = normalizedName;

            //_context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_NormalizedUserName
            //        @Id
            //        ,@NormalizedUserName"
            //        ,
            //         par1,
            //         par2);

            //3 - set to user
            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Nastaveni hesla
        /// NEJEDNA SE O ULOZENI HESLA DO DB !!!!!!!!
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {

            //1 - EF core
            ////ulozeni hesla do DB
            //user.PasswordHash = passwordHash;
            //_context.Users.Update(user);
            ////tady se nema ukladat - proc ? tak co se s tim ma delat ?
            ////_context.SaveChanges();

            //2 - ADO
            //    var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //    par1.Value = user.Id;
            //    var par2 = new SqlParameter("PasswordHash", System.Data.SqlDbType.NVarChar, -1);
            //    par2.Value = passwordHash;

            //    _context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_PasswordHash
            //        @Id
            //        ,@PasswordHash"
            //            ,
            //             par1,
            //             par2);

            //3 - set to user
            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public override Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            //1 - EF core
            //user.UserName = userName;
            //_context.Users.Update(user);
            //_context.SaveChanges();

            //2 - ADO
            //var par1 = new SqlParameter("Id", System.Data.SqlDbType.Int);
            //par1.Value = user.Id;
            //var par2 = new SqlParameter("UserName", System.Data.SqlDbType.NVarChar, 256);
            //par2.Value = userName;

            //_context.Database.ExecuteSqlCommand(@"ap_lego_UpdApplicationUser_UserName
            //        @Id
            //        ,@UserName"
            //        ,
            //         par1,
            //         par2);

            //3 - set to user
            user.UserName = userName;

            return Task.CompletedTask;
        }










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
