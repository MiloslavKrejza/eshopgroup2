﻿using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.Business.Enums;

namespace Trainee.Business.Business.Wrappers
{
    public class QueryParametersWrapper
    {
        //ToDo comment this pls

        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int> Authors { get; set; }
        public List<int> Publishers { get; set; }
        public List<int> Formats { get; set; }
        public List<int> Languages { get; set; }
        public SortType SortingType { get; set; }
        public SortingParameter SortingParameter { get; set; }
    }
}
