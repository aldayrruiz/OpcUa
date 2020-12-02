using Opc.UaFx;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.ViewModels;
using OpcUa.ClientWPF.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OpcUa.ClientWPF.State.Connectors
{
    public class Connector : IConnector
    {
        private readonly IClientStore _clientStore;

        public Connector(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public OpcClient CurrentClient
        {
            get
            {
                return _clientStore.CurrentClient;
            }
            set
            {
                _clientStore.CurrentClient = value;
                // Send notification
            }
        }

        public void Connect(string serverAddress)
        {
            CurrentClient = new OpcClient(serverAddress);
            try
            {
                CurrentClient.Connect();
            }
            catch (OpcException e)
            {
                Window popup = new InvalidServerAddress();
                popup.ShowDialog();
            }
        }

        public void Disconnect()
        {
            CurrentClient = null;
        }
    }
}
