using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TeleHome.Models;
using static TeleHome.Controllers.MainController;

namespace TeleHome.ViewComponents
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly RmlubecoTelehomeContext _db;

        public ProfileViewComponent(RmlubecoTelehomeContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {

            var count = 0;

            var basketDataJson = HttpContext.Request.Cookies["Basket"];
            List<BasketItem> cookieBasketItems = new List<BasketItem>();

            if (!string.IsNullOrEmpty(basketDataJson))
            {
                var basketData = JsonConvert.DeserializeObject<BasketCookieData>(basketDataJson);
                cookieBasketItems = basketData.BasketItems;
            }

            var productIds = cookieBasketItems.Select(item => item.ProductId).ToList();

            var dbProducts = _db.Products.Where(product => productIds.Contains(product.ProductId)).ToList();

            count = dbProducts.Count;   
            ViewBag.Count = count;  

            using (var item = new RmlubecoTelehomeContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
                    ViewBag.FavoriteCount = item.Favorites.Where(x => x.FavoriteUserId == userId).Count();
                    ViewBag.BasketCount = item.Baskets.Where(x => x.BasketUserId == userId).Count();
                }
                else
                {
                    ViewBag.FavoriteCount = 0;
                }
                return View();
            }


        }
    }
}
