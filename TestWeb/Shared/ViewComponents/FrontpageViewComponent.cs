using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business;
using Trainee.Business.Business.Enums;

namespace Eshop2.ViewComponents
{
    public class FrontpageViewComponent : ViewComponent
    {
        BusinessService _service;
        public FrontpageViewComponent(BusinessService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = _service.GetFrontPage(SortingParameter.Rating, SortType.Desc, 4, 1);
            var data = res.data;
            return View(data);
            
        }
    }
}
