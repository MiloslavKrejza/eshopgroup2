using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business;

namespace Eshop2.Abstraction
{
    /// <summary>
    /// Helper class for accessing visitor id
    /// </summary>
    public class CookieHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public CookieHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Gets the current visitor id, that is saved in the Cookie data. If there is no such Cookie, a new visitor id is generated
        /// </summary>
        /// <returns>Current or new visitor id string</returns>
        public string GetVisitorId()
        {
            HttpContext context = _accessor.HttpContext;
            var cookieId = context.Request.Cookies["VisitorId"];

            if (cookieId == null)
            {
                cookieId = Guid.NewGuid().ToString();
                CookieOptions opt = new CookieOptions();
                context.Response.Cookies.Append("VisitorId", cookieId, opt);
            }

            return cookieId;
        }

        public bool IsVisitorIdNull
        {
            get
            {
                return _accessor.HttpContext.Request.Cookies["VisitorId"] == null;
            }
        }

        /// <summary>
        /// Enables to set the visitor id.
        /// </summary>
        /// <param name="id">New visitor id</param>
        public void SetVisitorId(string id)
        {
            _accessor.HttpContext.Response.Cookies.Append("VisitorId", id);
        }

        /// <summary>
        /// Deletes the saved visitor id
        /// </summary>
        public void DeleteVisitorId()
        {
            _accessor.HttpContext.Response.Cookies.Delete("VisitorId");
        }

        /// <summary>
        /// Gets the temporary data of the old visitor id
        /// </summary>
        /// <returns>Saved visitor id</returns>
        public string GetOldVisitorId()
        {
            return _accessor.HttpContext.Request.Cookies["OldVisitorId"];
        }

        /// <summary>
        /// Enables to save a visitor id that would be deleted
        /// </summary>
        /// <param name="oldVisitorId">Visitor id that is to be saved</param>
        public void SetOldVisitorId(string oldVisitorId)
        {
            _accessor.HttpContext.Response.Cookies.Append("OldVisitorId", oldVisitorId);
        }

        /// <summary>
        /// Deletes the temporary visitor id data
        /// </summary>
        public void DeleteOldVisitorId()
        {
            _accessor.HttpContext.Response.Cookies.Delete("OldVisitorId");
        }
    }
}
