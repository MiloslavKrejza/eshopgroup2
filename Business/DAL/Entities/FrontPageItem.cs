using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Business.Business.Enums;

namespace Trainee.Business.DAL.Entities
{
    public class FrontPageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FrontPageParameter { get; set; }
        public string SortType { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public int? TimeOffSet { get; set; }
        public bool Active { get; set; }
    }
}
