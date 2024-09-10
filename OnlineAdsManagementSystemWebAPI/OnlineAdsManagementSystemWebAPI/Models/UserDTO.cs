using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public class UserDTO
    {
        
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

        public string Password { get; set; } = null!;
        
        public string UserType { get; set; } = null!;

        
    }
}
