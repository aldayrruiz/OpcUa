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

        public CallViewModel CallViewModel;
        private readonly IClientStore _clientStore;
        private OpcClient Client
        {
            get
            {
                return _clientStore.CurrentClient;
            }
        }

        public CallMethodNodeCommand(CallViewModel callViewModel, IClientStore clientStore)
        {
            CallViewModel = callViewModel;
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
                        "ns=2;s=Machine/Calculator/" + CallViewModel.Method,     /* NodeId of Method Node */
                        CallViewModel.X                                                 /* 2º parameter */,
                        CallViewModel.Y                                                 /* 1º parameter */);

                    CallViewModel.Result = string.Format("{0}", result.GetValue(0));

                    CallViewModel.ErrorMessage = string.Empty;

                }
                catch (OpcException e)
                {
                    CallViewModel.ErrorMessage = e.Message;
                }
            }
        }
    }
}
