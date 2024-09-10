using System;
using System.Collections.Generic;

namespace OnlineAdsManagementSystem.Models
{
    public partial class Interest
    {
        public int InterestId { get; set; }
        public int AdId { get; set; }
        public int UserId { get; set; }
        public string InterestMessage { get; set; } = null!;
        public DateTime? Timestamp { get; set; }

        public virtual Ad Ad { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
