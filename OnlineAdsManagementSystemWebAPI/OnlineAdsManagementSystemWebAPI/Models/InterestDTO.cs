namespace OnlineAdsManagementSystemWebAPI.Models
{
	public class InterestDTO
	{
		public int InterestId { get; set; }
		public int AdId { get; set; }
		public int UserId { get; set; }
		public string InterestMessage { get; set; } = null!;
		public DateTime? Timestamp { get; set; }
        public string ImageUrl { get; set; }

    }
}
