using OpcUa.ClientWPF.State.Navigators;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;
        //private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                // _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                switch (viewType)
                {
                    case ViewType.Read:
                        _navigator.CurrentViewModel = new ReadViewModel(); break;
                    case ViewType.Write:
                        _navigator.CurrentViewModel = new WriteViewModel(); break;
                    case ViewType.Call:
                        _navigator.CurrentViewModel = new CallViewModel(); break;
                    case ViewType.Subscribe:
                        _navigator.CurrentViewModel = new SubscribeViewModel(); break;
                    default:
                        break;
                }
            }
        }
    }
}
