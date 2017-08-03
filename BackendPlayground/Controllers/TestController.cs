using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendPlayground.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Trainee.Catalogue.Abstraction;
using BackendPlayground.ViewModels.TestViewModels;

namespace BackendPlayground.Controllers
{
    public class TestController : Controller
    {
        IHostingEnvironment _env;
        ICategoryRepository _rep;
        public TestController(IHostingEnvironment env, ICategoryRepository categoryRep)
        {
            _env = env;
            _rep = categoryRep;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FileUp()
        {
            var model = new FileUp();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> FileUp(FileUp model)
        {
            if (ModelState.IsValid)
            {
                using (var stream = new FileStream(_env.WebRootPath + "/" + model.File.FileName, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

            }
            return View(model);

        }
        [HttpGet]
        public IActionResult CheckBoxTest()
        {
            return View();
        }
        //[HttpGet("test/product/{name}")]
        //public IActionResult
        
        [HttpGet]
        public IActionResult CategoryTest()
        {
            var result = _rep.GetCategory(1);
            CategoryTestViewModel model = new CategoryTestViewModel { Category = result };
            return View(model);
        }


    }
}