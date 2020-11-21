using Opc.UaFx.Client;
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

        public CallMethodNodeCommand(CallViewModel callViewModel)
        {
            CallViewModel = callViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (var client = new OpcClient(CallViewModel.Address))
            {
                client.Connect();

                try
                {
                    object[] result = client.CallMethod(
                    "ns=2;s=Machine/Calculator",                                /* NodeId of Owner Node */
                    "ns=2;s=Machine/Calculator/" + CallViewModel.Method,     /* NodeId of Method Node */
                    CallViewModel.X                                                 /* 2º parameter */,
                    CallViewModel.Y                                                 /* 1º parameter */);

                    CallViewModel.Result = string.Format("{0}", result.GetValue(0));

                    CallViewModel.ErrorMessage = string.Empty;

                } catch (Opc.UaFx.OpcException e)
                {
                    CallViewModel.ErrorMessage = e.Message;
                } 
            }
        }
    }
}
