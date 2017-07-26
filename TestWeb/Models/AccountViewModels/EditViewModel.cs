using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Models.AccountViewModels
{
    public class EditViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Prosím vyplňte e-mail.")]
        [EmailAddress(ErrorMessage = "Prosím vyplňte platný e-mail.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Street { get; set; }

        [MaxLength(50, ErrorMessage = "Prosím vyplňte validní PSČ bez mezer.")]
        public string ZIP { get; set; }

        public string City { get; set; }


        public string Country { get; set; }

    }
}
