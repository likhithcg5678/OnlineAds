namespace OnlineAdsManagementSystemWebAPI.Models
{
	public class InterestDTO2
	{
		public int InterestId { get; set; }
		public int AdId { get; set; }
		public int UserId { get; set; }
		public string InterestMessage { get; set; } = null!;
		public DateTime? Timestamp { get; set; }
	}
}
