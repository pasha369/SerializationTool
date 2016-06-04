using System.Windows;
using Autofac;
using SerializationTool.Startup;
using SerializationTool.ViewModels;

namespace SerializationTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationViewModel _navigationViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AutoMapperConfig.RegisterMappings();
            AutofacConfiguration.Config();
        }
    }
}
