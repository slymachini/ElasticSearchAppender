using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    public interface IUniqueIdGenerator
    {
        string GenerateUniqueId();
        string GenerateUniqueId(int size);
    }
}
