using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Catalogue.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Annotation { get; set; }
        //Referenced properties
        public List<ProductBase> Products { get; set; }
        public List<AuthorBook> AuthorsBooks { get; set; }
    }
}
