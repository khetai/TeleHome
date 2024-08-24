using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class ImagesPh
{
    public int ImageId { get; set; }

    public int? ImageProductId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Product? ImageProduct { get; set; }
}
