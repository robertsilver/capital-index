using System.ComponentModel.DataAnnotations;

namespace capital_index.Models.Enums
{
    public enum Countries
    {
        [Display(Name = "EU")]
        EU = 0,
        [Display(Name = "Rest of the World")]
        ROW = 1,
        [Display(Name = "United Kingdom")]
        UnitedKingdom = 2,
        [Display(Name = "Australia")]
        Australia = 3,
        [Display(Name = "South Africa")]
        SouthAfrica = 4
    }
}