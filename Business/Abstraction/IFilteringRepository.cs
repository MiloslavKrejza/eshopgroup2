using Trainee.Business.Business.Wrappers;

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
    }
}