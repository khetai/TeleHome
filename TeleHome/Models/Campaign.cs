using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public string? CampaignImage { get; set; }

    public string? CampaignName { get; set; }

    public string? CampaignDescription { get; set; }

    public DateTime? CampaignStartDate { get; set; }

    public DateTime? CampaignEndDate { get; set; }

    public int? CampaignDeepCategoryId { get; set; }

    public virtual DeepCategory? CampaignDeepCategory { get; set; }
}
