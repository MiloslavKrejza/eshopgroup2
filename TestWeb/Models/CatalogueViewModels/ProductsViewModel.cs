using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.Business.Enums;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;

namespace Eshop2.Models.CatalogueViewModels
{
    public class ProductsViewModel
    {
        //Filter results
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Language> Languages { get; set; }
        public int ResultCount { get; set; }
        public List<ProductBO> Products { get; set; }
        public List<Format> Formats { get; set; }

        //Current category
        public Category currentCategory { get; set; }

        //Filter parameters
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 24; //TODO ask FE
        public decimal? MinPriceFilter { get; set; }
        public decimal? MaxPriceFilter { get; set; }
        public int AuthorsFilter { get; set; }
        public int PublishersFilter { get; set; }
        public List<int> FormatsFilter { get; set; }
        public List<int> LanguagesFilter { get; set; }
        public SortType SortingType { get; set; } //TODO ask for defaults
        public SortingParameter SortingParameter { get; set; }
    }
}
