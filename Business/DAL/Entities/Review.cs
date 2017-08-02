using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DAL.Entities
{
    public class Review
    {
        public int UserId { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
    }
}
