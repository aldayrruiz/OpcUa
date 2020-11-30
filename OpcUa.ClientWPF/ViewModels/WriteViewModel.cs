using Opc.UaFx;
using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class WriteViewModel : ViewModelBase
    {
        public WriteNodeCommand WriteNodeCommand { get; set; }

        public WriteViewModel(IClientStore clientStore)
        {
            NodeId = "ns=2;s=Machine/Job/Speed";
            WriteNodeCommand = new WriteNodeCommand(this, clientStore);
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

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string StatusMessage { get; set; }

        private OpcStatus _status;
        public OpcStatus Status 
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                StatusMessage = Status?.Description;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
    }
}
