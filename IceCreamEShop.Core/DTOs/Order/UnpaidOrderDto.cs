using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.Order
{
    public class UnpaidOrderDto
    {
        public int OrderInfoId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentProvider { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
