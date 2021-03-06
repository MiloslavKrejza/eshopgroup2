﻿using Eshop2.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Core.DAL.Entities;

namespace TestWeb.Models.AccountViewModels
{
    /// <summary>
    /// This ViewModel provides all important data to display on the Register View page
    /// </summary>
	public class RegisterViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Prosím vyplňte e-mail.")]
        [EmailAddress(ErrorMessage ="Prosím vyplňte validní e-mail.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Heslo")]
        [Required(ErrorMessage = "Prosím vyplňte heslo.")]
        [StringLength(100, ErrorMessage = "Heslo musí mít alespoň {2} a nejvýše {1} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Potvrďte heslo")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hesla musí být shodná.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Prosím vyplňte své jméno.")]
        public string Name { get; set; }

        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Prosím vyplňte své příjmení.")]
        public string Surname { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Prosím vyplňte telefonní číslo.")]
        public string Phone { get; set; }

        [Display(Name = "Ulice")]
        public string Street { get; set; }

        [Display(Name = "PSČ")]
        [MaxLength(50, ErrorMessage = "Prosím vyplňte PSČ.")]
        public string PostalCode { get; set; }

        [Display(Name = "Město")]
        public string City { get; set; }

        [Display(Name = "Stát")]
        [Required(ErrorMessage = "Prosím vyberte zemi.")]
        public int CountryId { get; set; }


        [IsTrue(ErrorMessage = "Prosím potvrďte souhlas s uložením údajů.")]
        public bool PersonalAgreement { get; set; }

        public List<Country> Countries { get; set; }

    }

}
