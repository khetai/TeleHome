using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class ColorsPr
{
    public int ColorId { get; set; }

    public string? ColorHash { get; set; }

    public string? ColorTitle { get; set; }

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
}
