using Opc.UaFx.Client;
using OpcUa.ClientWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OpcUa.ClientWPF.Commands
{
    public class CallNodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public CallViewModel CallViewModel;

        public CallNodeCommand(CallViewModel callViewModel)
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

                object[] result = client.CallMethod(
                    "ns=2;s=Machine",                  /* NodeId of Owner Node */
                    "ns=2;s=Machine/StopMachine",      /* NodeId of Method Node */
                    "Job Change",                      /* Parameter 1: 'reason' */
                    10023,                             /* Parameter 2: 'reasonCode' */
                    DateTime.Now                       /* Parameter 3: 'scheduleDate' */);
            }
        }
    }
}
