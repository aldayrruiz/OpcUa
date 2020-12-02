using OpcUa.ClientWPF.State;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.State.Connectors;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.Commands
{
    public class ClientConnectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly IConnector _connector;
        private readonly IClientStore _clientStore;
        private readonly Func<object, bool> _canExecute;
        private readonly ServerAddressViewModel _serverAddressViewModel;
        
        public ClientConnectCommand(IConnector connector, ServerAddressViewModel serverAddressViewModel, IClientStore clientStore, Func<object, bool> canExecute)
        {
            _connector = connector;
            _serverAddressViewModel = serverAddressViewModel;
            _clientStore = clientStore;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter)
        {
            _connector.Connect(_serverAddressViewModel.ServerAddress);
            _serverAddressViewModel.State = _clientStore.CurrentClient.State;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
