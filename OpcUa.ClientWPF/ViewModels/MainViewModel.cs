using OpcUa.ClientWPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
