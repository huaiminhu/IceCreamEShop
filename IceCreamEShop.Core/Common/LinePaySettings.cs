using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.Common
{
    public class LinePaySettings
    {
        public string SandboxUrl { get; set; } = string.Empty;
        public string ChannelId { get; set; } = string.Empty;
        public string ChannelSecret { get; set; } = string.Empty;
    }
}
