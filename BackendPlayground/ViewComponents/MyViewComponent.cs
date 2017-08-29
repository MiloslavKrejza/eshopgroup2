using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendPlayground.ViewComponents
{
    public class MyViewComponent:ViewComponent
    { public async Task<IViewComponentResult> InvokeAsync(int id)
            
        {   
            return View("Default",id);
        }
    }
}
