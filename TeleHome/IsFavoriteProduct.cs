using TeleHome.Models;

namespace TeleHome
{
    public class IsFavoriteProduct
    {
        public Product Product { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBasket { get; set; }
    }
}
