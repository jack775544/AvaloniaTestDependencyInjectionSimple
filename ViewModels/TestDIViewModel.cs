using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DependencyInjection.ViewModels
{
    partial class TestDIViewModel : ViewModelBase
    {


        [RelayCommand]
        private void ApplyData()
        {
            textCompile = "CLEARED";
        }


        [ObservableProperty]
        string textCompile = "";


    }
}
