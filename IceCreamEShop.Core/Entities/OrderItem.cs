using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class OrderItem
{
    public int OrderInfoId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual OrderInfo OrderInfo { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
