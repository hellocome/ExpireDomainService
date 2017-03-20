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

            Console.WriteLine(string.Format("{0} {1:-10} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), typeOfLog, message));
        }
    }
}
