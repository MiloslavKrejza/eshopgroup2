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
        public List<Book> Books { get; set; }
        //Overriden method of base class (object)
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            return Id == ((Author)obj).Id;
        }

    }
}
