using Alza.Core.Module.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface IRepositoryWithTypedId<T, in TId> where T : IEntityWithTypedId<TId>
    {
        IQueryable<T> Query();

        T Get(TId id);

        T Add(T entity);

        T Update(T entity);

        void Remove(TId id);
    }
}
