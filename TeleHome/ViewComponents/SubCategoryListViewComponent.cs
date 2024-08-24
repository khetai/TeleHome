using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class SubCategoryListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            using (var item = new RmlubecoTelehomeContext())
            {
                var subCategoryList = item.SubCategories.Where(x=>x.SubCategoryIsActive==true).ToList();
                var subCategoryViewModelList = new List<CategoryWithCount>();

                foreach (var subCategory in subCategoryList)
                {
                    var productCount = item.Products.Count(p => p.ProductDeepCategory.DeepCategorySubCategoryId == subCategory.SubCategoryId && p.ProductIsActive == true && p.ProductIsSctock == true);
                    var subCategoryViewModel = new CategoryWithCount
                    {
                        SubCategory = subCategory,
                        SubCategoryCount = productCount
                    };
                    subCategoryViewModelList.Add(subCategoryViewModel);
                }

                return View(subCategoryViewModelList);
            }

        }
    }
}
