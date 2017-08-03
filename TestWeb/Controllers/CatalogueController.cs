using Eshop2.Models.CatalogueViewModels;
using Microsoft.AspNetCore.Mvc;
using Trainee.Business.Business;
using Trainee.Business.Business.Wrappers;
using Trainee.Catalogue.DAL.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly BusinessService _businessService;

        public CatalogueController(BusinessService service)
        {
            _businessService = service;
        }

        // GET: /Catalogue/Book/BookId
        [HttpGet("/Catalogue/Book/{Id}")]
        public IActionResult Book(int? id)
        {
            if(id == null)
            {
                //OR redirect to action "Missing product" (probably better)
                ViewData["MissingProduct"] = true;
                return View();
            }
            var dto = _businessService.GetProduct(id.Value);
            if(!dto.isOK)
            {
                //Unknown id, redirect?
            }

          

            BookViewModel model = new BookViewModel { };

            return View(model);
        }

        // GET: /Catalogue/Cathegory
        [HttpGet]
        public IActionResult Category(int? categoryId)
        {
            QueryParametersWrapper parameters = new QueryParametersWrapper { };

            var dto = _businessService.GetPage(parameters);
            if(!dto.isOK)
            {
                //Another error TBD
            }

            QueryResultWrapper result = (QueryResultWrapper)dto.data;

            //Fill the ViewModel

            return View();
        }
    }
}
