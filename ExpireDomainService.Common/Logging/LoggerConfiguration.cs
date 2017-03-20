using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExpireDomainService.Common.Reflection;

namespace ExpireDomainService.Common.Logging
{
    public class LoggerConfiguration
    {
        public static readonly string CONFIGURATION_FILE = "LoggerConfiguration.xml";
        private static LoggerConfiguration instance = new LoggerConfiguration();

        public static LoggerConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        private LoggerConfiguration()
        {
            Load();
        }

        private void Load()
        {
            try
            {

                string localConfiguration = AppDomain.CurrentDomain.BaseDirectory + System.IO.Path.DirectorySeparatorChar + CONFIGURATION_FILE;
                XmlDocument doc = new XmlDocument();
                doc.Load(localConfiguration);

                XmlElement ele = (doc.SelectSingleNode("Configuration/Logger") as XmlElement);

                string moduleName = ele.GetAttribute("module");
                string implClass = ele.GetAttribute("class");

                if (string.IsNullOrEmpty(moduleName))
                {
                    Logger = ObjectHelper.Create<ILogger>(implClass);
                }
                else
                {
                    Logger = ObjectHelper.Create<ILogger>(moduleName, implClass);
                }

                IsDebugOn = ele.GetAttribute("IsDebugOn").Equals("true", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                IsDebugOn = false;
            }
        }

        public bool IsDebugOn
        {
            get;
            private set;
        }

        public ILogger Logger
        {
            get;
            private set;
        }
    }
}
