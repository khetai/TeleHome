using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class ProductColor
{
    public int ProductColorId { get; set; }

    public int? ProductColorProductId { get; set; }

    public int? ProductColorColorId { get; set; }

    public virtual ColorsPr? ProductColorColor { get; set; }

    public virtual Product? ProductColorProduct { get; set; }
}
