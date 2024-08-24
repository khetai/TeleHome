using TeleHome.Models;

namespace TeleHome
{
    public class CategoryWithCount
    {
        public SubCategory? SubCategory{ get; set; }
        public int SubCategoryCount { get; set; }
    }
}
