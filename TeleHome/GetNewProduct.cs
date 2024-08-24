using TeleHome.Models;

namespace TeleHome
{
    public class GetNewProduct
    {
        public List<TechCharacteristic> TechCharacteristic { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<DeepCategory> DeepCategories { get; set; }
    }
}
