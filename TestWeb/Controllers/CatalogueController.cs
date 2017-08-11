using Alza.Core.Identity.Dal.Entities;
using Eshop2.Abstraction;
using Eshop2.Models.CatalogueViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainee.Business.Business;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.Business;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    /// <summary>
    /// This controller provides a list of products and book details
    /// </summary>
    public class CatalogueController : Controller
    {
        private readonly BusinessService _businessService;
        private readonly CatalogueService _catalogueService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BookLoader _bookLoader;

        public CatalogueController(BusinessService businessService, CatalogueService catService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _businessService = businessService;
            _catalogueService = catService;
            _signInManager = signInManager;
            _userManager = userManager;
            _bookLoader = new BookLoader(businessService);
        }



        // GET: /Catalogue/Book/BookId
        [HttpGet("/Catalogue/Book/{id?}")]
        public IActionResult Book(int? id)
        {
            try
            {
                //book id missing
                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }



                ViewData["productId"] = id;

                BookViewModel model = _bookLoader.LoadBookModel(id.Value);
                if(model == null)
                  return RedirectToAction("Error", "Home");
                

                //to be sure //Or handle it in view
                model.Reviews = model.Reviews == null ? new List<Review>() : model.Reviews;

                return View(model);
            }
            catch (Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }
        }
        // POST: /Catalogue/Book/BookId
        [HttpPost("/Catalogue/Book/{id}")]
        public async Task<IActionResult> Book(int? id, BookViewModel model)
        {
            try
            {
                //to add a review, user must be signed in
                if (!_signInManager.IsSignedIn(User))
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/Catalogue/Book/{id}" });

                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                
                if (_businessService.GetProduct(id.Value) == null)
                    return RedirectToAction("Error", "Home");

                model.ProductId = id.Value;
                


                var user = await _userManager.GetUserAsync(User);

                ViewData["productId"] = id;


                Review review = new Review
                {
                    Date = DateTime.Now,
                    ProductId = model.ProductId,
                    Rating = model.NewRating,
                    Text = model.ReviewText,
                    UserId = user.Id
                };

                //review can be added only if there is no other 
                if (_businessService.GetReview(review.UserId, review.ProductId).isEmpty)
                {
                    _businessService.AddReview(review);
                }

                model = _bookLoader.LoadBookModel(id.Value);


                return View(model);
            }
            catch (Exception e)
            {
                return AlzaError.ExceptionActionResult(e);
            }
        }



        // GET: /Catalogue/Category
        [HttpGet("/Catalogue/Products/{id?}/{pageNum?}")]
        public IActionResult Products(int? id, int? pageNum, ProductsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (id == null)
                    {
                        id = 1;
                    }
                    int catId = id.Value;

                    model.currentCategory = _catalogueService.GetCategory(catId).data;
                    

                    if (model.currentCategory == null)
                    {
                        return RedirectToAction("Error", "Home");
                    }

                    if(pageNum == null)
                    {
                        pageNum = 1;
                    }
                    model.PageNum = pageNum.Value;

                    QueryParametersWrapper parameters = new QueryParametersWrapper
                    {
                        PageNum = pageNum.Value,
                        CategoryId = catId, //check this


                        MaxPrice = model.MaxPrice,
                        MinPrice = model.MinPrice,
                        PageSize = model.PageSize,
                        SortingParameter = model.SortingParameter,
                        SortingType = model.SortingType,


                    };

                    parameters.Formats = model.FormatsFilter == null ? null : new List<int>(){model.FormatsFilter.Value};
                    parameters.Languages = model.LanguagesFilter == null ? null : new List<int>() { model.LanguagesFilter.Value };
                    parameters.Authors = model.AuthorsFilter == null ? null : new List<int>(){model.AuthorsFilter.Value};
                    parameters.Publishers = model.PublishersFilter == null ? null: new List<int>() { model.PublishersFilter.Value };





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
