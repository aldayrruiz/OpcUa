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
        private readonly WriteViewModel _writeViewModel;
        private readonly IClientStore _clientStore;
        private OpcClient Client => _clientStore.CurrentClient;

        public WriteNodeCommand(WriteViewModel writeViewModel, IClientStore clientStore)
        {
            _writeViewModel = writeViewModel;
            _clientStore = clientStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _writeViewModel.Status = Client?.WriteNode(_writeViewModel.NodeId, _writeViewModel.Value);
        }
    }
}
