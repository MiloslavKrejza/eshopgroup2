using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Catalogue.DAL.Entities
{
    public class AuthorBook
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        //Referenced properties
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
