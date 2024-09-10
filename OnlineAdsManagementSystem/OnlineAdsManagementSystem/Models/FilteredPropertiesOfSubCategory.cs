using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    public class FilteredPropertiesOfSubCategory
    {
        [Key]
        public int Scid { get; set; }
        public int Mcid { get; set; }
        [Required]
        public string MCName { get; set; } = null!;
        public string Scname { get; set; } = null!;
        
    }
}
