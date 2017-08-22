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
using Trainee.User.DAL.Entities;
using Trainee.User.Business;


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
        private readonly UserService _userService;

        public OrderController(BusinessService businessService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor,
            CountryService countryService, UserService userService)
        {
            _businessService = businessService;
            _signInManager = signInManager;
            _userManager = userManager;
            _accessor = accessor;
            _countryService = countryService;
            _userService = userService;
        }

        // GET: /Order/Cart/
        public async Task<IActionResult> Cart()
        {
            try
            {
                //user tried to order, but the cart is empty
                ViewData["emptyOrder"] = TempData["emptyOrder"];


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

                List<CartItem> cart;
                if(result.isEmpty)
                {
                    cart = new List<CartItem>();
                }
                else
                {
                    cart = result.data;
                }
               
                if(cart.Count == 0)
                {
                    ViewData["emptyCart"] = true;
                }

                CartViewModel model = new CartViewModel() { Cart = cart };

                return View(model);

            }
            catch (Exception )
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

                UserProfile userProfile = null;
                ApplicationUser user = null;
                if (_signInManager.IsSignedIn(User))
                {
                    user = await _userManager.GetUserAsync(User);
                    result = _businessService.GetCart(user.Id);
                    userProfile = _userService.GetUserProfile(user.Id).data;
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
                    TempData["emptyOrder"] = true;
                    return RedirectToAction("Cart");
                }

                OrderViewModel model = new OrderViewModel();

                model.Items = cart;

                if (model.Items.Count > 0)
                {
                    if (userProfile != null)
                    {
                        model.CountryId = userProfile.CountryId;
                        model.Email = user.Email;
                        model.City = userProfile.City;
                        model.Name = userProfile.Name;
                        model.Phone = userProfile.PhoneNumber;
                        model.PostalCode = userProfile.PostalCode;
                        model.Street = userProfile.Address;
                        model.Surname = userProfile.Surname;
                    }

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
        public async Task<IActionResult> Order(OrderViewModel model)
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


                    var items = _businessService.GetCart(cookieId).data;

                    if (items.Count == 0)
                    {
                        TempData["emptyOrder"] = true;
                        return RedirectToAction("Cart");
                    }


                    var addedOrder = _businessService.AddOrder(order, cookieId).data;
                    int orderId = addedOrder.Id;
                    
                    foreach (var item in items)
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

                    TempData["OrderId"] = orderId;
                    return RedirectToAction("OKPage");
                }
                else
                {
                    throw new Exception("Data missing");
                }

            }
            catch (Exception)
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
            ViewData["OrderId"] = TempData["OrderId"];
            if (ViewData["OrderId"] == null)
                return RedirectToAction("Error", "Home");

            return View();
        }

        // GET: /Order/OrderLogin/
        public IActionResult OrderLogin()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddToCart([FromBody]CartItemModel model)
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

            return Json(result, settings);

        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart([FromBody]RemoveFromCartModel item)
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
            var result = _businessService.RemoveCartItem(cookieId, item.Id);
            return Json(result, settings);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody]CartItemModel item)
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
            var result = _businessService.UpdateCartItem(cookieId, item.ProductId, item.Amount);
            return Json(result, settings);

        }
    }
}
