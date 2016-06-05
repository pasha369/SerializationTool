using System;

namespace SerializationLogger.Abstract
{
    public interface ILoggerMethods
    {
        void Debug(object message);

        void Debug(string message, params object[] args);

        void DebugException(string message, Exception exception);

        void Error(object message);

        void Error(string message, params object[] args);

        void ErrorException(string message, Exception exception);

        void Fatal(object message);

        void Fatal(string message, params object[] args);

        void FatalException(string message, Exception exception);

        void Info(object message);

        void Info(string message, params object[] args);

        void InfoException(string message, Exception exception);

        void Warn(object message);

        void Warn(string message, params object[] args);

        void WarnException(string message, Exception exception);

        void Trace(object message);

        void Trace(string message, params object[] args);

        void TraceException(string message, Exception exception);
    }
}
