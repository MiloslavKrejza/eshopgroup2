
using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.DAL.Entities;
using Trainee.Catalogue.DAL.Entities;

namespace Trainee.Business.Business.Wrappers
{
    /// <summary>
    /// This class is a DTO made for transport of filtering results
    /// </summary>
    public class QueryResultWrapper
    {
        /// <summary>
        /// Possible minimum price for this filter result
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// Possible maximum price for this filter result
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// Authors of the filtered books
        /// </summary>
        public List<Author> Authors { get; set; }
        /// <summary>
        /// Publishers of the filtered books
        /// </summary>
        public List<Publisher> Publishers { get; set; }
        /// <summary>
        /// Available languages for this filter result
        /// </summary>
        public List<Language> Languages { get; set; }
        /// <summary>
        /// Total result count, regardless the page size
        /// </summary>
        public int ResultCount { get; set; }
        /// <summary>
        /// A page of products
        /// </summary>
        public List<ProductBO> Products { get; set; }
        /// <summary>
        /// Available formats for this filter result
        /// </summary>
        public List<Format> Formats { get; set; }
    }
}
 