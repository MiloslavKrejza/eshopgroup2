using Alza.Core.Module.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Trainee.Business.Abstraction;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.Abstraction;

namespace Trainee.Business.Business
{
    /// <summary>
    /// This class provides access to user and visitor carts and orders
    /// </summary>
    public class OrderService
    {
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingRepository _shippingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderStateRepository _orderStateRepository;

        public OrderService(IProductRatingRepository prodRRep,
                IProductRepository prodRep, ICategoryRepository catRep, ICartItemRepository cartRep, IOrderRepository orderRep,
                IShippingRepository shipRep, IPaymentRepository payRep, IOrderItemRepository orderItemRep, IOrderStateRepository ordStRep)
        {
            _productRatingRepository = prodRRep;
            _productRepository = prodRep;
            _cartItemRepository = cartRep;
            _orderRepository = orderRep;
            _paymentRepository = payRep;
            _shippingRepository = shipRep;
            _orderItemRepository = orderItemRep;
            _orderStateRepository = ordStRep;

        }

        /// <summary>
        /// Returns a cart of a specified visitor
        /// </summary>
        /// <param name="visitorId">Id of the visitor</param>
        /// <returns>DTO of the cart</returns>
        public AlzaAdminDTO<List<CartItem>> GetCart(string visitorId)
        {
            try
            {
                Expression<Func<CartItem, bool>> selector = ci => ci.VisitorId == visitorId;
                List<CartItem> completeCart = GetCartItems(selector);

                return AlzaAdminDTO<List<CartItem>>.Data(completeCart);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        /// <summary>
        /// Gets a cart by user id
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>DTO of cart</returns>
        public AlzaAdminDTO<List<CartItem>> GetCart(int userId)
        {
            try
            {
                Expression<Func<CartItem, bool>> selector = ci => ci.UserId == userId;
                List<CartItem> completeCart = GetCartItems(selector);

                return AlzaAdminDTO<List<CartItem>>.Data(completeCart);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets a cart by specifing a condition
        /// </summary>
        /// <param name="selector">Cart item condition</param>
        /// <returns>A cart</returns>
        private List<CartItem> GetCartItems(Expression<Func<CartItem, bool>> selector)
        {
            var cart = _cartItemRepository.GetCartItems().Where(selector).ToList();
            var products = _productRepository.GetAllProducts().Where(p => cart.Select(ci => ci.ProductId).Contains(p.Id));
            var ratings = _productRatingRepository.GetRatings().Where(pr => cart.Select(ci => ci.ProductId).Contains(pr.ProductId));
            var completeProducts = products.Join(ratings, p => p.Id, r => r.ProductId, (p, r) => new { product = p, rating = r }).Select(x => new ProductBO(x.product, x.rating, null));
            var completeCart = cart.Join(completeProducts, ci => ci.ProductId, p => p.Id, (ci, p) => { ci.Product = p; return ci; }).ToList();
            return completeCart;
        }

        /// <summary>
        /// Adds an item to a cart
        /// </summary>
        /// <param name="visitorId">Id of the Visitor</param>
        /// <param name="userId">Id of the user</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="amount">Product amount</param>
        /// <returns>DTO of the cart</returns>
        public AlzaAdminDTO<List<CartItem>> AddToCart(string visitorId, int? userId, int productId, int amount = 1)
        {
            try
            {
                var currentCartItem = _cartItemRepository.GetCartItem(visitorId, productId);
                if (currentCartItem != null)
                {
                    currentCartItem.Amount += amount;
                    _cartItemRepository.UpdateCartItem(currentCartItem);
                }
                else
                {
                    var cartItem = new CartItem { VisitorId = visitorId, Amount = amount, ProductId = productId, UserId = userId };
                    _cartItemRepository.AddCartItem(cartItem);
                }

                return GetCart(visitorId);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Adds a new order to the database
        /// </summary>
        /// <param name="order">Order to be added</param>
        /// <param name="visitorId">Visitor id of the corresponding cart</param>
        /// <returns>DTO of the order</returns>
        public AlzaAdminDTO<Order> AddOrder(Order order, string visitorId)
        {
            try
            {
                //todo check tolist
                var cartProductId = _cartItemRepository.GetCartItems().Where(ci => ci.VisitorId == visitorId).Select(ci => ci.ProductId).ToList();

                order.StateId = 1; //default orderState is the first one
                var createdOrder = _orderRepository.AddOrder(order);

                foreach (var productId in cartProductId)
                {
                    _cartItemRepository.DeleteCartItem(visitorId, productId);
                }
                return AlzaAdminDTO<Order>.Data(createdOrder);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Order>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets an order by id
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <returns>DTO of the order</returns>
        public AlzaAdminDTO<Order> GetOrder(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrder(orderId);
                //Might be slow-ish
                var products = _productRepository.GetAllProducts().Where(p => order.OrderItems.Select(oi => oi.ProductId).Contains(p.Id)).Select(p => new ProductBO(p, null, null));
                order.OrderItems = order.OrderItems.Join(products, oi => oi.ProductId, p => p.Id, (oi, p) => { oi.Product = p; return oi; }).ToList();
                return AlzaAdminDTO<Order>.Data(order);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Order>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets all orders of a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Dto of all user orders</returns>
        public AlzaAdminDTO<List<Order>> GetUserOrders(int userId)
        {
            try
            {
                //might be even more slow-ish and should be maybe redone in sql (again)

                /*Stopwatch watch = new Stopwatch();
                watch.Start();*/

                var orders = _orderRepository.GetOrders().Where(o => o.UserId == userId).ToList();
                var orderItems = _orderItemRepository.GetOrderItems().Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId)).ToList();
                var orderStates = _orderStateRepository.GetOrderStates();


                var pids = orderItems.Select(oi => oi.ProductId).ToList();



                var products = _productRepository.GetAllProducts().Where(p => pids.Contains(p.Id)).ToList();
                var ratings = _productRatingRepository.GetRatings().Where(r => products.Select(p => p.Id).Contains(r.ProductId));

                var prodsWithRating = products.Join(ratings, p => p.Id, r => r.ProductId, (p, r) => { return new ProductBO(p, r, null); });

                var itemsProducts = orderItems.Join(prodsWithRating, oi => oi.ProductId, p => p.Id, (oi, p) => { oi.Product = p; return oi; }).ToList();
                //var orderWithItems = orders.Join(itemsProducts, o => o.Id, ip=> ip.OrderId, (o, ip) => { o.OrderItems.Add(ip); return o; }).ToList();
                orders = orders.Join(orderStates, o => o.StateId, os => os.Id, (o, os) => { o.OrderState = os; return o; }).OrderByDescending(o => o.Date).ToList();

                /*watch.Stop();
                Debug.WriteLine($"GetUserOrders lasted {watch.Elapsed}");*/
                return AlzaAdminDTO<List<Order>>.Data(orders);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<Order>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Adds a new order item
        /// </summary>
        /// <param name="item">Item to be added</param>
        /// <returns>DTO of the order item</returns>
        public AlzaAdminDTO<OrderItem> AddOrderItem(OrderItem item)
        {

            try
            {
                var orderItem = _orderItemRepository.AddOrderItem(item);
                return AlzaAdminDTO<OrderItem>.Data(orderItem);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<OrderItem>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets a shipping by id
        /// </summary>
        /// <param name="id">Shipping id</param>
        /// <returns>DTO of the shipping</returns>
        public AlzaAdminDTO<Shipping> GetShipping(int id)
        {
            try
            {
                var shipping = _shippingRepository.GetShipping(id);
                return AlzaAdminDTO<Shipping>.Data(shipping);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Shipping>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets a payment by id
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>DTO of the payment</returns>
        public AlzaAdminDTO<Payment> GetPayment(int id)
        {
            try
            {
                var payment = _paymentRepository.GetPayment(id);
                return AlzaAdminDTO<Payment>.Data(payment);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Payment>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets all shippings
        /// </summary>
        /// <returns>Shippings</returns>
        public AlzaAdminDTO<List<Shipping>> GetShippings()
        {
            try
            {
                var shippings = _shippingRepository.GetShippings().ToList();
                return AlzaAdminDTO<List<Shipping>>.Data(shippings);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<Shipping>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets all payments
        /// </summary>
        /// <returns>DTO of payments</returns>
        public AlzaAdminDTO<List<Payment>> GetPayments()
        {
            try
            {
                var payments = _paymentRepository.GetPayments().ToList();
                return AlzaAdminDTO<List<Payment>>.Data(payments);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<List<Payment>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Merges an anonymous cart with an existing user cart. If there are any items in the user cart, it is possible to delete them or leave them intact.
        /// </summary>
        /// <param name="visitorId">Id of the visitor who owns the cart.</param>
        /// <param name="userId">Id of the user who should receive the visitor cart</param>
        /// <param name="delete">Specifies whether the old user cart items should be deleted or not</param>
        /// <returns>DTO of the merged cart</returns>
        public AlzaAdminDTO<List<CartItem>> TransformCart(string visitorId, int userId, bool delete)
        {
            try
            {
                //the old items should be deleted
                if (delete)
                {

                    var oldCartItems = _cartItemRepository.GetCartItems().Where(ci => ci.UserId == userId).ToList();
                    foreach (var item in oldCartItems)
                    {
                        _cartItemRepository.DeleteCartItem(item.VisitorId, item.ProductId);
                    }
                }

                //gets the visitor cart
                var currentCart = _cartItemRepository.GetCartItems().Where(ci => ci.VisitorId == visitorId).ToList();
                foreach (var item in currentCart)
                {
                    CartItem existingItem = null;

                    //if we want to keep the items, the amount might need to be updated for the same product
                    if (!delete)
                    {
                        existingItem = _cartItemRepository.GetCartItems().FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == item.ProductId);
                    }
                    if (existingItem != null)
                    {
                        //adding the amounts
                        existingItem.Amount += item.Amount;
                        _cartItemRepository.UpdateCartItem(existingItem);
                    }
                    else
                    {
                        item.VisitorId = userId.ToString();
                        item.UserId = userId;
                        _cartItemRepository.AddCartItem(item);

                    }

                    //the old cart needs to be deleted afterwards
                    _cartItemRepository.DeleteCartItem(visitorId, item.ProductId);

                }

                //the new cart
                var result = _cartItemRepository.GetCartItems().Where(ci => ci.UserId == userId).ToList();
                return AlzaAdminDTO<List<CartItem>>.Data(result);
            }
            catch (Exception e)
            {

                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Updates the amount of a cart item
        /// </summary>
        /// <param name="visitorId">Visitor id of the item owner</param>
        /// <param name="productId">Product id</param>
        /// <param name="amount">New item amount</param>
        /// <returns></returns>
        public AlzaAdminDTO<List<CartItem>> UpdateCartItem(string visitorId, int productId, int amount)
        {
            try
            {
                var item = _cartItemRepository.GetCartItem(visitorId, productId);
                item.Amount = amount;
                _cartItemRepository.UpdateCartItem(item);
                var result = _cartItemRepository.GetCartItems().Where(ci => ci.VisitorId == visitorId).ToList();
                return AlzaAdminDTO<List<CartItem>>.Data(result);
            }
            catch (Exception e)
            {

                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        /// <summary>
        /// Deletes an item from the cart.
        /// </summary>
        /// <param name="visitorId">Id of the item owner</param>
        /// <param name="productId">Product id</param>
        /// <returns>DTO of the cart</returns>
        public AlzaAdminDTO<List<CartItem>> RemoveCartItem(string visitorId, int productId)
        {
            try
            {
                _cartItemRepository.DeleteCartItem(visitorId, productId);
                var result = _cartItemRepository.GetCartItems().Where(ci => ci.VisitorId == visitorId).ToList();
                return AlzaAdminDTO<List<CartItem>>.Data(result);
            }
            catch (Exception e)
            {

                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
