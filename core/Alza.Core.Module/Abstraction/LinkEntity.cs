using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public abstract class LinkEntity : ILinkEntityWithTypedId<int, int>
    {
        public abstract int Id1 { get; set; }


        public abstract int Id2 { get; set; }
        
    }
}
