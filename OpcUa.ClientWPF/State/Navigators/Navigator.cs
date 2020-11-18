using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.Models;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                // Tells UI CurrentViewModel changed and it needs to be updated!
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
