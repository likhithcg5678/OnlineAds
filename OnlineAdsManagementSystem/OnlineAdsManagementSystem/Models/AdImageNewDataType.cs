namespace OnlineAdsManagementSystem.Models
{
    public class AdImageNewDataType
    {
        public int ImageId { get; set; }
        public int AdId { get; set; }
        public IFormFile photoUrl { get; set; } = null!;
    }
}
