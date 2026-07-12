using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class OrderInfo
{
    public int OrderInfoId { get; set; }

    public int UserAccountId { get; set; }

    public int PaymentAmount { get; set; }

    public string PaymentProvider { get; set; } = null!;

    public int PaymentStatus { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual UserAccount UserAccount { get; set; } = null!;
}
