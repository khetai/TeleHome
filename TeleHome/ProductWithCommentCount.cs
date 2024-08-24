using TeleHome.Models;

namespace TeleHome
{
    public class ProductWithCommentCount
    {
        public Product? Product { get; set; }
        public Campaign? Campaigns { get; set; }
        public int CommentCount { get; set; }
        public bool IsBasket { get; set; }
        public bool IsFavorite { get; set; }
    }
}
