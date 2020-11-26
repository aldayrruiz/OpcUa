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
            using (var client = new OpcClient("https://localhost:4840"))
            {
                client.Connect();
                Console.WriteLine("Connected");
                var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
                while (true)
                {

                    Console.Write("(B/R/W/S) Browse/Read/Write/Subscribe nodes: ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "B": Browse(client); break;
                        case "R": ReadNodes(client); break;
                        case "W": WriteNodes(client); break;
                        case "S": SubscribeNode(client); break;
                        default: break;
                    }

                }
            }
        }

        public static void Browse(OpcClient client)
        {
            // Do not show Server node and its children
            OpcNodeInfo machineNode = client.BrowseNode("ns=2;s=Machine");
            Browse(machineNode);
        }

        public static void Browse(OpcNodeInfo node, int level = 0)
        {
            foreach (var child in node.Children())
            {
                switch (child.Category)
                {
                    case OpcNodeCategory.Unspecified:
                    case OpcNodeCategory.Object:
                        DisplayNode(child, level++);
                        Browse(child, level); break;
                    case OpcNodeCategory.Variable:
                        DisplayNode(child, level); break;

                    case OpcNodeCategory.Method:
                        DisplayMethodNode((OpcMethodNodeInfo)child, level); break;

                    case OpcNodeCategory.ObjectType:
                    case OpcNodeCategory.VariableType:
                    case OpcNodeCategory.ReferenceType:
                    case OpcNodeCategory.DataType:
                    case OpcNodeCategory.View:
                        DisplayNode(child, level); break;
                    default:
                        break;
                }
            }
        }

        // Display a node of type general by the moment
        public static void DisplayNode(OpcNodeInfo node, int level = 0)
        {
            Console.WriteLine("{0}{1}: {2}", new string('.', level++ * 2), node.DisplayName, node.Category);
        }

        // Display a node of type method
        public static void DisplayMethodNode(OpcMethodNodeInfo node, int level = 0)
        {
            Console.WriteLine("{0}METHOD {1} args:", new string('.', level++ * 2), node.DisplayName);
            int argN = 0;
            foreach (var argument in node.GetInputArguments())
            {
                Console.WriteLine("{0} arg{1}: {2}", new string('.', level * 2), ++argN, argument.Name);
            }
        }

        public static void ReadNodes(OpcClient client)
        {
            OpcReadNode[] commands = new OpcReadNode[] {
                        new OpcReadNode("ns=2;s=Machine/Job/Number"),
                        new OpcReadNode("ns=2;s=Machine/Job/Name"),
                        new OpcReadNode("ns=2;s=Machine/Job/Speed"),
                        new OpcReadNode("ns=2;s=Machine/Job/Test"),
            };

            IEnumerable<OpcValue> nodes = client.ReadNodes(commands);
            foreach (var node in nodes)
            {
                if (node.Status.IsGood)
                {
                    Console.WriteLine("* " + node);
                }
            }
        }

        public static void WriteNodes(OpcClient client)
        {
            var rand = new Random();
            int randomNumber = rand.Next();

            OpcWriteNode[] commands = new OpcWriteNode[] {
                // new OpcWriteNode("ns=2;s=Machine/Job/Number", OpcAttribute.DisplayName, new OpcText("Serial")),
                // new OpcWriteNode("ns=2;s=Machine/Job/Name", OpcAttribute.DisplayName, new OpcText("Description")),
                new OpcWriteNode("ns=2;s=Machine/Job/Speed", randomNumber)
            };

            OpcStatusCollection results = client.WriteNodes(commands);

            Console.WriteLine("Changing ns=2;s=Machine/Job/Speed value for: " + randomNumber);

            foreach (var result in results)
            {
                if (result.IsBad)
                {
                    Console.WriteLine("Failed to write: " + result.Description);
                }
                
            }
        }

        public static void SubscribeNode(OpcClient client)
        {
            OpcSubscription subscription = client.SubscribeDataChange(
                    "ns=2;s=Machine/Job/Speed",
                    HandleDataChanged);
            subscription.PublishingInterval = 2000;
            subscription.ApplyChanges();
        }

        private static void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
        {
            OpcMonitoredItem item = (OpcMonitoredItem)sender;

            Console.WriteLine(
                    "Data Change from NodeId '{0}': {1}",
                    item.NodeId,
                    e.Item.Value);
        }
    }
}
