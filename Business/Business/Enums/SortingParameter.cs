using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainee.Business.Business.Enums
{
    public enum SortingParameter
    {
        [Display(Name = "Cena")]
        Price,
        [Display(Name = "Hodnocení")]
        Rating,
        [Display(Name = "Datum")]
        Date,
        [Display(Name = "Název")]
        Name
    }
}
