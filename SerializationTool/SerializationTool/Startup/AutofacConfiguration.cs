using Autofac;
using SerializationClient;
using SerializationClient.Core.FIleWriter;
using SerializationClient.Core.SerializeClients;
using SerializationLogger.Abstract;
using SerializationLogger.Concrete;
using SerializationTool.ViewModels;

namespace SerializationTool.Startup
{
    /// <summary>
    /// Represents autofac configuration.
    /// </summary>
    public class AutofacConfiguration
    {
        /// <summary>
        /// Register components.
        /// </summary>
        /// <returns>Container</returns>
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BinarySerializeClient>().As<ISerializeClient>();
            builder.RegisterType<FolderCreator>().As<IFolderCreator>();
            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>();

            builder.RegisterType<SerializeClientWrapper>().AsSelf();
            builder.RegisterType<NavigationViewModel>().AsSelf();
            builder.RegisterType<SerializeViewModel>().AsSelf();
            builder.RegisterType<DeserializeViewModel>().AsSelf();


            var container = builder.Build();
            DependencyResolver.Current = container;

            return container;
        }
    }
}
