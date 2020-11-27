using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.Server
{
    [OpcDataType(id: "TemperatureSensorStatus", namespaceIndex:2)]
    internal enum TemperatureSensorStatus : int
    {
        [OpcEnumMember("Started", Description = "Temperature sensor is working.")]
        Started = 0,

        [OpcEnumMember("Stopped", Description = "Temperature sensor has been stopped.")]
        Stopped = 1,

        [OpcEnumMember("Maintenance", Description = "Temperature sensor is maintenance now.")]
        Maintenance = 2
    }
}
