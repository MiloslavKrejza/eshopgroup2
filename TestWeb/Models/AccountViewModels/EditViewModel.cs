using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Core.DAL.Entities;

namespace TestWeb.Models.AccountViewModels
{
    public class EditViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Prosím vyplňte e-mail.")]
        [EmailAddress(ErrorMessage = "Prosím vyplňte platný e-mail.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Prosím vyplňte heslo.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Street { get; set; }

        [MaxLength(50, ErrorMessage = "Prosím vyplňte validní PSČ bez mezer.")]
        public string PostalCode { get; set; }

        public string City { get; set; }


        public string CountryCode { get; set; }

        public List<Country> Countries { get; set; }

    }
}
