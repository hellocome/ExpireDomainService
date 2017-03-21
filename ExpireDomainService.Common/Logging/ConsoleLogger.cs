using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Logging
{
    public sealed class ConsoleLogger : LoggerBase
    {
        protected override void WriteLine(LogLevel level, String log)
        {
            Print(level, log);
        }

        private void Print(LogLevel type, string message)
        {
            string typeOfLog = GetLogLevelString(type);

            switch (type)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.WriteLine(string.Format("{0} {1:-10} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), typeOfLog, message));
        }
    }
}
