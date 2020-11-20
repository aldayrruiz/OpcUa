using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class ReadViewModel : ViewModelBase
    {

        public ReadNodeCommand ReadNodeCommand { get; set; }
        public NodeAttributesViewModel NodeAttributesViewModel { get; set; }
        public ReadViewModel()
        {
            Address = "opc.tcp://localhost:4840";
            ReadNodeCommand = new ReadNodeCommand(this);
            NodeAttributesViewModel = new NodeAttributesViewModel();
        }

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _nodeId;
        public string NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                _nodeId = value;
                OnPropertyChanged(nameof(NodeId));
            }
        }
    }
}
