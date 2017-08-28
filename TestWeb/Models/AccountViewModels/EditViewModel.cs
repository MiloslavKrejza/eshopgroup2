using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Trainee.Core.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestWeb.ViewModels.Validations;

namespace TestWeb.Models.AccountViewModels
{
    /// <summary>
    /// This ViewModel provides all important data to display on the Edit View page
    /// </summary>
    public class EditViewModel : BaseViewModel
    {
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Prosím vyplňte své jméno.")]
        public string Name { get; set; }

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

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Stát")]
        public int CountryId { get; set; }

        [IsImage(ErrorMessage = "Zvolený soubor není obrázek.")]
        public IFormFile ProfileImage { get; set; }

        public Country Country { get; set; }

        public List<Country> Countries { get; set; }

        [Display(Name = "Nové heslo")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Heslo musí mít alespoň {2} a nejvýše {1} znaků.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        
        [Display(Name = "Potvrdit nové heslo")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hesla musí být shodná.")]
        public string ConfirmNewPassword { get; set; }

        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Prosím vyplňte heslo.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
