using System;
using System.Collections.Generic;

namespace TeleHome.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? UserStatusId { get; set; }

    public string? UserName { get; set; }

    public string? UserSurname { get; set; }

    public string? UserPassword { get; set; }

    public string? UserPhone { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhoto { get; set; }

    public string? UserAddress { get; set; }

    public string? UserForgotToken { get; set; }

    public DateTime? UserExpandDate { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Status? UserStatus { get; set; }
}
