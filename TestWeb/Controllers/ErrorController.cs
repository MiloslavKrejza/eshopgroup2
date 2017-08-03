using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alza.Core.Module.Http;
using TestWeb.Abstraction;
using TestWeb.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TestWeb.Controllers;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    public class ErrorController : Controller
    {

        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        public void ErrorToModel(AlzaAdminDTO<object> dto, BaseViewModel model)
        {
            model.ErrorNo = dto.errorNo;
            foreach (var item in dto.errors)
            {
                model.Errors.Add(item);
            }
        }
        public AlzaAdminDTO<object> InvalidModel()
        {
            AlzaAdminDTO<object> invalidresult = AlzaAdminDTO<object>.False;
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
        public AlzaAdminDTO<object> InvalidIdentityResultDTO(IdentityResult result)
        {
            Guid errNo = Guid.NewGuid();
            StringBuilder res = new StringBuilder();

            foreach (var error in result.Errors)
            {
                res.AppendLine(error.Description);
            }

            _logger.LogError(errNo + " - " + res.ToString());
            return AlzaAdminDTO<object>.Error(errNo, res.ToString());

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


            BaseViewModel model = new BaseViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(res.ToString());

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO<object> ErrorDTO(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + text);
            return AlzaAdminDTO<object>.Error(errNo, "someString");
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

            BaseViewModel model = new BaseViewModel();
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
        public RedirectToActionResult ErrorActionResult(AlzaAdminDTO<object> err)
        {
            _logger.LogError(err.errorNo + " - " + err.errorText);

            BaseViewModel model = new BaseViewModel();
            model.ErrorNo = err.errorNo;
            model.Errors.Add(err.errorText);

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO<object> ExceptionDTO(Exception e)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + e.Message + Environment.NewLine + e.StackTrace);
            return AlzaAdminDTO<object>.Error(errNo, e.Message + Environment.NewLine + e.StackTrace);
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


            BaseViewModel model = new BaseViewModel();
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


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
