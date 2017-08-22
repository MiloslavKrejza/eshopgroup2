using Trainee.Business.Business.Wrappers;

namespace Trainee.Business.Abstraction
{
    public interface IFilteringRepository
    {
        QueryResultWrapper FilterProducts(QueryParametersWrapper parameters);
    }
}