using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.CartItem
{
    public class DisplayItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public string ProductPicUrl { get; set; } = null!;
    }
}
