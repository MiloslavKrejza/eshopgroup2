using System;
using System.Collections.Generic;
using System.Text;

namespace User.DAL.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ProfileStateId { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ProfilePicAddress { get; set; }
        //Referenced properties
        public ProfileState ProfileState { get; set; }
        public Country Country { get; set; }
    }
}
