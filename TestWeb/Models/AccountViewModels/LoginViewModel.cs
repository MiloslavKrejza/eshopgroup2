using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWeb.Models.AccountViewModels
{
    /// <summary>
    /// This ViewModel provides all important data to display on the Login View page
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Display(Name = "Zapamatovat přihlášení?")]
        public bool RememberMe { get; set; }
    }
}
