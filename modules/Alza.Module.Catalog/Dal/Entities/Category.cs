using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Module.Catalog.Dal.Entities
{
    public class Category : Entity
    {
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string SEOName { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string MainColor { get; set; }
        [StringLength(50)]
        public string BackgroundColor { get; set; }


        public string MainImage { get; set; }
        public string LogoImage { get; set; }
        public string TopBackgroundImage { get; set; }
        public string BottomBackgroundImage { get; set; }

        //VAZBY
        public int? ParentId { get; set; }

        //NAVIGATION
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }


    
}
