using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? ProductDeepCategoryId { get; set; }

    public string? ProductName { get; set; }

    public double? ProductPrice { get; set; }

    public string? ProductDescription { get; set; }

    public bool? ProductIsSctock { get; set; }

    public bool? ProductIsActive { get; set; }

    public DateTime? ProductDate { get; set; }

    public string? ProductFirstPhotoUrl { get; set; }

    public double? ProductWithoutPrice { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<ImagesPh> ImagesPhs { get; set; } = new List<ImagesPh>();

    public virtual ICollection<OrderBasket> OrderBaskets { get; set; } = new List<OrderBasket>();

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

    public virtual DeepCategory? ProductDeepCategory { get; set; }

    public virtual ICollection<TechCharacteristicsContent> TechCharacteristicsContents { get; set; } = new List<TechCharacteristicsContent>();
}
