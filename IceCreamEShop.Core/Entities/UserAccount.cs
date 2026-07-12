using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class UserAccount
{
    public int UserAccountId { get; set; }
    
    public string Email { get; set; } = null!;

    public string EnPassword { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int UserRole { get; set; }

    public bool Isactive { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();

    public virtual ShoppingCart? ShoppingCart { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
