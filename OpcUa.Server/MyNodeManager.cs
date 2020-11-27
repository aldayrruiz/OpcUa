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

            // Creates machine node and adds it to root node
            var machine = new OpcFolderNode(DefaultNamespace.GetName("Machine"));
            references.Add(machine, OpcObjectTypes.ObjectsFolder);

            // Creates and adds machine status to machine node
            new OpcDataVariableNode<MachineStatus>(machine, name: "Status", value: MachineStatus.Started);

            // Creates calculator and set its methods
            var calculator = new OpcObjectNode(machine, name: "Calculator");
            SetCalculatorMethods(calculator);

            // Creates job node and adds its children (number, name, speed)
            var job = new OpcFolderNode(machine, name: "Job");
            var number = new OpcDataVariableNode<string>(job, name: "Number", value: "RTX-2070");
            var name = new OpcDataVariableNode<string>(job, name: "Name", value: "JobName");
            var speed = new OpcDataVariableNode<int>(job, name: "Speed", value: 123); ;

            // Creates a temperature sensor with variables like: temperature and status
            var temperatureSensor = new OpcObjectNode(machine, name: "TemperatureSensor");
            var temperature = new OpcDataVariableNode<int>(temperatureSensor, 
                name: "Temperature", 
                value: 20);
            new OpcDataVariableNode<TemperatureSensorStatus>(temperatureSensor, 
                name: "StatusTemperatureSensor", 
                value: TemperatureSensorStatus.Started);

            // Handle client requests (read and write).
            number.ReadVariableValueCallback = HandleReadVariableValue;
            speed.WriteVariableValueCallback = HandleWriteVariableValue;

            return new IOpcNode[] { machine, new OpcDataTypeNode<MachineStatus>() };
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
