using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    [Table("Ad")]
    public partial class Ad
    {
        public Ad()
        {
            AdImages = new HashSet<AdImage>();
            Interests = new HashSet<Interest>();
        }

        [Key]
        [Column("AdID")]
        public int AdId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("MCid")]
        public int Mcid { get; set; }
        [Column("SCid")]
        public int Scid { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string AdTitle { get; set; } = null!;
        [Column(TypeName = "text")]
        public string AdDescription { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string ContactName { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string ContactEmail { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string Status { get; set; } = null!;

        [ForeignKey("Mcid")]
        [InverseProperty("Ads")]
        public virtual MainCategory Mc { get; set; } = null!;
        [ForeignKey("Scid")]
        [InverseProperty("Ads")]
        public virtual SubCategory Sc { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("Ads")]
        public virtual User User { get; set; } = null!;
        [InverseProperty("Ad")]
        public virtual ICollection<AdImage> AdImages { get; set; }
        [InverseProperty("Ad")]
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
