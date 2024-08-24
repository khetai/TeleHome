using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class CampaignViewComponent:ViewComponent
    {
        private readonly RmlubecoTelehomeContext _db;

        public CampaignViewComponent(RmlubecoTelehomeContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var campaign = await _db.Campaigns.ToListAsync();
            var firstCampaignId = await _db.Campaigns.OrderBy(p => p.CampaignId).Select(p => p.CampaignId).FirstOrDefaultAsync();
            ViewBag.FirstCampaignId = firstCampaignId;
            return View(campaign);
        }
    }
}
