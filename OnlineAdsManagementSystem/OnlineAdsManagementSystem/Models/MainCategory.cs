using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    public partial class MainCategory
    {
        public MainCategory()
        {
            Ads = new HashSet<Ad>();
            SubCategories = new HashSet<SubCategory>();
        }

        
        public int Mcid { get; set; }
        public string Mcname { get; set; } = null!;

        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
