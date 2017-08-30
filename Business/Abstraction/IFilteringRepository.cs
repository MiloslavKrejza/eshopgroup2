using System.Linq;
using Trainee.Business.Business.Enums;
using Trainee.Business.Business.Wrappers;
using Trainee.Business.DAL.Entities;

namespace Trainee.Business.Abstraction
{
    public interface IFilteringRepository

    {
        /// <summary>
        /// Using provided QueryParametersWrapper filters products and gets all related infromation
        /// </summary>
        /// <param name="parameters">Wrapper that contains all parameters for filtering and ordering</param>
        /// <returns>Filtered products and related information</returns>
        QueryResultWrapper FilterProducts(QueryParametersWrapper parameters);
        /// <summary>
        /// Method that gets first n products from database sorted by specified column. 
        /// </summary>
        /// <param name="parameter">Column to sort by</param>
        /// <param name="type">Sorting order (ASC/DESC)</param>
        /// <param name="count">Number of products to fetch</param>
        /// <param name="categoryId">Category of fetched products</param>
        /// <param name="timeOffset">Maximal age (in days) of orders that are included in calculation of the amount of sold products</param>
        /// <returns>Requested products</returns>
        IQueryable<ProductBO> GetProducts(FrontPageParameter parameter,SortType type, int count,int categoryId, int? timeOffset=null);
    }
}