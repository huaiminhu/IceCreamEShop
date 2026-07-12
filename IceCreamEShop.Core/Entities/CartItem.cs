using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class CartItem
{
    public int ShoppingCartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ShoppingCart ShoppingCart { get; set; } = null!;
}
