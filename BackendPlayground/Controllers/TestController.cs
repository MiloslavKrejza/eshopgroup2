using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trainee.Core.Business;

namespace BackendPlayground.Controllers
{
    public class TestController : Controller
        
    {
        CountryService _countryService;
        public TestController(CountryService countryService)
        {
            _countryService = countryService;
        }
        public IActionResult Index()
        {
            ViewData["Countries"] = _countryService.GetAllCountries().data;
            return View();
        }
    }
}