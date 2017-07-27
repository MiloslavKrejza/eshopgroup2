using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trainee.Core.DAL.Entities;

namespace TestWeb.Models.AccountViewModels
{
    public class DetailsViewModel
    {
        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Prosím vyplňte své jméno.")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Prosím vyplňte své příjmení.")]
        public string Surname { get; set; }

        [Display(Name = "Ulice")]
        public string Street { get; set; }

        [Display(Name = "PSČ")]
        [MaxLength(50, ErrorMessage = "Prosím vyplňte validní PSČ bez mezer.")]
        public string PostalCode { get; set; }

        [Display(Name = "Město")]
        public string City { get; set; }

        [Display(Name = "Stát")]
        public string CountryCode { get; set; }

        public Country Country { get; set; }

        public List<Country> Countries { get; set; }

        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Prosím vyplňte heslo.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
