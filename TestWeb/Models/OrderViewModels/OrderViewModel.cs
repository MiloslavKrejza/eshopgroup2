using Eshop2.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trainee.Business.DAL.Entities;
using Trainee.Core.DAL.Entities;

namespace Eshop2.Models.OrderViewModels
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Prosím vyplňte e-mail.")]
        [EmailAddress(ErrorMessage = "Prosím vyplňte validní e-mail.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }


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

        public int PaymentId { get; set; }
        public int ShippingId { get; set; }

        public List<Payment> Payment { get; set; }
        public List<Shipping> Shipping { get; set; }
        public List<CartItem> Items { get; set; }
        public List<Country> Countries { get; set; }
        


        

    }
}
