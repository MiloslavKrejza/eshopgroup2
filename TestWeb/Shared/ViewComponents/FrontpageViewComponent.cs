using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business;
using Trainee.Business.Business.Enums;
using Trainee.Business.DAL.Entities;

namespace Eshop2.ViewComponents
{
    public class FrontpageViewComponent : ViewComponent
    {
        BusinessService _service;
        public FrontpageViewComponent(BusinessService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync(FrontPageItem item)
        {
            SortingParameter param = (SortingParameter)Enum.Parse(typeof(SortingParameter), item.SortingParameter, true);
            SortType type = (SortType)Enum.Parse(typeof(SortType), item.SortType, true);
            
            var res = _service.GetFrontPage(param , type, item.Count, item.CategoryId, item.TimeOffSet);
            var data = res.data;
            return View(data);

        }
    }
}
