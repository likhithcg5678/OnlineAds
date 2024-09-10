using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Ads = new HashSet<Ad>();
            Interests = new HashSet<Interest>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string FullName { get; set; } = null!;
        [Column("DOB", TypeName = "date")]
        public DateTime Dob { get; set; }
        [StringLength(7)]
        [Unicode(false)]
        public string Gender { get; set; } = null!;
        [Column("EmailID")]
        [StringLength(30)]
        [Unicode(false)]
        public string EmailId { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string City { get; set; } = null!;
        [StringLength(15)]
        [Unicode(false)]
        public string State { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PhoneNo { get; set; } = null!;
        [StringLength(9)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string UserType { get; set; } = null!;

        [InverseProperty("User")]
        public virtual ICollection<Ad> Ads { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
