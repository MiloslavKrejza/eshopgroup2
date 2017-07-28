using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackendPlayground.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace BackendPlayground.Controllers
{
    public class TestController : Controller
    {
        IHostingEnvironment _env;
        public TestController(IHostingEnvironment env)
        {
            _env = env;
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
    }
}