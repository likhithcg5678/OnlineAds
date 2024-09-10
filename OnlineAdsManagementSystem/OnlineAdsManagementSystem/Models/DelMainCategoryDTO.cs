using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public class DelMainCategoryDTO
    {
        [Key]
        public int Mcid { get; set; }
        public string Mcname { get; set; } = null!;
    }
}
