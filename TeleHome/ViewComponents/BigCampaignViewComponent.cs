using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleHome.Models;

namespace TeleHome.ViewComponents
{
    public class BigCampaignViewComponent: ViewComponent
    {
        private readonly RmlubecoTelehomeContext _db;

        public BigCampaignViewComponent(RmlubecoTelehomeContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bigCampaigns = await _db.BigCampaigns.ToListAsync();
            var firstCampaignId = await _db.BigCampaigns.OrderBy(p => p.BigCampaignId).Select(p => p.BigCampaignId).FirstOrDefaultAsync();
            ViewBag.FirstCampaignId = firstCampaignId;
            return View(bigCampaigns);
        }
    }
}
