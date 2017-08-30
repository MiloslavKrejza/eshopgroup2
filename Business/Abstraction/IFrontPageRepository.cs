using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IFrontPageRepository
    {
        /// <summary>
        /// Gets an FrontPageItem
        /// </summary>
        /// <param name="id">FrontPage item template id</param>
        FrontPageItem GetFrontPageItem(int id);

        /// <summary>
        /// Gets all FrontPageItems
        /// </summary>
        /// <returns>IQueryable of FrontPageItem</returns>
        IQueryable<FrontPageItem> GetFrontPageItems();

        /// <summary>
        /// Gets FrontPageSlots containing current FrontPageItems
        /// </summary>
        /// <returns>IQueryable of Page Slots</returns>
        IQueryable<FrontPageSlot> GetSlotItems();

        /// <summary>
        /// Adds a new FrontPageItem
        /// </summary>
        /// <param name="itemId">New FrontPageItem</param>
        /// <returns>Added FrontPageItem</returns> 
        FrontPageItem AddFrontPageItem(FrontPageItem item);

        /// <summary>
        /// Updates an FrontPageItem with new data
        /// </summary>
        /// <param name="item">FrontPageItem data to update</param>
        /// <returns>Updated FrontPageItem</returns>
        FrontPageItem UpdateFrontPageItem(FrontPageItem item);

        /// <summary>
        /// Deletes an FrontPageItem with specified FrontPageItemId
        /// </summary>
        /// <param name="id">Delete Frontpage template item</param>
        void DeleteFrontPageItem(int id);
    }
}
