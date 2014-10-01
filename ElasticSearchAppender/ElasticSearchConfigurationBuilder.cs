using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchAppender
{
    /// <summary>
    /// Class to 
    /// </summary>
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        public ElasticSearchConfiguration GetElasticSearchConfiguration(string configurationString)
        {
            try
            {
                var parameters = ParseConfigurationString(configurationString);
                var index = parameters["Index"];
                if (!string.IsNullOrEmpty(parameters["DatePostfixFormat"]))
                {
                    index = string.Format("{0}-{1}", index, DateTime.Now.ToString(parameters["DatePostfixFormat"]));
                }
                var configuration = new ElasticSearchConfiguration();
                configuration.Settings = CreateConnectionSettings(parameters, index);
                configuration.EventType = parameters["EventType"];
                return configuration;
            }
            catch
            {
                throw new InvalidOperationException("Connection string has invalid format");
            }
        }

        private IDictionary<string,string> ParseConfigurationString(string configurationString)
        {
            return configurationString.Split(';')
                    .Select(x => x.Split('='))
                    .Where(x => x.Length == 2)
                    .ToDictionary(x => x.First(), x => x.Last());
        }

        private ConnectionSettings CreateConnectionSettings(IDictionary<string, string> parameters, string index)
        {
            return new ConnectionSettings(new Uri(string.Format("http://{0}:{1}", parameters["Server"],
                Convert.ToInt32(parameters["Port"])))).SetDefaultIndex(index);
        }
    }
}
