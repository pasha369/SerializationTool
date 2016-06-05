using System.Collections.Generic;
using SerializationLogger.Abstract;

namespace SerializationLogger.Concrete
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly Dictionary<string, LoggerImplementation> _logs;

        public LoggerFactory()
        {
            _logs = new Dictionary<string, LoggerImplementation>();
        }

        public ISerializeLogger GetLogger(string name)
        {
            lock (_logs)
            {
                ISerializeLogger log;

                string logName = name.ToLower();

                if (!_logs.ContainsKey(logName))
                {
                    var newLog = new LoggerImplementation(name);
                    _logs[logName] = newLog;
                    log = newLog;
                }
                else
                {
                    log = _logs[name];
                }

                return log;
            }
        }
    }
}
