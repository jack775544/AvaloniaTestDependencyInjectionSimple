using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Test_DependencyInjection.ViewModels;
using Test_DependencyInjection.Views;

namespace Test_DependencyInjection
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }


        void ConfigureDependencyInjection()
        {
            //services
            var services = new ServiceCollection();
            //services.AddTransient<IAPIService, APIService>();

            //views
            services.AddSingleton(typeof(MainWindow));
            services.AddTransient(typeof(UCTest2View));

            //view models
            services.AddSingleton(typeof(MainWindowViewModel));
            services.AddTransient(typeof(UCTest2ViewModel));

            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }

        private void CustomDIOriginalLocator()
        {
            ConfigureDependencyInjection();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>(),
                };
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            CustomDIOriginalLocator();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
