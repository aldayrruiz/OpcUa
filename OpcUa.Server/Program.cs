using Opc.UaFx.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace OpcUa.Server
{
    class Program
    {

        public static void Main()
        {
            IList<string> uris = new List<string>
            {
                "opc.tcp://localhost:4840/",
                "https://localhost/"
            };

            OpcNodeManager nodeManager = new MyNodeManager();

            ServerStarter serverStarter = new ServerStarter(uris, nodeManager);

            serverStarter.StartServer();
        }
    }
}
