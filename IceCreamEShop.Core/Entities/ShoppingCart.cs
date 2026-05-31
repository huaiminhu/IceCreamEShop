using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class ShoppingCart
{
    public int ShoppingCartId { get; set; }

    public int UserAccountId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual UserAccount UserAccount { get; set; } = null!;
}
