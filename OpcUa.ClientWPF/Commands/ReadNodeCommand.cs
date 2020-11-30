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
        public ReadViewModel ReadViewModel;
        private readonly IClientStore _clientStore;
        private OpcClient Client
        {
            get
            {
                return _clientStore.CurrentClient;
            }
        }

        public ReadNodeCommand(ReadViewModel readViewModel, IClientStore clientStore)
        {
            ReadViewModel = readViewModel;
            _clientStore = clientStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Client?.State == OpcClientState.Connected)
            {
                if (!string.IsNullOrEmpty(ReadViewModel.NodeId))
                {
                    string nodeId = ReadViewModel.NodeId;

                    // Read Attributes 
                    OpcValue value = Client.ReadNode(nodeId);
                    OpcValue displayName = Client.ReadNode(nodeId, OpcAttribute.DisplayName);

                    if (value.Status.IsGood && displayName.Status.IsGood)
                    {
                        ReadViewModel.NodeAttributesViewModel = NodeAttributesViewModel.LoadNodeAttributes(displayName, value);
                    }
                    else
                    {
                        // Show a message error
                    }
                }
            }
        }
    }
}
