using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAds.Models
{
    public partial class UserTbl
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
