using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface ILinkRepository<T> : ILinkRepositoryWithTypedId<T, int,int> where T : ILinkEntityWithTypedId<int, int> 
    {
    }
}
