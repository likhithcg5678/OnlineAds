using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    [Table("MainCategory")]
    [Index("Mcname", Name = "UQ__MainCate__490BB05D8FC98C6B", IsUnique = true)]
    public partial class MainCategory
    {
        public MainCategory()
        {
            Ads = new HashSet<Ad>();
            SubCategories = new HashSet<SubCategory>();
        }

        [Key]
        [Column("MCid")]
        public int Mcid { get; set; }
        [Column("MCname")]
        [StringLength(50)]
        [Unicode(false)]
        public string Mcname { get; set; } = null!;

        [InverseProperty("Mc")]
        public virtual ICollection<Ad> Ads { get; set; }
        [InverseProperty("Mc")]
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
