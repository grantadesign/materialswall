using System;
using Ninject.Extensions.Logging;
using NLog;

namespace Granta.MaterialsWall.Logging
{
    internal sealed class NLogToNinjectLogConverter : ILogger
    {
        private readonly Logger logger;

        public NLogToNinjectLogConverter(Logger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            
            this.logger = logger;
        }

        public Type Type{get {return GetType();}}
        public bool IsDebugEnabled{get {return logger.IsDebugEnabled;}}
        public bool IsInfoEnabled{get {return logger.IsInfoEnabled;}}
        public bool IsTraceEnabled{get {return logger.IsTraceEnabled;}}
        public bool IsWarnEnabled{get {return logger.IsWarnEnabled;}}
        public bool IsErrorEnabled{get {return logger.IsErrorEnabled;}}
        public bool IsFatalEnabled{get {return logger.IsFatalEnabled;}}

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            logger.Debug(format, args);
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.DebugException(message, exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            logger.Info(format, args);
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.InfoException(message, exception);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(string format, params object[] args)
        {
            logger.Trace(format, args);
        }

        public void Trace(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.TraceException(message, exception);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string format, params object[] args)
        {
            logger.Warn(format, args);
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.WarnException(message, exception);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string format, params object[] args)
        {
            logger.Error(format, args);
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.ErrorException(message, exception);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string format, params object[] args)
        {
            logger.Fatal(format, args);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            string message = string.Format(format, args);
            logger.FatalException(message, exception);
        }
    }
}
