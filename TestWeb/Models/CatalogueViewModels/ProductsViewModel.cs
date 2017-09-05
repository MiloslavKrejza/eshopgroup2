using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business.Enums;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;

namespace Eshop2.Models.CatalogueViewModels
{
    /// <summary>
    /// This ViewModel provides all important data to display on the Products View page
    /// </summary>
    public class ProductsViewModel
    {
        //Filter results
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Language> Languages { get; set; }
        public int ResultCount { get; set; }
        public List<ProductBO> Products { get; set; }
        public List<Format> Formats { get; set; }


        //Current category
        public Category currentCategory { get; set; }

        

        //Filter parameters
        public int? PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 20; 
        public decimal? MinPriceFilter { get; set; }
        public decimal? MaxPriceFilter { get; set; }
        public int? AuthorsFilter { get; set; }
        public int? PublishersFilter { get; set; }

        public int? FormatsFilter { get; set; }
        public int? LanguagesFilter { get; set; }

        public SortType? SortingType { get; set; }
        public SortingParameter SortingParameter { get; set; }
    }
}
