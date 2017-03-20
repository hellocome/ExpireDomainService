using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Logging
{
    public static class LoggerFactory
    {
        private static volatile ILogger loggerInstance = null;
        private static object locker = new object();

        public static ILogger getLogger()
        {
            if (loggerInstance == null)
            {
                lock (locker)
                {
                    if (loggerInstance == null)
                    {
                        if (LoggerConfiguration.Instance.Logger == null)
                        {
                            loggerInstance = new ConsoleLogger();
                        }
                        else
                        {
                            loggerInstance = LoggerConfiguration.Instance.Logger;
                        }
                    }
                }
            }

            return loggerInstance;
        }
    }
}
