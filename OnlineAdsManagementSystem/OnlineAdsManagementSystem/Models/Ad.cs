using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Mcname { get; set; } = null!;
        [Required]
        public string Scname { get; set; } = null!;
        [Required]
        public string AdTitle { get; set; } = null!;
        [Required]
        public string AdDescription { get; set; } = null!;
        [Required]
        public string ContactName { get; set; } = null!;
        [Required]
        public string ContactEmail { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;

        public virtual MainCategory McnameNavigation { get; set; } = null!;
        public virtual SubCategory ScnameNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<AdImage> AdImages { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
