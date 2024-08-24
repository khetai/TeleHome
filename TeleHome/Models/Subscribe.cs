using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Subscribe
{
    public int SubscribeId { get; set; }

    public string? SubscribeEmail { get; set; }

    public DateTime? SubscribeDate { get; set; }
}
