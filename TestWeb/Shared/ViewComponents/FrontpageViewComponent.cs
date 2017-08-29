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
        public async Task<IViewComponentResult> InvokeAsync(SortingParameter parameter, SortType type, int count, int categoryId, int? timeOffSet = null)
        {
            var res = _service.GetFrontPage(parameter, type, count, categoryId, timeOffSet);
            var data = res.data;
            return View(data);

        }
    }
}
