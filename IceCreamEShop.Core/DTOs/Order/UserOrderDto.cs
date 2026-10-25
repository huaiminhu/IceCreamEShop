using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.Order
{
    public class UserOrderDto
    {
        public int OrderInfoId { get; set; }
        public int PaymentAmount { get; set; }
        public int PaymentStatus { get; set; }
    }
}
