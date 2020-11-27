using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.Server
{
    // Example of Traeger SDK
    [OpcDataType(id: "MachineStatus", namespaceIndex:2)]
    internal enum MachineStatus : int
    {
        [OpcEnumMember("Unkown", Description = "Machine is still not configured.")]
        Unknown = 0,

        [OpcEnumMember("Stopped", Description = "The machine has been stopped by operator.")]
        Stopped = 1,

        [OpcEnumMember("Started", Description = "The machine has started.")]
        Started = 2,

        [OpcEnumMember("Waiting", Description = "The machine is waiting for new orders.")]
        Waiting = 3,

        [OpcEnumMember("Suspended", Description = "The machine operator suspender the order processing.")]
        Suspended = 4
    }
}
