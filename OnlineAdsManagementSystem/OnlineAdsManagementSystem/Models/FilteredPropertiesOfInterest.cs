using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
	

		public class FilteredPropertiesOfInterest
		{
			[Key]
			public int interestId { get; set; }
			public int adId { get; set; }
			public int userId { get; set; }
			public string interestMessage { get; set; }
			 public string ImageUrl { get; set; } = null!;
        public int AdId { get; set; }
        public DateTime timestamp { get; set; }
        
    }

	
}
