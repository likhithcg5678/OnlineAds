namespace OnlineAdsManagementSystemWebAPI.Models
{
    public class SubCategoryDTO
    {
        public int Scid { get; set; }
        public int Mcid { get; set; }
        public string MCName { get; set; }

        public string Scname { get; set; } = null!;
    }
}
