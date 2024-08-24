using Microsoft.AspNetCore.Mvc;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class LastTriCampaignViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            using (var item = new RmlubecoTelehomeContext())
            {
                ViewBag.Kamil = item.Campaigns.OrderByDescending(x => x.CampaignStartDate).Take(3).ToList();
                return View();
            }

        }
    }
}
