using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Core.DAL.Entities;

namespace Trainee.Catalogue.DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CountryId { get; set; }
        //Referenced properties
        public Country Country { get; set; }
        public List<AuthorBook> AuthorsBooks { get; set; }

    }
}
