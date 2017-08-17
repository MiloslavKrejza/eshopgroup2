using System.Linq;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface ICartItemRepository
    {
        /// <summary>
        /// Gets a Cart
        /// </summary>
        /// <param name="visitorId">Visitor identifier</param>
        /// <param name="productId">Product identifier</param>
        /// <returns>Cart by id</returns>
        CartItem GetCartItem(string visitorId, int productId);

        /// <summary>
        /// Gets all Carts
        /// </summary>
        /// <returns>IQueryable of Cart</returns>
        IQueryable<CartItem> GetCartItems();

        /// <summary>
        /// Adds a new Cart
        /// </summary>
        /// <param name="cartItem">New Cart Item</param>
        /// <returns>Added Cart</returns> 
        CartItem AddCartItem(CartItem cartItem);

        /// <summary>
        /// Updates a Cart with new data
        /// </summary>
        /// <param name="cartItem">Cart data to update</param>
        /// <returns>Updated Cart</returns>
        CartItem UpdateCartItem(CartItem cartItem);

        /// <summary>
        /// Deletes a Cart with specified CartId
        /// </summary>
        /// <param name="visitorId">Visitor identifier</param>
        /// <param name="productId">Product identifier</param>
        void DeleteCartItem(string visitorId, int productId);
    }
}
