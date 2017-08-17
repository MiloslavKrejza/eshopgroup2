using System;
using System.Collections.Generic;
using System.Text;
using Trainee.Core.DAL.Entities;

namespace Trainee.Catalogue.DAL.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? CountryId { get; set; }
        //Referenced properties

        public List<AuthorBook> AuthorsBooks { get; set; }
        public Country Country { get; set; }
        //Overriden method of base class (object)
        public override int GetHashCode()
        {
            return AuthorId;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == GetType())
            {

                return AuthorId == ((Author)obj).AuthorId;

            }
            else return false;
        }

    }
}
