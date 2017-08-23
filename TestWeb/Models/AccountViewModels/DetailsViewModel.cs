using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trainee.Business.DAL.Entities;
using Trainee.Core.DAL.Entities;

namespace TestWeb.Models.AccountViewModels
{
    /// <summary>
    /// This ViewModel provides all important data to display on the Details View page
    /// </summary>
    public class DetailsViewModel
    {
        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Prosím vyplňte své jméno.")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

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
        //proc to vsechno ma validace, kdyz to je jen na ukazani?
        public string ProfilePicAddress { get; set; }

        public List<Order> Orders { get; set; }

    }
}
