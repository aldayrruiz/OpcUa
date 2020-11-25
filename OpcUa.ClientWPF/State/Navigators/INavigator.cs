using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.State.Navigators
{
    public enum ViewType
    {
        Read,
        Write,
        Call,
        Subscribe
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }

        //event Action StateChanged;
    }
}
