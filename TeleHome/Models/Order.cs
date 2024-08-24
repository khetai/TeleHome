using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? OrderUserId { get; set; }

    public double? OrderMoney { get; set; }

    public DateTime? OrderDate { get; set; }

    public bool? OrderStatus { get; set; }

    public virtual ICollection<OrderBasket> OrderBaskets { get; set; } = new List<OrderBasket>();

    public virtual User? OrderUser { get; set; }
}
