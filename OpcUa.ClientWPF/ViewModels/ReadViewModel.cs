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
            ReadNodeCommand = new ReadNodeCommand(this, clientStore,
                canExecute: (_) => NodeIdIsValid);
            NodeId = "ns=2;s=Machine/Job/Speed"; // Remove after test completed
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
                OnPropertyChanged(nameof(NodeIdIsValid));
                ReadNodeCommand.RaiseCanExecuteChanged();
            }
        }

        public bool NodeIdIsValid
        {
            get
            {
                if (OpcNodeId.TryParse(NodeId, out var result) && result != OpcNodeId.Null)
                {
                    return true;
                }
                return false;
            }
        }

        private string _nodeStatus;
        public string NodeStatus
        {
            get => _nodeStatus;
            set
            {
                OnPropertyChanging(nameof(NodeStatus));
                _nodeStatus = value;
                OnPropertyChanged(nameof(NodeStatus));
            }
        }

        private object _nodeValue;
        public object NodeValue
        {
            get => _nodeValue;
            set
            {
                OnPropertyChanging(nameof(NodeValue));
                _nodeValue = value;
                OnPropertyChanged(nameof(NodeValue));
            }
        }
    }
}
