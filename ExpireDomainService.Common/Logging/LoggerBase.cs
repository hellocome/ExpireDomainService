using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpireDomainService.Common.Logging
{
    public abstract class LoggerBase : ILogger
    {
        private IFormatProvider InternalFormatProvider;

        protected LoggerBase(IFormatProvider formatProvider)
        {
            InternalFormatProvider = formatProvider;
        }

        protected LoggerBase()
        {
            InternalFormatProvider = null;
        }
        
        public virtual IFormatProvider FormatProvider
        {
            get
            {
                if (InternalFormatProvider == null)
                    return System.Threading.Thread.CurrentThread.CurrentCulture;
                else
                    return InternalFormatProvider;
            }
        }

        protected virtual String FormatLine(String format, params Object[] arg)
        {
            if (arg == null)                       // avoid ArgumentNullException from String.Format
                return format; // faster than Out.WriteLine(format, (Object)arg);
            else
                return String.Format(FormatProvider, format, arg);
        }
        protected abstract void WriteLine(LogLevel level, String log);
        public virtual bool IsDebugOn { get; protected set; }
        public virtual void Error(string logContent)
        {
            WriteLine(LogLevel.Error, logContent);
        }
        public virtual void Error(string logContent, Exception ex)
        {
            if (ex != null)
            {
                WriteLine(LogLevel.Error, FormatLine("{0} {1}", logContent, IsDebugOn ? ex.ToString() : ex.Message));
            }
            else
            {
                WriteLine(LogLevel.Error, logContent);
            }
        }
        public virtual void Error(Exception ex)
        {
            if (ex != null)
            {
                WriteLine(LogLevel.Error, IsDebugOn ? ex.ToString() : ex.Message);
            }
            else
            {
                WriteLine(LogLevel.Error, "Unknown Error: Exception = null");
            }
        }
        public virtual void Error(String format, params Object[] arg)
        {
            WriteLine(LogLevel.Error, FormatLine(format, arg));
        }
        public virtual void Warning(string logContent)
        {
            WriteLine(LogLevel.Error, logContent);
        }
        public virtual void Warning(string logContent, Exception ex)
        {
            if (ex != null)
            {
                WriteLine(LogLevel.Warning, FormatLine("{0} {1}", logContent, IsDebugOn ? ex.ToString() : ex.Message));
            }
            else
            {
                WriteLine(LogLevel.Warning, logContent);
            }
        }
        public virtual void Warning(Exception ex)
        {
            if (ex != null)
            {
                WriteLine(LogLevel.Warning, IsDebugOn ? ex.ToString() : ex.Message);
            }
            else
            {
                WriteLine(LogLevel.Warning, "Unknown Error: Exception = null");
            }
        }
        public virtual void Warning(String format, params Object[] arg)
        {
            WriteLine(LogLevel.Warning, FormatLine(format, arg));
        }
        public virtual void Info(string logContent)
        {
            WriteLine(LogLevel.Info, logContent);
        }
        public virtual void Info(String format, params Object[] arg)
        {
            WriteLine(LogLevel.Info, FormatLine(format, arg));
        }
        public virtual void Debug(string logContent)
        {
            WriteLine(LogLevel.Debug, logContent);
        }
        public virtual void Debug(String format, params Object[] arg)
        {
            WriteLine(LogLevel.Debug, FormatLine(format, arg));
        }


        public static String GetLogLevelString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARNING";
                default:
                    return "INFO";
            }
        }
    }
}
