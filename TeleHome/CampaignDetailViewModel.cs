using TeleHome.Models;

namespace TeleHome
{
    public class CampaignDetailViewModel
    {
        public Campaign Campaigns { get; set; }
        public IEnumerable<ProductWithCommentCount> ProductList { get; set; }
    }
}
