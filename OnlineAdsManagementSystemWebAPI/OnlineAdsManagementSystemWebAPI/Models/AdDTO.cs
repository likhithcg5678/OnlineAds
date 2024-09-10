using System.Text.Json.Serialization;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public class AdDTO
    {
        
        public int AdId { get; set; }
        public int UserId { get; set; }
        public string Mcname { get; set; }
        public string Scname { get; set; }
        public string AdTitle { get; set; }
        public string AdDescription { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
		public string ImageUrl { get; set; }
	}
}
