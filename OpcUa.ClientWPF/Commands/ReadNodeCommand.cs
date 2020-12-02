using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.Commands
{
    public class ReadNodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ReadViewModel _readViewModel;
        private readonly IClientStore _clientStore;
        private readonly Func<object, bool> _canExecute;

        private OpcClient Client => _clientStore.CurrentClient;

        public ReadNodeCommand(ReadViewModel readViewModel, IClientStore clientStore, Func<object, bool> canExecute)
        {
            _readViewModel = readViewModel;
            _clientStore = clientStore;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute(parameter);

        public void Execute(object parameter)
        {
            if (Client?.State == OpcClientState.Connected)
            {
                if (!string.IsNullOrEmpty(_readViewModel.NodeId))
                {
                    string nodeId = _readViewModel.NodeId;

                    // Read Attributes 
                    OpcValue value = Client.ReadNode(nodeId);
                    OpcValue displayName = Client.ReadNode(nodeId, OpcAttribute.DisplayName);

                    if (value.Status.IsGood && displayName.Status.IsGood)
                    {
                        _readViewModel.NodeAttributesViewModel = NodeAttributesViewModel.LoadNodeAttributes(displayName, value);
                    }
                    else
                    {
                        _readViewModel.NodeStatus = value.Status.Description;
                    }
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
