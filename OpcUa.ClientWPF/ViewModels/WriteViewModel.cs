using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class WriteViewModel : ViewModelBase
    {
        public string Address { get; set; }
        public OpcNodeId NodeId { get; set; }
        

    }
}
