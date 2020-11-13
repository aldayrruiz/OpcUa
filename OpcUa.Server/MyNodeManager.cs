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
            var number = new OpcDataVariableNode<string>("Number", value: "RTX-2070");

            number.ReadVariableValueCallback = HandleReadVariableValue;
            number.WriteVariableValueCallback = HandleWriteVariableValue;

            var job = new OpcObjectNode(
                "Job",
                number,
                new OpcDataVariableNode<string>("Name", value: "JobName"),
                new OpcDataVariableNode<double>("Speed", value: 123));



            var machine = new OpcObjectNode(
                    "Machine",
                    job);

            references.Add(machine, OpcObjectTypes.ObjectsFolder);
            yield return machine;
        }

        protected override bool IsNodeAccessible(OpcContext context, OpcNodeId viewId, IOpcNodeInfo node)
        {
            if (context.Identity.DisplayName == "Anonymous" && node.Name.Value == "Job")
                return true;
            else if (context.Identity.DisplayName == "Anonymous" && node.Name.Value == "Speed")
                return true;

            return base.IsNodeAccessible(context, viewId, node);
        }


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
