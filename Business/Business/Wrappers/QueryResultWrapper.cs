
using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.Business.Wrappers
{
    class QueryResultWrapper
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Language> Languages { get; set; }
        public int ResultCount { get; set; }
        public List<RatedProductBO> Products { get; set; }
        public List<Format> Formats { get; set; }
    }
}
 