using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Core.Module.Abstraction
{
    public class CodetableEntity : Entity
    {
        [StringLength(100)]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}




