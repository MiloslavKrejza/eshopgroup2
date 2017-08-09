using Alza.Core.Identity.Dal.Entities;
using Eshop2.Abstraction;
using Eshop2.Models.CatalogueViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Trainee.Business.Business;
using Trainee.Business.Business.Enums;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.Business;
using Trainee.Catalogue.DAL.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly BusinessService _businessService;
        private readonly CatalogueService _catalogueService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CatalogueController(BusinessService service, CatalogueService catService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _businessService = service;
            _catalogueService = catService;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        

        // GET: /Catalogue/Book/BookId
        [HttpGet("/Catalogue/Book/{id?}")]
        public IActionResult Book(int? id)
        {
            try
            {

                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                var dto = _businessService.GetProduct(id.Value);
                if (!dto.isOK || dto.isEmpty)
                {
                    return RedirectToAction("Error", "Home");
                }

                ProductBO product = dto.data;

                BookViewModel model = new BookViewModel
                {
                    Name = product.Name,

                    CategoryName = product.Category.Name,

                    Authors = product.Book.AuthorsBooks.Select(ab => ab.Author).ToList(),
                    ProductFormat = product.Format.Name,
                    AverageRating = product.AverageRating,
                    Annotation = product.Book.Annotation,
                    ProductText = product.Text,
                    PicAddress = product.PicAddress,
                    ProductId = product.Id,

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
        // GET: /Catalogue/Book/BookId
        [HttpPost("/Catalogue/Book/{id}")]
        public IActionResult Book(int? id, BookViewModel model)
        {
            try
            {
                if (!_signInManager.IsSignedIn(User))
                    return RedirectToAction("Login", "Account", $"~/Catalogue/Book/{id}");

                
                _businessService.GetProduct(id.Value);
                model.ProductId = id.Value;



                Review review = new Review
                {
                    Date = DateTime.Now,
                    ProductId = model.ProductId,
                    Rating = model.NewRating,
                    Text = model.ReviewText,
                    UserId = _userManager.GetUserAsync(User).Id
                };

                if (_businessService.GetReview(review.UserId, review.ProductId).isEmpty)
                {
                    _businessService.AddReview(review);
                }
                else
                {
                    //todo remove this
                    _businessService.UpdateReview(review);
                }
                return View(model);
            }
            catch(Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }
        }



        // GET: /Catalogue/Category
        [HttpGet("/Catalogue/Products/{id?}")]
        public IActionResult Products(int? id, ProductsViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    if (id == null)
                    {
                        id = 1;
                    }
                    int catId = id.Value;

                    model.currentCategory = _catalogueService.GetCategory(catId).data;

                    if(model.currentCategory == null)
                    {
                        return RedirectToAction("Error", "Home");
                    }

                    QueryParametersWrapper parameters = new QueryParametersWrapper
                    {
                        PageNum = model.PageNum,
                        CategoryId = catId, //check this

                        Authors = new List<int>(model.AuthorsFilter),

                        Formats = new List<int>(model.FormatsFilter),
                        Languages = new List<int>(model.LanguagesFilter),
                        MaxPrice = model.MaxPrice,
                        MinPrice = model.MinPrice,
                        PageSize = model.PageSize,
                        SortingParameter = model.SortingParameter,
                        SortingType = model.SortingType,

                        Publishers = new List<int>(model.PublishersFilter)

                    };
                    


                    var dto = _businessService.GetPage(parameters);
                    if (!dto.isOK)
                    {
                        return RedirectToAction("Error", "Home");
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
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }

        }
    }
}
