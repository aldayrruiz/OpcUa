using Opc.UaFx;
using Opc.UaFx.Client;
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
        private WriteViewModel WriteViewModel { get; set; }

        public WriteNodeCommand(WriteViewModel writeViewModel)
        {
            WriteViewModel = writeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using(OpcClient client = new OpcClient(WriteViewModel.Address))
            {
                OpcStatus status = client.WriteNode(WriteViewModel.NodeId, parameter);

                if (status.IsGood)
                {
                    // Show good message
                }
                else
                {
                    // Show error message
                }
            }
        }
    }
}
