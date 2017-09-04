using System.ComponentModel.DataAnnotations;

namespace Trainee.Business.Business.Enums
{
    /// <summary>
    /// This class contains sorting parameters for products
    /// </summary>
    public enum SortingParameter
    {
        [Display(Name = "Data přidání")]
        Date,
        [Display(Name = "Ceny")]
        Price,
        [Display(Name = "Hodnocení")]
        Rating,
        [Display(Name = "Názvu")]
        Name
    }
}
