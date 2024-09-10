using System;
using System.Collections.Generic;

namespace OnlineAdsManagementSystem.Models
{
    public partial class MainCategory
    {
        public MainCategory()
        {
            Ads = new HashSet<Ad>();
            SubCategories = new HashSet<SubCategory>();
        }

        public int MainCategoryId { get; set; }
        public string CategoryType { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
