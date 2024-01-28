using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Test_DependencyInjection.ViewModels;

namespace Test_DependencyInjection.Views;

public partial class TestDI : UserControl
{
    public TestDI()
    {
        InitializeComponent();
        try
        {
            DataContext = Ioc.Default.GetService<TestDIViewModel>();
        }
        catch (System.Exception)
        {
         
        }
    }
}