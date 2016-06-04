using Autofac;

namespace SerializationTool.Startup
{
    /// <summary>
    /// Represents dependency resolver.
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// Gets or sets current container.
        /// </summary>
        public static IContainer Current { get; set; }
    }
}
