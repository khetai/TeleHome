using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? CommentUserId { get; set; }

    public int? CommentProductId { get; set; }

    public string? CommentContent { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual Product? CommentProduct { get; set; }

    public virtual User? CommentUser { get; set; }
}
