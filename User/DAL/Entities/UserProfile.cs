using System.ComponentModel.DataAnnotations;

namespace User.DAL.Entities
{
    class UserProfile
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }

        [Key]
        public int ProfileStadeId { get; set; }
        public string PhoneNumber { get; set; }
        [Key]
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Address { get; set; }
        public string ProfilePicAddress { get; set; }


    }
}
