using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> where T : IEntityWithTypedId<int>
    {
    }
}
