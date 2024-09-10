using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    

        public class FilteredPropertiesOfMainCategory
        {
            [Key]
            public int mcid { get; set; }
        [Display(Name ="Main Category Name")]    
        public string mcname { get; set; }
        }

    
}
