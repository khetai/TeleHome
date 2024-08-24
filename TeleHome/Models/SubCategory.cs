using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class SubCategory
{
    public int SubCategoryId { get; set; }

    public int? SubCategoryCategoryId { get; set; }

    public string? SubCategoryName { get; set; }

    public string? SubCategoryPhotoUrl { get; set; }

    public bool? SubCategoryIsActive { get; set; }

    public virtual ICollection<DeepCategory> DeepCategories { get; set; } = new List<DeepCategory>();

    public virtual Category? SubCategoryCategory { get; set; }

    public virtual ICollection<TechCharacteristic> TechCharacteristics { get; set; } = new List<TechCharacteristic>();
}
