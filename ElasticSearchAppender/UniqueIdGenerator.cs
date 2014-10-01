using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    /// <summary>
    /// Class to generate unique id of log event
    /// </summary>
    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        public const int DEFAULT_BYTES_NUMBER = 10;

        public string GenerateUniqueId()
        {
            return GenerateUniqueId(DEFAULT_BYTES_NUMBER);
        }

        public string GenerateUniqueId(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var rndBytes = new byte[size];
                rng.GetBytes(rndBytes);
                return BitConverter.ToString(rndBytes).Replace("-", "");
            }
        }
    }
}
