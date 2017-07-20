using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface ILinkEntityWithTypedId<TId1,TId2>
    {
        [Key]
        TId1 Id1 { get; }

        [Key]
        TId1 Id2 { get; }
    }
}
