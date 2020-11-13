using Opc.UaFx;
using Opc.UaFx.Client;
using System;
using System.Collections.Generic;

namespace OpcUa.Client
{
    class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840"))
            {
                client.Connect();
                Console.WriteLine("Connected");
                while (true)
                {

                    Console.WriteLine("(R/W) Read/Write nodes: ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "R": readNodes(client); break;
                        case "W": writeNodes(client); break;
                        default: break;
                    }

                }
            }
        }

        public static void readNodes(OpcClient client)
        {
            OpcReadNode[] commands = new OpcReadNode[] {
                        new OpcReadNode("ns=2;s=Machine/Job/Number"),
                        new OpcReadNode("ns=2;s=Machine/Job/Name"),
                        new OpcReadNode("ns=2;s=Machine/Job/Speed"),
            };

            IEnumerable<OpcValue> nodes = client.ReadNodes(commands);
            foreach (var node in nodes)
            {
                Console.WriteLine(node);
            }
        }

        public static void writeNodes(OpcClient client)
        {
            OpcWriteNode[] commands = new OpcWriteNode[] {
                new OpcWriteNode("ns=2;s=Machine/Job/Number", OpcAttribute.DisplayName, new OpcText("Serial")),
                new OpcWriteNode("ns=2;s=Machine/Job/Name", OpcAttribute.DisplayName, new OpcText("Description")),
                new OpcWriteNode("ns=2;s=Machine/Job/Speed", OpcAttribute.DisplayName, new OpcText("Rotations per Second"))
            };

            OpcStatusCollection results = client.WriteNodes(commands);

            foreach (var result in results)
            {
                Console.WriteLine(result.Description);
            }
        }
    }
}
