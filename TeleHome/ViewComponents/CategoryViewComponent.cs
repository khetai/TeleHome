using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            using (var item = new RmlubecoTelehomeContext())
            {
                return View(item.Categories.Include(item => item.SubCategories).ThenInclude(sb => sb.DeepCategories).ToList());
            }

        }
    }
}
