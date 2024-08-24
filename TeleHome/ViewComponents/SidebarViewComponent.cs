using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class SidebarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            using (var item = new RmlubecoTelehomeContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                    ViewBag.Favorite = item.Favorites.Include(x=>x.FavoriteProduct).Where(x => x.FavoriteUserId == userId).ToList();
                }
                else
                {
                    ViewBag.Favorite = null;
                }
                return View();
            }

        }
    }
}
