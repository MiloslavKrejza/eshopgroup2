using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface ILinkRepositoryWithTypedId<T, in TId1,TId2> where T : ILinkEntityWithTypedId<TId1,TId2>
    {
        IQueryable<T> Query();

        T Get(TId1 id1, TId2 id2);
        List<T> GetById1(TId1 id1);
        List<T> GetById2(TId2 id2);

        T Add(T entity);

        T Update(T entity);

        void Remove(TId1 id1, TId2 id2);
        void RemoveById1(TId1 id1);
        void RemoveById2(TId2 id2);
    }
}
