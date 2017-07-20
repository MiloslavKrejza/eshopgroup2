using Alza.Core.Identity.Dal.Entities;
//using Alza.Core.Module.Http;
using TestWeb.Models;
using TestWeb.Models.AccountViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeb.Controllers;
using Alza.Core.Module.Http;
using TestWeb.Abstraction;

namespace TestWeb.Controllers
{
    public class AccountController : Controller
    {
        private IHostingEnvironment _env;
        private ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private LegoUserService _legoUserService;
        //private LegoGamingService _legoGamingService;

        public AccountController(
            IHostingEnvironment env,
            ILogger<AccountController> logger,
            IStringLocalizer<AccountController> localizerizer,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _env = env;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [Route("/Account/Login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                    _signInManager.SignOutAsync();


                ViewData["ReturnUrl"] = returnUrl;

                return View("Login");
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [Route("/Account/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                    return ErrorActionResult("Uživatel již je přihlášen");


                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {

                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {

                        return RedirectToLocal(returnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(2, "someString");
                        ModelState.AddModelError("UserName", "someString");
                        return View("Lockout");
                    }
                    else
                    {

                        var isExist = await _userManager.FindByEmailAsync(model.Email);
                        if (isExist == null)
                        {
                            _logger.LogWarning(2, "someString");
                            ModelState.AddModelError("UserName", "someString");
                        }
                        else
                        {
                            _logger.LogWarning(2, "someString");
                            ModelState.AddModelError("Password", "someString");
                        }


                        return View(model);
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [Route("/Account/Register")]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                    _signInManager.SignOutAsync();


                ViewData["ReturnUrl"] = returnUrl;

                return View("Register");
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }




        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("/Account/Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                {
                    //await _signInManager.SignOutAsync();
                    return ErrorActionResult("Uživatel již je přihlášen");
                }

                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };






                    //Kontrola jestli uzivatel uz neexistuje
                    if (String.IsNullOrEmpty(user.NormalizedUserName))
                        user.NormalizedUserName = user.UserName;
                    var exist = await _userManager.GetUserIdAsync(user);

                    if (exist != "")
                        return RedirectToAction("Uzivatel existuje");




                    //osetreni username

                    string usernameTemp = model.Email.Split('@')[0];

                    user.UserName = usernameTemp;


                    //Create AspNet Identity User
                    IdentityResult res = await _userManager.CreateAsync(user, model.Password);
                    IdentityResult res2 = null;
                    AlzaAdminDTO res3 = null;
                    if (res.Succeeded)
                    {
                        //zjisteni ulozeneho Id uzivatele
                        var resId = await _userManager.GetUserIdAsync(user);
                        user.Id = Int32.Parse(resId);

                        //prirazeni uzivatele do Role
                        res2 = await _userManager.AddToRoleAsync(user, "User");

                        if (res2.Succeeded)
                        {

                            return View(model);

                        }

                    }
                    else
                    {
                        return RedirectToAction("CHYBA");
                    }



                    await _signInManager.SignInAsync(user, isPersistent: true);
                    


                    //??
                    return RedirectToAction("Forbidden");
                }



                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }


        //
        // GET: /Account/Forbidden
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            try
            {
                //OSTATNI
                //var user = _userManager.GetUserAsync(User).Result;
                //if (user == null)
                //    return ErrorActionResult("user == null");


                //_logger.LogError("Forbidden - userId = " + user.Id);
                //_logger.LogError(user.EmailConfirmed.ToString());
                //_logger.LogError(user.NormalizedUserName.ToString());
                //_logger.LogError(user.LockoutEnd.ToString());


                //var userRole = _userManager.GetRolesAsync(user);

                //foreach (var item in userRole.Result)
                //{
                //    _logger.LogError(item);

                //}


                return View();
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }



        //
        // GET: /Account/ResendConfirmationEmail
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<AlzaAdminDTO> ResendConfirmationEmail()
        //{
        //    try
        //    {
        //        var user = _userManager.GetUserAsync(User).Result;
        //        if (user != null)
        //        {
        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

        //            //precteni tela mailu ze souboru
        //            var pathToEmail = _env.WebRootPath
        //                                + Path.DirectorySeparatorChar.ToString()
        //                                + "emailTemplates"
        //                                + Path.DirectorySeparatorChar.ToString()
        //                                + "registration.html";

        //            string emailBody = "";

        //            using (StreamReader SourceReader = new StreamReader(pathToEmail))
        //            {
        //                emailBody = await SourceReader.ReadToEndAsync();
        //            }

        //            emailBody = emailBody.Replace("{confirmRegistrationURL}", callbackUrl);
        //            var serverName = "http://" + Request.Host;
        //            emailBody = emailBody.Replace("{ServerName}", serverName);


        //            await _emailSender.SendEmailAsync(user.Email, _localizerizer["ConfirmEmailTitle"], emailBody);

        //            return AlzaAdminDTO.True;
        //        }

        //        return AlzaAdminDTO.False;
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionDTO(e);
        //    }
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }










        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public AlzaAdminDTO ChangePassword([FromBody] ChangePasswordViewModel model)
        //{
        //    try
        //    {
        //        //VALIDACE MODELU
        //        if (!ModelState.IsValid)
        //            return InvalidModel();


        //        //OSTATNI
        //        var user = _userManager.GetUserAsync(User).Result;
        //        if (user == null)
        //            return ErrorDTO("user == null");


        //        //CHANGE PASSWORD
        //        var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;
        //        if (!result.Succeeded)
        //            return InvalidIdentityResultDTO(result);


        //        _signInManager.SignInAsync(user, isPersistent: false);

        //        return AlzaAdminDTO.True;
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionDTO(e);
        //    }
        //}




















        //// GET: /Account/ConfirmEmail
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    try
        //    {
        //        if (userId == null)
        //            return ErrorActionResult("userId == null");

        //        if (code == null)
        //            return ErrorActionResult("code == null");


        //        var user = await _userManager.FindByIdAsync(userId);

        //        if (user == null)
        //            return ErrorActionResult("user == null");


        //        var result = await _userManager.ConfirmEmailAsync(user, code);

        //        if (!result.Succeeded)
        //        {

        //            if (result.Errors.First().Code == "InvalidToken")
        //            {
        //                return RedirectToAction("InvalidCode", "Error");
        //            }

        //            return InvalidIdentityResultActionResult(result);
        //        }



        //        return View("ConfirmEmail");
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}

        ////
        //// GET: /Account/ForgotPassword
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ForgotPassword()
        //{
        //    try
        //    {
        //        /* if it is ajax call we want partial */
        //        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        //        {
        //            return PartialView("_ForgotPasswordPartial");
        //        }

        //        return View("ForgotPassword");
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}


        ////
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    try
        //    {

        //        if (ModelState.IsValid)
        //        {
        //            var user = await _userManager.FindByNameAsync(model.Nickname);
        //            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //            {
        //                // Don't reveal that the user does not exist or is not confirmed
        //                return View("ForgotPasswordConfirmation");
        //            }

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //            // Send an email with this link
        //            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //            await _emailSender.SendEmailAsync(user.Email, "Reset Password",
        //               $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // If we got this far, something failed, redisplay form
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}

        ////
        //// GET: /Account/ForgotPasswordConfirmation
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ForgotPasswordConfirmation()
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}

        ////
        //// GET: /Account/ResetPassword
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string code = null)
        //{
        //    try
        //    {
        //        if (code == null)
        //            return ErrorActionResult("code == null");

        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}

        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }
        //        var user = await _userManager.FindByNameAsync(model.Nickname);
        //        if (user == null)
        //        {
        //            // Don't reveal that the user does not exist
        //            return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        //        }
        //        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        //        if (!result.Succeeded)
        //            return InvalidIdentityResultActionResult(result);

        //        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}

        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPasswordConfirmation()
        //{
        //    try
        //    {
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionActionResult(e);
        //    }
        //}



















        #region Helpers




        public void ErrorToModel(AlzaAdminDTO dto, LegoViewModel model)
        {
            model.ErrorNo = dto.errorNo;
            foreach (var item in dto.errors)
            {
                model.Errors.Add(item);
            }
        }
        public AlzaAdminDTO InvalidModel()
        {
            AlzaAdminDTO invalidresult = AlzaAdminDTO.False;
            foreach (var item in ModelState.ToList())
            {

                foreach (var item2 in item.Value.Errors)
                {
                    invalidresult.errors.Add(item.Key + " - " + item2.ErrorMessage);
                }

            }
            return invalidresult;
        }














        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO InvalidIdentityResultDTO(IdentityResult result)
        {
            Guid errNo = Guid.NewGuid();
            StringBuilder res = new StringBuilder();

            foreach (var error in result.Errors)
            {
                res.AppendLine(error.Description);
            }

            _logger.LogError(errNo + " - " + res.ToString());
            return AlzaAdminDTO.Error(errNo, res.ToString());

        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RedirectToActionResult InvalidIdentityResultActionResult(IdentityResult result)
        {
            Guid errNo = Guid.NewGuid();
            StringBuilder res = new StringBuilder();

            foreach (var error in result.Errors)
            {
                res.AppendLine(error.Description);
            }

            _logger.LogError(errNo + " - " + res.ToString());


            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(res.ToString());

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO ErrorDTO(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + text);
            return AlzaAdminDTO.Error(errNo, "someString");
        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IActionResult ErrorActionResult(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + text);

            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(text);



            /*************************************************************************/
            //BUG Notification

            //var userId = _userManager.GetUserId(User);
            //if (String.IsNullOrEmpty(userId))
            //    userId = "0";

            //var bug = new BugNotification
            //{
            //    UserProfileId = Int32.Parse(userId),
            //    Severity = "Error",
            //    ErrorNo = errNo,
            //    CreatedDate = DateTime.Now,
            //    Note = text
            //};
            //_mediator.PublishAsync(bug);

            /*************************************************************************/



            //FINALni varianta Custom ActionResult
            return new AlzaActionResult("someString", model);

        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RedirectToActionResult ErrorActionResult(AlzaAdminDTO err)
        {
            _logger.LogError(err.errorNo + " - " + err.errorText);

            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = err.errorNo;
            model.Errors.Add(err.errorText);

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO ExceptionDTO(Exception e)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + e.Message + Environment.NewLine + e.StackTrace);
            return AlzaAdminDTO.Error(errNo, e.Message + Environment.NewLine + e.StackTrace);
        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IActionResult ExceptionActionResult(Exception e)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + e.Message + Environment.NewLine + e.StackTrace);


            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(e.Message + Environment.NewLine + e.StackTrace);



            /*************************************************************************/
            //BUG Notification

            //var userId = _userManager.GetUserId(User);
            //if (String.IsNullOrEmpty(userId))
            //    userId = "0";

            //var bug = new BugNotification
            //{
            //    UserProfileId = Int32.Parse(userId),
            //    Severity = "Critical",
            //    ErrorNo = errNo,
            //    CreatedDate = DateTime.Now,
            //    Note = e.Message + Environment.NewLine + e.StackTrace
            //};
            //_mediator.PublishAsync(bug);

            /*************************************************************************/



            return new AlzaActionResult("someString", model);

        }



        #endregion









        //Odstranit
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}
