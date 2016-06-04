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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AutoMapperConfig.RegisterMappings();
            AutofacConfiguration.Config();
            
            var navigayionViewModel = DependencyResolver.Current.Resolve<NavigationViewModel>();
            var window = new MainWindow(navigayionViewModel);
            
            window.Show();
        }
    }
}
