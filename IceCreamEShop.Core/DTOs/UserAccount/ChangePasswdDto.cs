using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.DTOs.UserAccount
{
    public class ChangePasswdDto
    {
        public int UserAccountId { get; set; }
        public string CurrentPasswd { get; set; } = null!;
        public string NewPasswd { get; set; } = null!;
    }
}
