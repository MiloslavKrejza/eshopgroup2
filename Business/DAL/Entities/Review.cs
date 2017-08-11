using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Trainee.User.DAL.Entities;

namespace Trainee.Business.DAL.Entities
{
    /// <summary>
    /// This class represents a review rated by a specific user
    /// </summary>
    public class Review
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        //Referenced properties
        [NotMapped]
        public UserProfile User { get; set; }
        //public ProductBO Product { get; set; }

    }
}
