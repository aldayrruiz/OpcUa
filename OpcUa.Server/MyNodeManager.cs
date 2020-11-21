using Opc.UaFx;
using Opc.UaFx.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.Server
{
    partial class MyNodeManager : OpcNodeManager
    {
        public MyNodeManager() : base("http://ServerApp/nodes")
        {

        }
        
        protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
        {

            var machine = new OpcFolderNode(DefaultNamespace.GetName("Machine"));
            var calculator = new OpcObjectNode(machine, "Calculator");
            SetCalculatorMethods(calculator);

            var job = new OpcFolderNode(machine, "Job");

            var number = new OpcDataVariableNode<string>(job, "Number", value: "RTX-2070");
            var name = new OpcDataVariableNode<string>(job, "Name", value: "JobName");
            var speed = new OpcDataVariableNode<int>(job, "Speed", value: 123);
            

            number.ReadVariableValueCallback = HandleReadVariableValue;
            number.WriteVariableValueCallback = HandleWriteVariableValue;
            speed.WriteVariableValueCallback = HandleWriteVariableValue;

            

            references.Add(machine, OpcObjectTypes.ObjectsFolder);
            yield return machine;
        }


        private void SetCalculatorMethods(OpcObjectNode calculator)
        {
            var add = new OpcMethodNode(calculator, "Add", new Func<double, double, double>(Add));
            var substract = new OpcMethodNode(calculator, "Substract", new Func<double, double, double>(Substract));
            var multiply = new OpcMethodNode(calculator, "Multiply", new Func<double, double, double>(Multiply));
            var divide = new OpcMethodNode(calculator, "Divide", new Func<double, double, double>(Divide));
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

            Console.WriteLine(value.Value.GetType());
            return new OpcVariableValue<object>(int.Parse(value.Value.ToString()));
        }
    }
}
