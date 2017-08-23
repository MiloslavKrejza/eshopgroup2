using Alza.Core.Module.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Trainee.Business.Abstraction;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.Abstraction;
using Trainee.Catalogue.DAL.Entities;
using Trainee.User.Abstraction;

namespace Trainee.Business.Business
{
    /// <summary>
    /// This service provides access to products with reviews and ratings included
    /// </summary>
    public class BusinessService
    {
        private readonly ICategoryRelationshipRepository _categoryRelationshipRepository;
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingRepository _shippingRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IFilteringRepository _filteringRepository;

        public BusinessService(ICategoryRelationshipRepository catRRep, IProductRatingRepository prodRRep,
                IReviewRepository reviewRep, IProductRepository prodRep, ICategoryRepository catRep, IUserProfileRepository userRep, ICartItemRepository cartRep, IOrderRepository orderRep,
                IShippingRepository shipRep, IPaymentRepository payRep, IOrderItemRepository orderItemRep, IFilteringRepository filterRep)
        {
            _categoryRelationshipRepository = catRRep;
            _productRatingRepository = prodRRep;
            _reviewRepository = reviewRep;
            _productRepository = prodRep;
            _categoryRepository = catRep;
            _userProfileRepository = userRep;
            _cartItemRepository = cartRep;
            _orderRepository = orderRep;
            _paymentRepository = payRep;
            _shippingRepository = shipRep;
            _orderItemRepository = orderItemRep;
            _filteringRepository = filterRep;

        }
        #region Products


        public AlzaAdminDTO<QueryResultWrapper> GetPageADO(QueryParametersWrapper parameters)
        {

            return AlzaAdminDTO<QueryResultWrapper>.Data(_filteringRepository.FilterProducts(parameters));

            /*catch (Exception e)
            {
                throw;
                return AlzaAdminDTO<QueryResultWrapper>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }*/
        }
        /// <summary>
        /// Gets a page of product with applied filtering
        /// </summary>
        /// <param name="parameters">Filter parameters</param>
        /// <returns>Page of products</returns>
        public AlzaAdminDTO<QueryResultWrapper> GetPage(QueryParametersWrapper parameters)
        {


            try
            {
                //get all children of the specified category
                var childCategoriesId = _categoryRelationshipRepository.GetAllRelationships().Where(c => c.Id == parameters.CategoryId).Select(c => c.ChildId);

                //get all products
                IQueryable<ProductBase> query = _productRepository.GetAllProducts();

                //return only products which belong in these categories
                query = query.Where(p => childCategoriesId.Contains(p.CategoryId));

                decimal minPrice = decimal.MaxValue;
                decimal maxPrice = 0;
                QueryResultWrapper result = new QueryResultWrapper();
                HashSet<Language> languages = new HashSet<Language>();
                HashSet<Publisher> publishers = new HashSet<Publisher>();
                HashSet<Format> formats = new HashSet<Format>();
                HashSet<Author> authors = new HashSet<Author>();

                //specifying filter options correspondingly
                foreach (ProductBase product in query)
                {
                    minPrice = product.Price < minPrice ? product.Price : minPrice;
                    maxPrice = product.Price > maxPrice ? product.Price : maxPrice;
                    languages.Add(product.Language);
                    publishers.Add(product.Publisher);
                    formats.Add(product.Format);
                    foreach (Author author in product.Book.AuthorsBooks.Select(ab => ab.Author))
                    {
                        authors.Add(author);
                    }
                }

                result.MinPrice = minPrice;
                result.MaxPrice = maxPrice;

                result.Authors = authors.OrderBy(a => a.Surname).ToList();

                result.Languages = languages.OrderBy(l => l.Name).ToList();
                result.Publishers = publishers.OrderBy(p => p.Name).ToList();
                result.Formats = formats.OrderBy(f => f.Name).ToList();

                //filtering
                if (parameters.MinPrice != null)
                {
                    query = query.Where(p => p.Price >= parameters.MinPrice);
                }
                if (parameters.MaxPrice != null)
                {
                    query = query.Where(p => p.Price <= parameters.MaxPrice);
                }
                if (parameters.Languages != null)
                {
                    query = query.Where(p => parameters.Languages.Contains(p.LanguageId));
                }
                if (parameters.Publishers != null)
                {
                    query = query.Where(p => parameters.Publishers.Contains(p.PublisherId));
                }
                if (parameters.Formats != null)
                {
                    query = query.Where(p => parameters.Formats.Contains(p.FormatId));
                }
                if (parameters.Authors != null)
                {
                    query = query.Where(p => p.Book.AuthorsBooks.Select(ab => ab.Author).Select(a => a.AuthorId).Intersect(parameters.Authors).Count() > 0);
                }


                //Add average ratings
                List<int> pIds = query.Select(p => p.Id).ToList();
                IQueryable<ProductRating> ratings = _productRatingRepository.GetRatings().Where(pr => pIds.Contains(pr.ProductId));
                var products = query.Join(ratings, q => q.Id, r => r.ProductId, (p, r) => new { product = p, rating = r }).Select(x => new ProductBO(x.product, x.rating, null));




                //sort
                Func<ProductBO, IComparable> sortingParameter;
                switch (parameters.SortingParameter)
                {
                    case Enums.SortingParameter.Price:
                        sortingParameter = p => p.Price;
                        break;
                    case Enums.SortingParameter.Rating:
                        sortingParameter = p => p.AverageRating;
                        break;
                    case Enums.SortingParameter.Date:
                        sortingParameter = p => p.DateAdded;
                        break;
                    case Enums.SortingParameter.Name:
                        sortingParameter = p => p.Name;
                        break;
                    default:
                        sortingParameter = p => p.AverageRating;
                        break;
                }
                switch (parameters.SortingType)
                {
                    case Enums.SortType.Asc:
                        products = products.OrderBy(sortingParameter).AsQueryable();
                        break;
                    case Enums.SortType.Desc:
                        products = products.OrderByDescending(sortingParameter).AsQueryable();
                        break;
                    default:
                        break;
                }

                //return a "page"
                result.ResultCount = products.Count();
                products = products.Skip((parameters.PageNum - 1) * parameters.PageSize).Take(parameters.PageSize);
                result.Products = products.ToList();
                return AlzaAdminDTO<QueryResultWrapper>.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<QueryResultWrapper>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        /// <summary>
        /// Get a product with reviews and ratings
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>DTO of Product with Reviews and Average rating</returns>
        public AlzaAdminDTO<ProductBO> GetProduct(int id)
        {

            try
            {
                var baseProduct = _productRepository.GetProduct(id);

                if (ReferenceEquals(baseProduct, null))
                    return AlzaAdminDTO<ProductBO>.Data(null);
                //average rating of the product
                var avRating = _productRatingRepository.GetRating(id);

                //get product reviews and users who submitted the reviews
                var reviews = _reviewRepository.GetReviews().Where(r => r.ProductId == id).ToList();
                var users = _userProfileRepository.GetAllProfiles().Where(p => reviews.Select(r => r.UserId).Contains(p.Id));
                reviews = reviews.Join(users, r => r.UserId, p => p.Id, (r, p) => { r.User = p; return r; }).OrderBy(r => r.Date).ToList();
                var product = new ProductBO(baseProduct, avRating, reviews);

                return AlzaAdminDTO<ProductBO>.Data(product);
            }
            catch (Exception e)
            {

                return AlzaAdminDTO<ProductBO>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }

        }

        #endregion
        #region Reviews
        public AlzaAdminDTO<Review> AddReview(Review review)
        {
            try
            {
                return AlzaAdminDTO<Review>.Data(_reviewRepository.AddReview(review));
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Review>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO<Review> GetReview(int userId, int productId)
        {
            try
            {
                var result = _reviewRepository.GetReview(userId, productId);
                result.User = _userProfileRepository.GetProfile(result.UserId);
                return AlzaAdminDTO<Review>.Data(result);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Review>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO<Review> UpdateReview(Review review)
        {
            try
            {
                return AlzaAdminDTO<Review>.Data(_reviewRepository.UpdateReview(review));
            }
            catch (Exception e)
            {
                return AlzaAdminDTO<Review>.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public void DeleteReview(int userId, int productId)
        {
            _reviewRepository.DeleteReview(userId, productId);
        }
        #endregion
        #region Cart and Orders
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

        public AlzaAdminDTO<List<CartItem>> DeleteFromCart(string visitorId, int productId)
        {
            try
            {
                _cartItemRepository.DeleteCartItem(visitorId, productId);
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
                //might be even more slow-ish
                
                /*Stopwatch watch = new Stopwatch();
                watch.Start();*/

                var orders = _orderRepository.GetOrders().ToList();
                var orderItems = _orderItemRepository.GetOrderItems().Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId)).ToList();

                var pids = orderItems.Select(oi => oi.ProductId).ToList();


                var products = _productRepository.GetAllProducts().Where(p => pids.Contains(p.Id)).ToList();
                var ratings = _productRatingRepository.GetRatings().Where(r => products.Select(p => p.Id).Contains(r.ProductId));

                var prodsWithRating = products.Join(ratings, p => p.Id, r => r.ProductId, (p, r) => { return new ProductBO(p, r, null); });

                var itemsProducts = orderItems.Join(prodsWithRating, oi => oi.ProductId, p => p.Id, (oi, p) => { oi.Product = p; return oi; }).ToList();
                var orderWithItems = orders.Join(itemsProducts, o => o.Id, ip=> ip.OrderId, (o, ip) => { o.OrderItems.Add(ip); return o; }).ToList();

                /*watch.Stop();
                Debug.WriteLine($"GetUserOrders lasted {watch.Elapsed}");*/
                return AlzaAdminDTO<List<Order>>.Data(orderWithItems);
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
                        existingItem = _cartItemRepository.GetCartItems().FirstOrDefault(ci => (ci.UserId == userId) && (ci.ProductId == item.ProductId));
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
        #endregion
    }
}
