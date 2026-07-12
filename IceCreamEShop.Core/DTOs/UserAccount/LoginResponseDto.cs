using IceCreamEShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.UserAccount
{
    public class LoginResponseDto
    {
        public int UserAccountId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserRole { get; set; } = null!;
    }
}
