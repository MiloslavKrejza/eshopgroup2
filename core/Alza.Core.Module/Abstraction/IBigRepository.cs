using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface IBigRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long>
    {
        
    }
}
