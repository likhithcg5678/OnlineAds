using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    public partial class User
    {
        public User()
        {
            Ads = new HashSet<Ad>();
            Interests = new HashSet<Interest>();
        }

        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string EmailId { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string PhoneNo { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[\\W_])[A-Za-z\\d\\W_]{9}$", ErrorMessage = "Password must be exactly 9 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = null!;
        [Required]
        public string UserType { get; set; } = null!;

        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
