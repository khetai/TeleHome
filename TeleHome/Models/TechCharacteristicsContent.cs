using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class TechCharacteristicsContent
{
    public int TechCharacteristicsContentId { get; set; }

    public int? TechCharacteristicsContentProductId { get; set; }

    public int? TechCharacteristicsContentTechCharacteristicsId { get; set; }

    public string? TechCharacteristicsContentTitle { get; set; }

    public virtual Product? TechCharacteristicsContentProduct { get; set; }

    public virtual TechCharacteristic? TechCharacteristicsContentTechCharacteristics { get; set; }
}
