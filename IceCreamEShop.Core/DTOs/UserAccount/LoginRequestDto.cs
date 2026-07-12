using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.UserAccount
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = null!;
        public string Passwd { get; set; } = null!;
    }
}
