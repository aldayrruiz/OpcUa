using Opc.UaFx.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.State.Clients
{
    public class ClientStore : IClientStore
    {
        public OpcClient CurrentClient { get; set; }
    }
}
