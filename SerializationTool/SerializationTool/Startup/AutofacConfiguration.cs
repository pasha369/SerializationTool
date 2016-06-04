using Autofac;
using SerializationTool.ViewModels;

namespace SerializationTool.Startup
{
    public class AutofacConfiguration
    {
        public static IContainer Config()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<NavigationViewModel>()
                .AsSelf();

            var container = builder.Build();

            return container;
        }
    }
}
