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
    public class CallMethodNodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly CallViewModel _callViewModel;
        private readonly IClientStore _clientStore;
        private OpcClient Client => _clientStore.CurrentClient;

        public CallMethodNodeCommand(CallViewModel callViewModel, IClientStore clientStore)
        {
            _callViewModel = callViewModel;
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
                try
                {
                    object[] result = Client.CallMethod(
                        "ns=2;s=Machine/Calculator",                                /* NodeId of Owner Node */
                        "ns=2;s=Machine/Calculator/" + _callViewModel.Method,     /* NodeId of Method Node */
                        _callViewModel.X                                                 /* 2º parameter */,
                        _callViewModel.Y                                                 /* 1º parameter */);

                    _callViewModel.Result = string.Format("{0}", result.GetValue(0));

                    _callViewModel.ErrorMessage = string.Empty;

                }
                catch (OpcException e)
                {
                    _callViewModel.ErrorMessage = e.Message;
                }
            }
        }
    }
}
