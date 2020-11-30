using Opc.UaFx.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.State.Clients
{
    public interface IClientStore
    {
        OpcClient CurrentClient { get; set; }
    }
}
