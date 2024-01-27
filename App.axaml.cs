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

        public override void OnFrameworkInitializationCompleted()
        {
            var locator = new ViewLocator();
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

            base.OnFrameworkInitializationCompleted();
        }

        [Singleton(typeof(MainWindowViewModel))]
        [Transient(typeof(TestDIViewModel))]
        internal static partial void ConfigureViewModels(IServiceCollection services);


        [Singleton(typeof(MainWindow))]
        [Transient(typeof(TestDI))]
        internal static partial void ConfigureViews(IServiceCollection services);
    }
}

