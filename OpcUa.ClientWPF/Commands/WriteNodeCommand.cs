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
    public class WriteNodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public  WriteViewModel WriteViewModel { get; set; }
        private readonly IClientStore _clientStore;
        private OpcClient Client
        {
            get
            {
                return _clientStore.CurrentClient;
            }
        }

        public WriteNodeCommand(WriteViewModel writeViewModel, IClientStore clientStore)
        {
            WriteViewModel = writeViewModel;
            _clientStore = clientStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            WriteViewModel.Status = Client?.WriteNode(WriteViewModel.NodeId, WriteViewModel.Value);
        }
    }
}
