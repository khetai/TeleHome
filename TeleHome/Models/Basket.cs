using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Basket
{
    public int BasketId { get; set; }

    public int? BasketProductId { get; set; }

    public int? BasketUserId { get; set; }

    public int? BasketQuantity { get; set; }

    public bool? BasketSelected { get; set; }

    public virtual Product? BasketProduct { get; set; }

    public virtual User? BasketUser { get; set; }
}
