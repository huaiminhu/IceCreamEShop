using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IceCreamEShop.Core.Common
{
    public static class LinePaySignUtility
    {
        public static string CreateSignature(string secret, string uri, string body, string nonce)
        {
            var signatureSource = secret + uri + body + nonce;
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var sourceBytes = Encoding.UTF8.GetBytes(signatureSource);

            using (var hmac = new HMACSHA256(secretBytes))
            {
                var hashBytes = hmac.ComputeHash(sourceBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
