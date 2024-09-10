using System;
using System.Collections.Generic;

namespace OnlineAdsManagementSystem.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Ads = new HashSet<Ad>();
        }

        public int Scid { get; set; }
        public int Mcid { get; set; }
        public string Scname { get; set; } = null!;

        public virtual MainCategory Mc { get; set; } = null!;
        public virtual ICollection<Ad> Ads { get; set; }
    }
}
