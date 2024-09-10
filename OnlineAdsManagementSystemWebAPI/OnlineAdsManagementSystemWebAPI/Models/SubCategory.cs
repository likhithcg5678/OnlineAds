using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    [Table("SubCategory")]
    [Index("Scname", Name = "UQ__SubCateg__6737F22BA8AA35E7", IsUnique = true)]
    public partial class SubCategory
    {
        public SubCategory()
        {
            Ads = new HashSet<Ad>();
        }

        [Key]
        [Column("SCid")]
        public int Scid { get; set; }
        [Column("MCid")]
        public int Mcid { get; set; }
        [Column("SCname")]
        [StringLength(50)]
        [Unicode(false)]
        public string Scname { get; set; } = null!;

        [ForeignKey("Mcid")]
        [InverseProperty("SubCategories")]
        public virtual MainCategory Mc { get; set; } = null!;
        [InverseProperty("Sc")]
        public virtual ICollection<Ad> Ads { get; set; }
    }
}
