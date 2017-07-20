using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Entities
{
    public class ProductMedia
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        [Key]
        public int MediaId { get; set; }
        public Media Media { get; set; } = new Media();
    }
}
