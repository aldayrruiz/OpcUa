using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.ViewModels;

namespace OpcUa.ClientWPF.Commands
{
    public class SubscribeNodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private OpcSubscription _subscription;
        private readonly IClientStore _clientStore;
        private readonly Func<object, bool> _canExecute;
        private readonly SubscribeViewModel _subscribeViewModel;

        public SubscribeNodeCommand(IClientStore clientStore, SubscribeViewModel subscribeViewModel, Func<object, bool> canExecute)
        {
            _clientStore = clientStore;
            _subscribeViewModel = subscribeViewModel;
            _canExecute = canExecute;
        }

        private OpcClient Client => _clientStore.CurrentClient;

        private void SetNodeStatus(string value)
        {
            _subscribeViewModel.NodeStatus = value;
        }

        private void SetNodeValue(object value)
        {
            _subscribeViewModel.NodeValue = value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            try
            {
                _subscription = Client.SubscribeDataChange(_subscribeViewModel.NodeId, HandleDataChangeReceived);

                _subscription.PublishingInterval = 2000;
                _subscription.ApplyChanges();

                var monitoredItem = _subscription.MonitoredItems[0];
                var monitoredItemStatus = monitoredItem.Status;

                if (monitoredItemStatus.Error != null)
                    SetNodeStatus(monitoredItemStatus.Error.Description);
            }
            catch (Exception ex)
            {
                SetNodeStatus(ex.Message);
            }
        }

        private void HandleDataChangeReceived(object sender, OpcDataChangeReceivedEventArgs e)
        {
            var value = e.Item.Value;

            SetNodeStatus(value.Status.Description);
            SetNodeValue($"{value.Value} ({e.MonitoredItem.NodeId})");
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
