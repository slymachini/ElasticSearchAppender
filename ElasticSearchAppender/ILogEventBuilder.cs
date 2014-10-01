using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    public interface ILogEventBuilder
    {
        dynamic CreateLogEvent(LoggingEvent loggingEvent);
    }
}
