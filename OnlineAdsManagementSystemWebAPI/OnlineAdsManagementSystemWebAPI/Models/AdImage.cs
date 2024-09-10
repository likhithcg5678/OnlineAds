using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public partial class AdImage
    {
        [Key]
        [Column("ImageID")]
        public int ImageId { get; set; }
        [Column("AdID")]
        public int AdId { get; set; }
        [Column("ImageURL")]
        [StringLength(255)]
        [Unicode(false)]
        public string ImageUrl { get; set; } = null!;

        [ForeignKey("AdId")]
        [InverseProperty("AdImages")]
        public virtual Ad Ad { get; set; } = null!;
    }
}
