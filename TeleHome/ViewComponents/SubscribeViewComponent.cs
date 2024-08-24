using Microsoft.AspNetCore.Mvc;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class SubscribeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
