using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public abstract class BigEntity : IEntityWithTypedId<long>
    {
        [Key]
        public long Id
        {
            get; set;
        }

        //vhodne :
        //CreatedDate
        //LastChangeDate

    }
}
