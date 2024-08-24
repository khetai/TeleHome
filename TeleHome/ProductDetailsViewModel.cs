using TeleHome.Models;

namespace TeleHome
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public List<Comment> Comment { get; set; }
        public List<ColorDetailsViewModel> Colors { get; set; }
        public int CommentCount { get; set; }
        public bool IsBasket { get; set; }
        public bool IsFavorite { get; set; }
        public List<Product> SimilarProduct { get; set; }
        public List<ImagesPh> Image { get; set; }
        public List<TechCharacteristicsContent> TechCharacteristicsContent { get; set; }
    }
}
