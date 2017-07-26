using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Core.DAL.Entities;

namespace TestWeb.Models.AccountViewModels
{
	public class RegisterViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Prosím vyplňte e-mail.")]
        [EmailAddress(ErrorMessage ="Prosím vyplňte validní e-mail.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Prosím vyplňte heslo.")]
        [StringLength(100, ErrorMessage = "Heslo musí mít alespoň {2} a nejvýše {1} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Hesla musí být shodná.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Prosím vyplňte své jméno.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prosím vyplňte své příjmení.")]
        public string Surname { get; set; }

        
        public string Street { get; set; }

        
        [MaxLength(50, ErrorMessage = "Prosím vyplňte validní PSČ bez mezer.")]
        public string PostalCode { get; set; }


        public string City { get; set; }


        public string CountryCode { get; set; }

        public List<Country> Countries { get; set; }

    }
}
