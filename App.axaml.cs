using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Extensions.DependencyInjection;
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
            services.AddTransient(typeof(TestDIViewModel));

            //view models
            services.AddSingleton(typeof(MainWindowViewModel));
            services.AddTransient(typeof(TestDI));

            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }


        private void CustomLocatorStartup()
        {
            var locator = new ViewLocatorCustom();
            DataTemplates.Add(locator);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var services = new ServiceCollection();
                ConfigureViewModels(services);
                ConfigureViews(services);
                var provider = services.BuildServiceProvider();

                Ioc.Default.ConfigureServices(provider);

                var vm = Ioc.Default.GetService<MainWindowViewModel>();
                var view = (Window)locator.Build(vm);
                view.DataContext = vm;

                desktop.MainWindow = view;
            }
        }
        private void CustomDIStartupNoLocator()
        {
            ConfigureDependencyInjection();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {               
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }
        }


        private void CustomDIOriginalLocator()
        {           

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {

            ///In All cases check App.axaml.cs

            //Custom locator DI
            //CustomLocatorStartup();

            //Custom DI without locator at all
            //CustomDIStartupNoLocator();

            //Custom DI with original locator
            CustomDIOriginalLocator();

            base.OnFrameworkInitializationCompleted();
        }


        //Enable this only with custom locator
        [Singleton(typeof(MainWindowViewModel))]
        [Transient(typeof(TestDIViewModel))]
        internal static partial void ConfigureViewModels(IServiceCollection services);


        [Singleton(typeof(MainWindow))]
        [Transient(typeof(TestDI))]
        internal static partial void ConfigureViews(IServiceCollection services);
    }
}

