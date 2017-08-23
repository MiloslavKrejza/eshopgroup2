using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eshop2.Models.HomeViewModels;
using Trainee.Business.Business;
using Trainee.Business.DAL.Entities;

namespace TestWeb.Controllers
{
    /// <summary>
    /// This is a home controller providing basic data
    /// </summary>
    public class HomeController : Controller
    {
        private readonly BusinessService _businessService;

        public HomeController(BusinessService busSer)
        {
            _businessService = busSer;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Conditions()
        {
            return View();
        }

        public IActionResult Shipping()
        {

            ShippingViewModel model = new ShippingViewModel();

            var resultShipping = _businessService.GetShippings();
            if (!resultShipping.isOK)
                return RedirectToAction("Error");
            model.Shipping = resultShipping.isEmpty ? new List<Shipping>() : resultShipping.data;

            var resultPayment = _businessService.GetPayments();
            if (!resultPayment.isOK)
                return RedirectToAction("Error");
            model.Payment = resultPayment.isEmpty ? new List<Payment>() : resultPayment.data;

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
