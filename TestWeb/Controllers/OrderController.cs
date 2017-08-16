using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    public class OrderController : Controller
    {
        // GET: /Order/Cart/
        public IActionResult Cart()
        {
            return View();
        }

        // GET: /Order/Order/
        public IActionResult Order()
        {
            return View();
        }

        // GET: /Order/Summary/
        public IActionResult Summary()
        {
            return View();
        }

        // GET: /Order/OKPage/
        public IActionResult OKPage()
        {
            return View();
        }

        // GET: /Order/OrderLogin/
        public IActionResult OrderLogin()
        {
            return View();
        }
    }
}
