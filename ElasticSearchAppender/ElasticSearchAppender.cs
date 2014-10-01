using log4net.Appender;
using log4net.Core;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    public class ElasticSearchAppender : AppenderSkeleton
    {
        private IConfigurationBuilder configurationBuilder;
        private IUniqueIdGenerator uniqueIdGenerator;
        private ILogEventBuilder logEventBuilder;

        public string ConnectionString { get; set; }

        public ElasticSearchAppender()
        {
            configurationBuilder = new ConfigurationBuilder();
            uniqueIdGenerator = new UniqueIdGenerator();
            logEventBuilder = new LogEventBuilder();
        }

        public ElasticSearchAppender(IConfigurationBuilder configurationBuilder, IUniqueIdGenerator uniqueIdGenerator, ILogEventBuilder logEventBuilder)
        {
            this.configurationBuilder = configurationBuilder;
            this.uniqueIdGenerator = uniqueIdGenerator;
            this.logEventBuilder = logEventBuilder;
        }



        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                var exception = new InvalidOperationException("Connection string not present.");
                ErrorHandler.Error("Connection string not included in appender.", exception, ErrorCode.GenericFailure);
                return;
            }

            SendEvent(loggingEvent);
        }

        private void SendEvent(log4net.Core.LoggingEvent loggingEvent)
        {
            var configuration = configurationBuilder.GetElasticSearchConfiguration(ConnectionString);
            var client = new ElasticClient(configuration.Settings);
            var logEvent = logEventBuilder.CreateLogEvent(loggingEvent);
            try
            {
                client.IndexAsync(logEvent, configuration.Settings.DefaultIndex, configuration.EventType);
            }
            catch (InvalidOperationException ex)
            {
                ErrorHandler.Error("Failde to connect to ElasticSearch", ex, ErrorCode.GenericFailure);
            }
        }
    }
}
