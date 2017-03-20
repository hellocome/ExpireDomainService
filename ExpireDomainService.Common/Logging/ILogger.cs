using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Logging
{
    public interface ILogger
    {
        bool IsDebugOn { get; }
        void Error(string logContent);
        void Error(string logContent, Exception ex);
        void Error(Exception ex);
        void Error(String format, params Object[] arg);
        void Warning(string logContent);
        void Warning(string logContent, Exception ex);
        void Warning(Exception ex);
        void Warning(String format, params Object[] arg);
        void Info(string logContent);
        void Info(String format, params Object[] arg);
        void Debug(String format, params Object[] arg);
        void Debug(string logContent);
    }
}
