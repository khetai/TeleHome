using static TeleHome.Controllers.MainController;

namespace TeleHome
{
    public class ProductsViewModel
    {
        public List<ProductInfo> Products { get; set; }
        public int Count { get; set; }
        public int? DeepId { get; set; }
        public int? ProductId { get; set; }
        public string Search { get; set; }
        public double? MinimumPr { get; set; }
        public double? MaximumPr { get; set; }
    }
}
