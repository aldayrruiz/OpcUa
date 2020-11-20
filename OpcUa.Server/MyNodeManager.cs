using Opc.UaFx;
using Opc.UaFx.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.Server
{
    public class MyNodeManager : OpcNodeManager
    {
        public MyNodeManager() : base("http://ServerApp/nodes")
        {

        }
        
        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {

            var machine = new OpcFolderNode(DefaultNamespace.GetName("Machine"));
            
            var job = new OpcFolderNode(machine, "Job");

            var number = new OpcDataVariableNode<string>(job, "Number", value: "RTX-2070");
            var name = new OpcDataVariableNode<string>(job, "Name", value: "JobName");
            var speed = new OpcDataVariableNode<double>(job, "Speed", value: 123);
            var test = new OpcDataVariableNode<int[]>(job, "Test", value: new int[] { 1, 2, 3 });

            number.ReadVariableValueCallback = HandleReadVariableValue;
            number.WriteVariableValueCallback = HandleWriteVariableValue;

            var accelerate = new OpcMethodNode(
                speed,
                "Accelerate",
                new Func<int, int>(Accelerate));

            references.Add(machine, OpcObjectTypes.ObjectsFolder);
            yield return machine;
        }

        private int Accelerate(int increaseNumber)
        {
            Console.WriteLine("Method called: " + increaseNumber);

            return 3;
        }
        /*
        protected override bool IsNodeAccessible(OpcContext context, OpcNodeId viewId, IOpcNodeInfo node)
        {
            // DefaultNamespace.GetName("Machine") access this method with context.Identity == null
            // delegate work to default system
            if (context.Identity == null)
            {
                return base.IsNodeAccessible(context, viewId, node);
            }
            
            if (context.Identity.DisplayName == "Anonymous" && node.Name.Value == "Job")
                return true;
            else if (context.Identity.DisplayName == "Anonymous" && node.Name.Value == "Speed")
                return true;
            
            
            return base.IsNodeAccessible(context, viewId, node);
        }*/
        

        // Activate when node variable is read
        private OpcVariableValue<object> HandleReadVariableValue(
                OpcReadVariableValueContext context,
                OpcVariableValue<object> value)
        {
            if (context.Identity != null)
            {
                Console.WriteLine(
                        "\t[{0} (SID='{1}')] Read: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value);
            }
            return value;
        }

        private OpcVariableValue<object> HandleWriteVariableValue(
        OpcWriteVariableValueContext context,
        OpcVariableValue<object> value)
        {
            if (context.Identity != null)
            {
                Console.WriteLine(
                        "\t[{0} (SID='{1}')] Write: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value);
            }
            return value;
        }
    }
}
