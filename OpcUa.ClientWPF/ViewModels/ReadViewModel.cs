using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class ReadViewModel : ViewModelBase
    {

        public ReadNodeCommand ReadNodeCommand { get; set; }
        private NodeAttributesViewModel _nodeAttributesViewModel;
        public NodeAttributesViewModel NodeAttributesViewModel
        {
            get
            {
                return _nodeAttributesViewModel;
            }
            set
            {
                _nodeAttributesViewModel = value;
                OnPropertyChanged(nameof(NodeAttributesViewModel));
            }
        }

        public ReadViewModel(IClientStore clientStore)
        {
            NodeId = "ns=2;s=Machine/Job/Speed";
            ReadNodeCommand = new ReadNodeCommand(this, clientStore);
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
