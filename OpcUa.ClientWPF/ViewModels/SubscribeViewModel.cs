using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    // TODO: Use ServerAddress of ServerAddressViewModel
    public class SubscribeViewModel : ViewModelBase
    {
        public SubscribeNodeCommand SubscribeNodeCommand { get; set; }
        
        public SubscribeViewModel(IClientStore clientStore)
            : base()
        {
            _nodeId = "ns=2;s=Machine/Job/Speed";
            _nodeStatus = "Enter a node identifier and click 'Subscribe'.";
            SubscribeNodeCommand = new SubscribeNodeCommand(clientStore, this, 
                    canExecute: (_) => NodeIdIsValid);
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
                SubscribeNodeCommand.RaiseCanExecuteChanged();
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
