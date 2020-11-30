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
        public readonly IConnector _connector;
        private readonly IClientStore _clientStore;
        public ServerAddressViewModel ServerAddressViewModel { get; set; }

        public ClientConnectCommand(IConnector connector, ServerAddressViewModel serverAddressViewModel, IClientStore clientStore)
        {
            _connector = connector;
            ServerAddressViewModel = serverAddressViewModel;
            _clientStore = clientStore;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            _connector.Connect(ServerAddressViewModel.ServerAddress);
            ServerAddressViewModel.State = _clientStore.CurrentClient.State;
        }
    }
}
