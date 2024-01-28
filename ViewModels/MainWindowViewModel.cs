namespace Test_DependencyInjection.ViewModels
{
    public partial class MainWindowViewModel(UCTest2ViewModel ucTest2ViewModel) : ViewModelBase
    {
        public UCTest2ViewModel UcTest2ViewModel { get; } = ucTest2ViewModel;
    }
}
