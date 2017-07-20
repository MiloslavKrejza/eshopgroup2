using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Models
{
    public class LegoViewModel
    {
        public Guid ErrorNo { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
