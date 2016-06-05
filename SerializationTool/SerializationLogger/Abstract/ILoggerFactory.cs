namespace SerializationLogger.Abstract
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Returns logger by it's name.
        /// </summary>
        /// <param name="name">Logger name.</param>
        ISerializeLogger GetLogger(string name);
    }
}
