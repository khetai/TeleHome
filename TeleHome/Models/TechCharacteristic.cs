using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class TechCharacteristic
{
    public int TechCharacteristicId { get; set; }

    public string? TechCharacteristicName { get; set; }

    public int? TechCharacteristicSubCategoryId { get; set; }

    public virtual SubCategory? TechCharacteristicSubCategory { get; set; }

    public virtual ICollection<TechCharacteristicsContent> TechCharacteristicsContents { get; set; } = new List<TechCharacteristicsContent>();
}
