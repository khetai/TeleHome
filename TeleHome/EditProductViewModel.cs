using TeleHome.Models;

namespace TeleHome
{
    public class EditProductViewModel
    {
        public Product Products{ get; set; }
        public List<ImagesPh> Image { get; set; }
        public List<DeepCategory> DeepCategory { get; set; }
        public List<TechCharacteristicsContent> TechCharacteristicsContent { get; set; }
        public List<ColorDetailsViewModel> Colors { get; set; }
    }
}
