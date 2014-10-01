using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    public class ElasticSearchConfiguration
    {
        public ConnectionSettings Settings { get; set; }
        public string EventType { get; set; }
    }
}
