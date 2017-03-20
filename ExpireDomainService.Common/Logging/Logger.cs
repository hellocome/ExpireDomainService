using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Logging
{
    public sealed class Logger
    {
        private static ILogger instance = LoggerFactory.getLogger();

        public static ILogger Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
