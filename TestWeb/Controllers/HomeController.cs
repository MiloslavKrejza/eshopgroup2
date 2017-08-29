using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eshop2.Models.HomeViewModels;
using Trainee.Business.Business;
using Trainee.Business.DAL.Entities;
using TestWeb.Models.HomeViewModels;

namespace TestWeb.Controllers
{
    /// <summary>
    /// This is a home controller providing basic data
    /// </summary>
    public class HomeController : Controller
    {
        private readonly OrderService _orderService;
        private readonly BusinessService _businessService;

        public HomeController(OrderService ordServ, BusinessService busServ)
        {
            _orderService = ordServ;
            _businessService = busServ;
        }

        public IActionResult Index()
        {
            var result = _businessService.GetActivePageItems();
            List<FrontPageItem> items;
            items = result.isOK ? result.data : new List<FrontPageItem>();
            IndexViewModel model = new IndexViewModel { FrontPageItems = items };
            return View(model);
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

            var resultShipping = _orderService.GetShippings();
            if (!resultShipping.isOK)
                return RedirectToAction("Error");
            model.Shipping = resultShipping.isEmpty ? new List<Shipping>() : resultShipping.data;

            var resultPayment = _orderService.GetPayments();
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
