using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.Business
{
    class OrderService
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
        private readonly IOrderStateRepository _orderStateRepository;

        public OrderService(ICategoryRelationshipRepository catRRep, IProductRatingRepository prodRRep,
                IReviewRepository reviewRep, IProductRepository prodRep, ICategoryRepository catRep, IUserProfileRepository userRep, ICartItemRepository cartRep, IOrderRepository orderRep,
                IShippingRepository shipRep, IPaymentRepository payRep, IOrderItemRepository orderItemRep, IFilteringRepository filterRep, IOrderStateRepository ordStRep)
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
            _orderStateRepository = ordStRep;

        }
    }
}
