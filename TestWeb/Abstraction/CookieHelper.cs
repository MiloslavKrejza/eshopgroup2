using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business;

namespace Eshop2.Abstraction
{
    public class CookieHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public CookieHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetVisitorId()
        {
            HttpContext context = _accessor.HttpContext;
            var cookieId = context.Request.Cookies["VisitorId"];

            if (cookieId == null)
            {
                cookieId = Guid.NewGuid().ToString();
                CookieOptions opt = new CookieOptions();
                opt.Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(14, 0, 0, 0));
                context.Response.Cookies.Append("VisitorId", cookieId, opt);
            }

            return cookieId;
        }

        public bool IsVisitorIsNull
        {
            get
            {
                return _accessor.HttpContext.Request.Cookies["VisitorId"] == null;
            }
        }

        public void SetVisitorId(string id)
        {
            _accessor.HttpContext.Response.Cookies.Append("VisitorId", id);
        }

        public void DeleteVisitorId()
        {
            string id = GetVisitorId();
            _accessor.HttpContext.Response.Cookies.Delete(id);
        }
    }
}
