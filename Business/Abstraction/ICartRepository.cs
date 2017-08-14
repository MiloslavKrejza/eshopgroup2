using System.Linq;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    interface ICartRepository
    {
        /// <summary>
        /// Gets a Cart
        /// </summary>
        /// <param name="cartId">Cart id</param>
        /// <returns>Cart by id</returns>
        Cart GetCart(int cartId);

        /// <summary>
        /// Gets all Carts
        /// </summary>
        /// <returns>IQueryable of Cart</returns>
        IQueryable<Cart> GetCarts();

        /// <summary>
        /// Adds a new Cart
        /// </summary>
        /// <param name="cartId">New Cart</param>
        /// <returns>Added Cart</returns> 
        Cart AddCart(Cart cart);

        /// <summary>
        /// Updates a Cart with new data
        /// </summary>
        /// <param name="cart">Cart data to update</param>
        /// <returns>Updated Cart</returns>
        Cart UpdateCart(Cart cart);

        /// <summary>
        /// Deletes a Cart with specified CartId
        /// </summary>
        /// <param name="cartId">Cart identifier</param>
        void DeleteCart(int cartId);
    }
}
