using System;
using System.Collections.Generic;

namespace OnlineAdsManagementSystem.Models
{
    public partial class AdImage
    {
        public int ImageId { get; set; }
        public int AdId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual Ad Ad { get; set; } = null!;
    }
}
