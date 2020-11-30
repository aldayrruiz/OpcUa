using Opc.UaFx.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpcUa.ClientWPF.State.Connectors
{
    public interface IConnector
    {
        void Connect(string serverAddress);

        void Disconnect();
    }
}
