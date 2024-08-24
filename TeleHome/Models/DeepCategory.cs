using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class DeepCategory
{
    public int DeepCategoryId { get; set; }

    public int? DeepCategorySubCategoryId { get; set; }

    public string? DeepCategoryName { get; set; }

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual SubCategory? DeepCategorySubCategory { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
