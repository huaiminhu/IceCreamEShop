using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.UserAccount
{
    public class UpdateUserDto
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; } = null!;
        public string? PhoneNumber { get; set; } 
    }
}
