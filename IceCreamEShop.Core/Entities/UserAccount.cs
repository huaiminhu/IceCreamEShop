using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class UserAccount
{
    public int UserAccountId { get; set; }

    public string UserName { get; set; } = null!;

    public string EnPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int? ShoppingCartId { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
