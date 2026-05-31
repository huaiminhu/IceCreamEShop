using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class OrderInfo
{
    public int OrderInfoId { get; set; }

    public int UserAccountId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual UserAccount UserAccount { get; set; } = null!;
}
