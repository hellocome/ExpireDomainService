using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ExpireDomainService.Common.Logging
{
    public sealed class Log4netLogger : LoggerBase
    {
        private static ILog logger = LogManager.GetLogger("Logger");

        public Log4netLogger()
        {
            // Throw exception if failed. so that the object will be null 
            logger.Debug("Init...");
        }

        protected override void WriteLine(LogLevel level, String log)
        {
            switch (level)
            {
                case LogLevel.Info:
                    logger.Info(log);
                    break;
                case LogLevel.Debug:
                    logger.Debug(log);
                    break;
                case LogLevel.Error:
                    logger.Error(log);
                    break;
                case LogLevel.Warning:
                    logger.Warn(log);
                    break;
                default:
                    logger.Info(log);
                    break;
            }
        }
    }
}
