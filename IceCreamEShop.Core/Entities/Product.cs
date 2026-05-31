using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductPrice { get; set; }

    public string ProductPicUrl { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
