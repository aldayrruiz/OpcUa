using Opc.UaFx.Server;
using System;
using System.Threading;

namespace OpcUa.Server
{
    class Program
    {
        public static void Main()
        {

            var uri = "opc.tcp://localhost:4840/";
            OpcNodeManager nodeManager = new MyNodeManager();
            using (var server = new OpcServer(uri, nodeManager))
            {
                server.Start();
                Console.WriteLine("Server started: " + uri);
                while (true)
                {

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
