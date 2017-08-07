using Eshop2.Abstraction;
using Eshop2.Models.CatalogueViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Trainee.Business.Business;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
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
        [HttpGet("/Catalogue/Book/{id}")]
        public IActionResult Book(int? id)
        {
            try
            {

                if (id == null)
                {
                    //OR redirect to action "Missing product" (probably better)
                    ViewData["MissingProduct"] = true;
                    return View();
                }
                var dto = _businessService.GetProduct(id.Value);
                if (!dto.isOK)
                {
                    //Unknown id, redirect?
                }

                ProductBO product = dto.data;

                BookViewModel model = new BookViewModel
                {
                    Name = product.Name,

                    CategoryName = product.Category.Name,

                    Authors = product.Book.AuthorsBooks.Select(ab => ab.Author).ToList(),
                    ProductFormat = product.Format.Name,
                    Rating = product.AverageRating,
                    Annotation = product.Book.Annotation,
                    ProductText = product.Text,
                    PicAddress = product.PicAddress,

                    State = product.State.Name,
                    Price = product.Price,
                    Language = product.Language.Name,

                    PageCount = product.PageCount,
                    Year = product.Year,
                    Publisher = product.Publisher.Name,
                    ISBN = product.ISBN,
                    EAN = product.EAN,

                    Reviews = product.Reviews

                };

                //to be sure //Or handle it in view
                model.Reviews = model.Reviews == null ? new List<Review>() : model.Reviews;
                
                return View(model);
            }
            catch (Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }
        }

        // GET: /Catalogue/Category
        [HttpGet("/Catalogue/Products/{categoryId}")]
        public IActionResult Products(int? categoryId, ProductsViewModel model)
        {
            try
            {
                if (categoryId == null)
                {
                    //categoryId = ...
                    //it means all products OR error .?
                }
                int catId = categoryId.Value;


                QueryParametersWrapper parameters = new QueryParametersWrapper
                {
                    PageNum = model.PageNum,
                    CategoryId = catId, //check this
                    //Authors = model.AuthorsFilter,
                    Formats = model.FormatsFilter,
                    Languages = model.LanguagesFilter,
                    MaxPrice = model.MaxPrice,
                    MinPrice = model.MinPrice,
                    PageSize = model.PageSize,
                    //Publishers = model.PublishersFilter,
                    SortingParameter = model.SortingParameter,
                    SortingType = model.SortingType
                };
                

                

                var dto = _businessService.GetPage(parameters);
                if (!dto.isOK)
                {
                    //Another error TBD (bad parameters, bad)
                }

                QueryResultWrapper result = dto.data;

                //Fill the ViewModel with new data
                
                model.MinPrice = result.MinPrice;
                model.MaxPrice = result.MaxPrice;
                model.Authors = result.Authors;
                model.Formats = result.Formats;
                model.Languages = result.Languages;
                model.Products = result.Products;
                model.Publishers = result.Publishers;
                model.ResultCount = result.ResultCount;


                return View(model);
            }
            catch (Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }

        }
    }
}
