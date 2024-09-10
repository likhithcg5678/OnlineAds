using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    public class NewViewMyAds
    {
        [Key]
        public int adId { get; set; }
        public int userId { get; set; }
        public string mCname { get; set; }
        public string sCname { get; set; }
        public string adTitle { get; set; }
        public string adDescription { get; set; }
        public string contactName { get; set; }
        public string contactEmail { get; set; }
        public string phoneNumber { get; set; }
        public string status { get; set; }
    }
}
