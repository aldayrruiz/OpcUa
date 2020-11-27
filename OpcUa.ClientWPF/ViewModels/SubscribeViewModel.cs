using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class SubscribeViewModel : ViewModelBase
    {
        private Uri address;
        private string addressStatus;

        private OpcClient client;
        private OpcSubscription subscription;

        private OpcNodeId nodeId;
        private string nodeStatus;
        private object nodeValue;


        public SubscribeViewModel()
            : base()
        {
            address = new Uri("https://localhost:4840");
            addressStatus = "Enter address of server.";

            nodeId = "ns=2;s=Machine/Job/Speed";
            nodeStatus = "Enter a node identifier and click 'Subscribe'.";

            SubscribeCommand = new SubscribeNodeCommand(
                    execute: (_) => Subscribe(),
                    canExecute: (_) => AddressIsValid && NodeIdIsValid);
        }

        public string Address
        {
            get => address?.ToString();
            set
            {
                OnPropertyChanging(nameof(Address));
                OnPropertyChanging(nameof(AddressIsValid));

                if (Uri.TryCreate(value, UriKind.Absolute, out var result)
                        && (result.Scheme == "opc.tcp" || result.Scheme == "https"))
                {
                    if (subscription != null)
                    {
                        if (subscription.IsCreated)
                        {
                            subscription.Unsubscribe();
                        }
                        else
                        {
                            subscription = null;
                        }
                    }
                    
                    client?.Disconnect();

                    client = null;
                    address = result;

                    OnPropertyChanged(nameof(Address));
                    OnPropertyChanged(nameof(AddressIsValid));

                    SubscribeCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    AddressStatus = "Invalid address uri.";
                }
            }
        }

        public bool AddressIsValid
        {
            get => address != null;
        }

        public string AddressStatus
        {
            get => addressStatus;
            set
            {
                OnPropertyChanging(nameof(AddressStatus));
                addressStatus = value;
                OnPropertyChanged(nameof(AddressStatus));
            }
        }

        public string NodeId
        {
            get => nodeId?.ToString();
            set
            {
                OnPropertyChanging(nameof(NodeId));
                OnPropertyChanging(nameof(NodeIdIsValid));

                if (OpcNodeId.TryParse(value, out var result))
                {
                    nodeId = result;

                    OnPropertyChanged(nameof(NodeId));
                    OnPropertyChanged(nameof(NodeIdIsValid));

                    SubscribeCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    NodeStatus = "Invalid node identifier.";
                }
            }
        }

        public bool NodeIdIsValid
        {
            get => nodeId != null && nodeId != OpcNodeId.Null;
        }

        public string NodeStatus
        {
            get => nodeStatus;
            set
            {
                OnPropertyChanging(nameof(NodeStatus));
                nodeStatus = value;
                OnPropertyChanged(nameof(NodeStatus));
            }
        }

        public object NodeValue
        {
            get => nodeValue;
            set
            {
                OnPropertyChanging(nameof(NodeValue));
                nodeValue = value;
                OnPropertyChanged(nameof(NodeValue));
            }
        }

        public SubscribeNodeCommand SubscribeCommand
        {
            get;
        }

        public void Subscribe()
        {
            try
            {
                if (client == null)
                {
                    client = new OpcClient(address);
                    client.Connect();
                }

                if (subscription != null)
                    subscription.Unsubscribe();

                subscription = client.SubscribeDataChange(
                        nodeId,
                        HandleDataChangeReceived);

                subscription.PublishingInterval = 2000;
                subscription.ApplyChanges();

                var monitoredItem = subscription.MonitoredItems[0];
                var monitoredItemStatus = monitoredItem.Status;

                if (monitoredItemStatus.Error != null)
                    NodeStatus = monitoredItemStatus.Error.Description;
            }
            catch (Exception ex)
            {
                NodeStatus = ex.Message;
            }
        }

        private void HandleDataChangeReceived(object sender, OpcDataChangeReceivedEventArgs e)
        {
            var value = e.Item.Value;

            NodeStatus = value.Status.Description;
            NodeValue = $"{value.Value} ({e.MonitoredItem.NodeId})";
        }
    }

}
