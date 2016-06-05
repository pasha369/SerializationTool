using System;
using NLog;
using SerializationLogger.Abstract;

namespace SerializationLogger.Concrete
{
    public class LoggerImplementation: ISerializeLogger
    {
        private string _name;
        private readonly Logger _log;

        public LoggerImplementation()
        {
            const string name = "main";
            _log = LogManager.GetLogger(name);
        }

        public LoggerImplementation(string name)
        {
            _log = LogManager.GetLogger(name);
        }

        public void Debug(object message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Debug(string message, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message, args);
            }
        }

        public void DebugException(string message, Exception exception)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(exception, message);
            }
        }

        public void Error(object message)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message);
            }
        }

        public void Error(string message, params object[] args)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message, args);
            }
        }

        public void ErrorException(string message, Exception exception)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(exception, message);
            }
        }

        public void Fatal(object message)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(message);
            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(message, args);
            }
        }

        public void FatalException(string message, Exception exception)
        {
            if (_log.IsFatalEnabled)
            {
                _log.Fatal(exception, message);
            }
        }

        public void Info(object message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message, args);
            }
        }

        public void InfoException(string message, Exception exception)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(exception, message);
            }
        }

        public void Warn(object message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        public void Warn(string message, params object[] args)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message, args);
            }
        }

        public void WarnException(string message, Exception exception)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(exception, message);
            }
        }

        public void Trace(object message)
        {
            if (_log.IsTraceEnabled)
            {
                _log.Trace(message);
            }
        }

        public void Trace(string message, params object[] args)
        {
            if (_log.IsTraceEnabled)
            {
                _log.Trace(message, args);
            }
        }

        public void TraceException(string message, Exception exception)
        {
            if (_log.IsTraceEnabled)
            {
                _log.Trace(exception, message);
            }
        }
    }   
}
