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
        public  WriteViewModel WriteViewModel { get; set; }

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
                client.Connect();
                WriteViewModel.Status = client.WriteNode(WriteViewModel.NodeId, WriteViewModel.Value);
            }
        }
    }
}
