using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.Product
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? Category { get; set; }

        public string? ProductDescription { get; set; }

        public int? ProductPrice { get; set; }

        public string? ProductPicUrl { get; set; }

        public int? Quantity { get; set; }

        public bool? IsPublished { get; set; }
    }
}
