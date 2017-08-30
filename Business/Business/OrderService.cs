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

        private List<CartItem> GetCartItems(Expression<Func<CartItem, bool>> selector)
        {
            var cart = _cartItemRepository.GetCartItems().Where(selector).ToList();
            var products = _productRepository.GetAllProducts().Where(p => cart.Select(ci => ci.ProductId).Contains(p.Id));
            var ratings = _productRatingRepository.GetRatings().Where(pr => cart.Select(ci => ci.ProductId).Contains(pr.ProductId));
            var completeProducts = products.Join(ratings, p => p.Id, r => r.ProductId, (p, r) => new { product = p, rating = r }).Select(x => new ProductBO(x.product, x.rating, null));
            var completeCart = cart.Join(completeProducts, ci => ci.ProductId, p => p.Id, (ci, p) => { ci.Product = p; return ci; }).ToList();
            return completeCart;
        }

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
        public AlzaAdminDTO<List<CartItem>> TransformCart(string visitorId, int userId, bool delete)
        {
            try
            {

                if (delete)
                {

                    var oldCartItems = _cartItemRepository.GetCartItems().Where(ci => ci.UserId == userId).ToList();
                    foreach (var item in oldCartItems)
                    {
                        _cartItemRepository.DeleteCartItem(item.VisitorId, item.ProductId);
                    }
                }
                var currentCart = _cartItemRepository.GetCartItems().Where(ci => ci.VisitorId == visitorId).ToList();
                foreach (var item in currentCart)
                {
                    CartItem existingItem = null;
                    if (!delete)
                    {
                        existingItem = _cartItemRepository.GetCartItems().FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == item.ProductId);
                    }
                    if (existingItem != null)
                    {
                        existingItem.Amount += item.Amount;
                        _cartItemRepository.UpdateCartItem(existingItem);
                    }
                    else
                    {
                        item.VisitorId = userId.ToString();
                        item.UserId = userId;
                        _cartItemRepository.AddCartItem(item);

                    }
                    _cartItemRepository.DeleteCartItem(visitorId, item.ProductId);

                }
                var result = _cartItemRepository.GetCartItems().Where(ci => ci.UserId == userId).ToList();
                return AlzaAdminDTO<List<CartItem>>.Data(result);
            }
            catch (Exception e)
            {

                return AlzaAdminDTO<List<CartItem>>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
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
