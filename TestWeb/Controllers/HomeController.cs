using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestWeb.Controllers
{
    /// <summary>
    /// This is a home controller providing basic data
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Products", "Catalogue", new { id = 1, pageNum = 1 });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
