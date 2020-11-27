using Opc.UaFx.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OpcUa.Server
{
    public class ServerStarter
    {
        public IList<string> Uris { get; set; }
        public OpcNodeManager NodeManager { get; set; }
        public OpcServer server { get; set; }

        public ServerStarter(IList<string> uris, OpcNodeManager nodeManager)
        {
            Uris = uris;
            NodeManager = nodeManager;
        }

        public void StartServer()
        {
            using (server = new OpcServer(NodeManager))
            {
                foreach (string uri in Uris)
                {
                    server.RegisterAddress(uri);
                    Console.WriteLine("Server starting: " + uri);
                }

                server.Start();
                
                while (true)
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
