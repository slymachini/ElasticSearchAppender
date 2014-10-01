using log4net.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    public class LogEventBuilder : ILogEventBuilder
    {
        public dynamic CreateLogEvent(LoggingEvent loggingEvent)
        {
            if (loggingEvent == null)
            {
                throw new ArgumentNullException("loggingEvent");
            }
            dynamic logEvent = new ExpandoObject();
            logEvent.Id = new UniqueIdGenerator().GenerateUniqueId();
            logEvent.LoggerName = loggingEvent.LoggerName;
            logEvent.Domain = loggingEvent.Domain;
            logEvent.Identity = loggingEvent.Identity;
            logEvent.ThreadName = loggingEvent.ThreadName;
            logEvent.UserName = loggingEvent.UserName;
            logEvent.MessageObject = loggingEvent.MessageObject == null ? "" : loggingEvent.MessageObject.ToString();
            logEvent.TimeStamp = loggingEvent.TimeStamp;
            logEvent.Exception = loggingEvent.ExceptionObject == null ? "" : loggingEvent.ExceptionObject.ToString();
            logEvent.Message = loggingEvent.RenderedMessage;
            logEvent.Fix = loggingEvent.Fix.ToString();
            logEvent.HostName = Environment.MachineName;
            if (loggingEvent.Level != null)
            {
                logEvent.Level = loggingEvent.Level.DisplayName;
            }
            if (loggingEvent.LocationInformation != null)
            {
                logEvent.ClassName = loggingEvent.LocationInformation.ClassName;
                logEvent.FileName = loggingEvent.LocationInformation.FileName;
                logEvent.LineNumber = loggingEvent.LocationInformation.LineNumber;
                logEvent.FullInfo = loggingEvent.LocationInformation.FullInfo;
                logEvent.MethodName = loggingEvent.LocationInformation.MethodName;
            }
            var properties = loggingEvent.GetProperties();
            var expandoDict = logEvent as IDictionary<string, Object>;
            foreach (var propertyKey in properties.GetKeys())
            {
                expandoDict.Add(propertyKey, properties[propertyKey].ToString());
            }
            return logEvent;
        }
    }
}
