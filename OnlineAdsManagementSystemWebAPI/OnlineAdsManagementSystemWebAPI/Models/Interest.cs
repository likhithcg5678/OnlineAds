using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    [Table("Interest")]
    public partial class Interest
    {
        [Key]
        [Column("InterestID")]
        public int InterestId { get; set; }
        [Column("AdID")]
        public int AdId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column(TypeName = "text")]
        public string InterestMessage { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? Timestamp { get; set; }

        [ForeignKey("AdId")]
        [InverseProperty("Interests")]
        public virtual Ad Ad { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("Interests")]
        public virtual User User { get; set; } = null!;
    }
}
