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



namespace Eshop2.Controllers
{
    public class OrderController : Controller
    {
        private readonly BusinessService _businessService;
        private readonly OrderService _orderService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly CountryService _countryService;
        private readonly UserService _userService;

        public OrderController(BusinessService businessService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor,
            CountryService countryService, UserService userService, OrderService ordServ)
        {
            _businessService = businessService;
            _signInManager = signInManager;
            _userManager = userManager;
            _accessor = accessor;
            _countryService = countryService;
            _userService = userService;
            _orderService = ordServ;
        }

        // GET: /Order/Cart/
        public async Task<IActionResult> Cart()
        {
            try
            {
                //user tried to order, but the cart is empty
                ViewData["emptyOrder"] = TempData["emptyOrder"];

                //getting the visitor id
                CookieHelper cookieHelper = new CookieHelper(_accessor);

                AlzaAdminDTO<List<CartItem>> result;
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.GetUserAsync(User);
                    result = _orderService.GetCart(user.Id);
                }
                else
                {
                    string cookieId = cookieHelper.GetVisitorId();
                    result = _orderService.GetCart(cookieId);

                }


                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                //handling an empty cart
                List<CartItem> cart;
                if (result.isEmpty)
                {
                    cart = new List<CartItem>();
                }
                else
                {
                    cart = result.data;
                }

                if (cart.Count == 0)
                {
                    ViewData["emptyCart"] = true;
                }

                CartViewModel model = new CartViewModel() { Cart = cart };

                return View(model);

            }
            catch (Exception)
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
                //first getting the cart items
                CookieHelper cookieHelper = new CookieHelper(_accessor);

                AlzaAdminDTO<List<CartItem>> result;

                //anonymous shopping?
                UserProfile userProfile = null;
                ApplicationUser user = null;
                if (_signInManager.IsSignedIn(User))
                {
                    user = await _userManager.GetUserAsync(User);
                    result = _orderService.GetCart(user.Id);
                    userProfile = _userService.GetUserProfile(user.Id).data;
                }
                else
                {
                    string cookieId = cookieHelper.GetVisitorId();
                    result = _orderService.GetCart(cookieId);
                }
                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                var cart = result.data;

                //cannot order with empty cart
                if (cart.Count == 0)
                {
                    TempData["emptyOrder"] = true;
                    return RedirectToAction("Cart");
                }

                OrderViewModel model = new OrderViewModel();

                model.Items = cart;

                //cannot order with an empty cart
                if (model.Items.Count > 0)
                {
                    //the user data is auto-filled in
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

                    //listing all possible choices for fields
                    model.Countries = _countryService.GetAllCountries().data.ToList();
                    model.Shipping = _orderService.GetShippings().data.ToList();
                    model.Payment = _orderService.GetPayments().data.ToList();
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

        [HttpPost]
        public async Task<IActionResult> Order(OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CookieHelper helper = new CookieHelper(_accessor);

                    AlzaAdminDTO<List<CartItem>> result;
                    if (_signInManager.IsSignedIn(User))
                    {
                        var user = await _userManager.GetUserAsync(User);
                        result = _orderService.GetCart(user.Id);
                    }
                    else
                    {
                        string cookieId = helper.GetVisitorId();
                        result = _orderService.GetCart(cookieId);
                    }
                    if (!result.isOK)
                        throw new Exception("Could not find the cart");

                    var cart = result.data;

                    //cannot order with empty cart
                    if (cart.Count == 0)
                    {
                        TempData["emptyOrder"] = true;
                        return RedirectToAction("Cart");
                    }
                    model.Items = cart;


                    Country country = _countryService.GetCountry(model.CountryId).data;
                    Shipping shipping = _orderService.GetShipping(model.ShippingId).data;
                    Payment payment = _orderService.GetPayment(model.PaymentId).data;

                    model.Countries = new List<Country>() { country };
                    model.Shipping = new List<Shipping>() { shipping };
                    model.Payment = new List<Payment>() { payment };



                    return View("Summary", model);
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpPost("Order/BackToOrder")]
        public IActionResult BackToOrder(OrderViewModel model)
        {
            try
            {
                CookieHelper cookieHelper = new CookieHelper(_accessor);

                string cookieId = cookieHelper.GetVisitorId();
                var result = _orderService.GetCart(cookieId);
                if (!result.isOK)
                    throw new Exception("Could not find the cart");

                var cart = result.data;

                //cannot order with empty cart
                if (cart.Count == 0)
                {
                    TempData["emptyOrder"] = true;
                    return RedirectToAction("Cart");
                }

                model.Items = cart;
                model.Countries = _countryService.GetAllCountries().data.ToList();
                model.Shipping = _orderService.GetShippings().data.ToList();
                model.Payment = _orderService.GetPayments().data.ToList();

                return View("Order", model);

            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost("/Order/SendOrder")]
        public async Task<IActionResult> SendOrder(OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //sending the order
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
                        CountryId = model.CountryId,
                        Date = DateTime.Now
                    };


                    if (_signInManager.IsSignedIn(User))
                    {
                        var result = await _userManager.GetUserAsync(User);
                        order.UserId = result.Id;
                    }


                    var items = _orderService.GetCart(cookieId).data;

                    //cannot send empty order
                    if (items.Count == 0)
                    {
                        TempData["emptyOrder"] = true;
                        return RedirectToAction("Cart");
                    }

                    //adding order to the database
                    var addedOrder = _orderService.AddOrder(order, cookieId).data;
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
                        _orderService.AddOrderItem(orderItem);
                    }

                    //order id to be displayed in the view
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


        // GET: /Order/OKPage/
        public IActionResult OKPage()
        {
            //page confirming the order had been sent
            ViewData["OrderId"] = TempData["OrderId"];
            if (ViewData["OrderId"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // GET: /Order/OrderLogin/
        public IActionResult OrderLogin()
        {
            //page displayed between cart and order if user is not signed in
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Redirect");
            }
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

            var result = _orderService.AddToCart(cookieId, uid, model.ProductId, model.Amount);

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
            var result = _orderService.RemoveCartItem(cookieId, item.Id);
            return Json(result, settings);
        }

        [HttpGet]
        public IActionResult Redirect()
        {
            //redirect page leading to order
            return View();
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
            var result = _orderService.UpdateCartItem(cookieId, item.ProductId, item.Amount);
            return Json(result, settings);

        }


        [HttpPost]
        public async Task<IActionResult> MergeCarts([FromBody] MergeCartModel model)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var user = await _userManager.GetUserAsync(User);

            //cannot transfer cart if there is no user
            if (user == null)
                return RedirectToAction("Error", "Home");

            //getting the visitor id of the anonymous cart
            CookieHelper helper = new CookieHelper(_accessor);
            string oldVisitorId = helper.GetOldVisitorId();

            var result = _orderService.TransformCart(oldVisitorId, user.Id, model.DeleteOld);

            //the old cart has been transfered and deleted, deleting the old visitor id
            helper.DeleteOldVisitorId();

            return Json(result, settings);
        }
    }
}
