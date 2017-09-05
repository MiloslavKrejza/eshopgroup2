using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.Business.Enums;

namespace Trainee.Business.Business.Wrappers
{
    /// <summary>
    /// This class is a DTO made for filter parameters
    /// </summary>
    public class QueryParametersWrapper
    {

        /// <summary>
        /// Page number
        /// </summary>
        public int PageNum { get; set; }
        /// <summary>
        /// Products per page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Product category
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Minimum product price
        /// </summary>
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// Maximum product price
        /// </summary>
        public decimal? MaxPrice { get; set; }
        /// <summary>
        /// Chosen product authors
        /// </summary>
        public List<int> Authors { get; set; }
        /// <summary>
        /// Chosen publishers
        /// </summary>
        public List<int> Publishers { get; set; }
        /// <summary>
        /// Chosen formats
        /// </summary>
        public List<int> Formats { get; set; }
        /// <summary>
        /// Chosen languages
        /// </summary>
        public List<int> Languages { get; set; }
        /// <summary>
        /// Ascending or descending
        /// </summary>
        public SortType SortingType { get; set; }
        public SortingParameter SortingParameter { get; set; }
    }
}
