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
        IQueryable<ProductBO> GetProducts(SortingParameter parameter, int count,int categoryId, int? timeOffset=null);
    }
}