using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class OrderBasket
{
    public int OrderBasketId { get; set; }

    public int OrderBasketProductId { get; set; }

    public double OrderBasketMoney { get; set; }

    public int OrderBasketOrderId { get; set; }

    public int OrderBasketCount { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Order OrderBasketOrder { get; set; } = null!;

    public virtual Product OrderBasketProduct { get; set; } = null!;
}
