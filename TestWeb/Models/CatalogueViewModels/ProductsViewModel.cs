﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;

namespace Eshop2.Models.CatalogueViewModels
{
    public class ProductsViewModel
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Language> Languages { get; set; }
        public int ResultCount { get; set; }
        public List<ProductBO> Products { get; set; }
        public List<Format> Formats { get; set; }
    }
}
