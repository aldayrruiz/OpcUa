using Opc.UaFx.Client;
using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.State.Connectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class ServerAddressViewModel : ViewModelBase
    {
        // TODO: Fix problem when serverAddress is equal null, empty or invalid Uri
        private string _serverAddress;
        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
                OnPropertyChanged(nameof(IsAddressValid));
                ClientConnectCommand.RaiseCanExecuteChanged();
            }
        }

        private bool IsAddressValid => ValidateAddressServer(ServerAddress);

        private OpcClientState _state;
        public OpcClientState State
        {
            get
            {
                return _state == OpcClientState.Connected ? OpcClientState.Connected : OpcClientState.Disconnected;
            }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public ClientConnectCommand ClientConnectCommand { get; set; }

        public ServerAddressViewModel(IConnector connector, IClientStore clientStore)
        {
            ClientConnectCommand = new ClientConnectCommand(connector, this, clientStore, 
                canExecute: (_) => IsAddressValid);
        }

        private bool ValidateAddressServer(string addressServer)
        {
            if (Uri.TryCreate(addressServer, UriKind.Absolute, out var result)
                && (result.Scheme == "opc.tcp" || result.Scheme == "https"))
            {
                return true;
            }
            return false;
        }
    }
}
