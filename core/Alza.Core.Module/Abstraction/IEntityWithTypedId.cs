using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public interface IEntityWithTypedId<TId>
    {
        [Key]
        TId Id { get; }
    }
   
}
