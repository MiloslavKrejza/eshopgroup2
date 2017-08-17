using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Eshop2.Models.OrderViewModels;
using Trainee.Business.Business;
using Microsoft.AspNetCore.Identity;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Http;
using Eshop2.Abstraction;
using Trainee.Business.DAL.Entities;
using Trainee.Core.Business;
using Alza.Core.Module.Http;
using Newtonsoft.Json;

using Trainee.Core.DAL.Entities;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eshop2.Controllers
{
    public class OrderController : Controller
    {
        private readonly BusinessService _businessService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly CountryService _countryService;

        public OrderController(BusinessService businessService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor,
            CountryService countryService)
        {
            _businessService = businessService;
            _signInManager = signInManager;
            _userManager = userManager;
            _accessor = accessor;
            _countryService = countryService;
        }

        // GET: /Order/Cart/
        public async Task<IActionResult> Cart()
        {
            try
            {
                //user tried to order, but the cart is empty
                ViewData["emptyCart"] = TempData["emptyCart"];


                CookieHelper cookieHelper = new CookieHelper(_accessor);

                AlzaAdminDTO<List<CartItem>> result;
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    result = _businessService.GetCart(user.Id);
                }
                else
                {
                    string cookieId = cookieHelper.GetVisitorId();
                    result = _businessService.GetCart(cookieId);
                    
                }


                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                var cart = result.isEmpty ? new List<CartItem>() : result.data;


                CartViewModel model = new CartViewModel() { Cart = cart };

                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public async Task<IActionResult> Redirect()
        {
            try
            {
                CookieHelper cookieHelper = new CookieHelper(_accessor);

                AlzaAdminDTO<List<CartItem>> result;
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    result = _businessService.GetCart(user.Id);
                }
                else
                {
                    string cookieId = cookieHelper.GetVisitorId();
                    result = _businessService.GetCart(cookieId);
                }
                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                var cart = result.data;

                if (cart.Count == 0)
                {
                    TempData["emptyCart"] = true;
                    return RedirectToAction("Cart");
                }

                OrderViewModel model = new OrderViewModel();

                model.Items = cart;


                return RedirectToAction("Order");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: /Order/Order/
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            try
            {
                CookieHelper cookieHelper = new CookieHelper(_accessor);

                AlzaAdminDTO<List<CartItem>> result;
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    result = _businessService.GetCart(user.Id);
                }
                else
                {
                    string cookieId = cookieHelper.GetVisitorId();
                    result = _businessService.GetCart(cookieId);
                }
                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                var cart = result.data;

                if (cart.Count == 0)
                {
                    TempData["emptyCart"] = true;
                    return RedirectToAction("Cart");
                }

                OrderViewModel model = new OrderViewModel();

                model.Items = cart;

                if (model.Items.Count > 0)
                {
                    model.Countries = _countryService.GetAllCountries().data.ToList();
                    model.Shipping = _businessService.GetShippings().data.ToList();
                    model.Payment = _businessService.GetPayments().data.ToList();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost("/Order/Order/")]
        public async Task<IActionResult> SendOrder(OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CookieHelper cookieHelper = new CookieHelper(_accessor);
                    string cookieId = cookieHelper.GetVisitorId();

                    Order order = new Order()
                    {
                        Address = model.Street,
                        City = model.City,
                        Name = model.Name,
                        Surname = model.Surname,
                        PaymentId = model.PaymentId,
                        ShippingId = model.ShippingId,
                        PostalCode = model.PostalCode,
                        PhoneNumber = model.Phone,
                        CountryId = model.CountryId
                    };
                    if (_signInManager.IsSignedIn(User))
                    {
                        var result = await _userManager.GetUserAsync(User);
                        order.UserId = result.Id;
                    }

                    //ToDo delete the correct cart
                    var addedOrder = _businessService.AddOrder(order, cookieId).data;
                    int orderId = addedOrder.Id;

                    foreach (var item in model.Items)
                    {
                        OrderItem orderItem = new OrderItem()
                        {
                            OrderId = orderId,
                            Amount = item.Amount,
                            Price = item.Product.Price,
                            ProductId = item.ProductId
                        };
                        _businessService.AddOrderItem(orderItem);
                    }

                    return RedirectToAction("OKPage");
                }
                else
                {
                    throw new Exception("Data missing");
                }

            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: /Order/Summary/
        public IActionResult Summary()
        {
            return View();
        }

        // GET: /Order/OKPage/
        public IActionResult OKPage()
        {
            return View();
        }

        // GET: /Order/OrderLogin/
        public IActionResult OrderLogin()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddToCart([FromBody]AddToCartModel model)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            CookieHelper helper = new CookieHelper(_accessor);
            string cookieId = helper.GetVisitorId();
            int? uid = null;
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                uid = user.Id;
            }

            var result = _businessService.AddToCart(cookieId, uid, model.ProductId, model.Amount);

            return Json(result,settings);

        }
    }
}
