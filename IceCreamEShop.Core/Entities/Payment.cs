using System;
using System.Collections.Generic;

namespace IceCreamEShop.Core.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderInfoId { get; set; }

    public int PaymentAmount { get; set; }

    public string PaymentProvider { get; set; } = null!;

    public int PaymentStatus { get; set; }

    public virtual OrderInfo OrderInfo { get; set; } = null!;
}
