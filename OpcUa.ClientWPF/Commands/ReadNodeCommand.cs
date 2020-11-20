using Opc.UaFx;
using Opc.UaFx.Client;
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
        public OpcClient Client;

        public ReadNodeCommand(ReadViewModel readViewModel)
        {
            ReadViewModel = readViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (Client = new OpcClient(ReadViewModel.Address))
            {
                Client.Connect();

                if (!string.IsNullOrEmpty(ReadViewModel.NodeId))
                {
                    string nodeId = ReadViewModel.NodeId;

                    // Read Attributes 
                    OpcValue value = Client.ReadNode(nodeId);
                    OpcValue displayName = Client.ReadNode(nodeId, OpcAttribute.DisplayName);

                    if (value.Status.IsGood && displayName.Status.IsGood)
                    {
                        ReadViewModel.NodeAttributesViewModel.setAttributes(
                            displayName: displayName.Value.ToString(),
                            value: value.Value.ToString(),
                            serverTimeStamp: value.ServerTimestamp.ToString()
                        );
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
