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

        public int SubCategoryId { get; set; }
        public int MainCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;

        public virtual MainCategory MainCategory { get; set; } = null!;
        public virtual ICollection<Ad> Ads { get; set; }
    }
}
