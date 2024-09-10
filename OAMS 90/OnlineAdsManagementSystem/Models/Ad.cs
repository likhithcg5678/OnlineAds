using System;
using System.Collections.Generic;

namespace OnlineAdsManagementSystem.Models
{
    public partial class Ad
    {
        public Ad()
        {
            AdImages = new HashSet<AdImage>();
            Interests = new HashSet<Interest>();
        }

        public int AdId { get; set; }
        public int UserId { get; set; }
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string AdTitle { get; set; } = null!;
        public string AdDescription { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual MainCategory MainCategory { get; set; } = null!;
        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AdImage> AdImages { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
