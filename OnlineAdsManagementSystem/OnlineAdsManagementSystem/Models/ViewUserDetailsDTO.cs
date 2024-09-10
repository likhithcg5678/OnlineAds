
using System.ComponentModel.DataAnnotations;

namespace OnlineAdsManagementSystem.Models
{
    public class ViewUserDetailsDTO
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserType { get; set; } = null!;

        
    }
}
