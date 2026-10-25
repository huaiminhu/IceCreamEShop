using IceCreamEShop.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.CartItem
{
    public class DisplayItemDto
    {
        public ProductDto DisplayedItem { get; set; } = null!;
        public int ItemQuantity { get; set; }
    }
}
