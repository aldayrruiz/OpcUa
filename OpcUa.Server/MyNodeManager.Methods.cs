using Opc.UaFx;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.Server
{
    partial class MyNodeManager
    {
        [return: OpcArgument("add", Description = "The result of addition.")]
        private double Add(
            [OpcArgument("x (summand)", Description = "The first summand.")]
            double x,
            [OpcArgument("y (summand)", Description = "The second summand.")]
            double y)
        {
            return x + y;
        }


        [return: OpcArgument("substract", Description = "The result of substraction.")]
        private double Substract(
            [OpcArgument("x (minuend)", Description = "The number which is decreased.")]
            double x,
            [OpcArgument("y (subtraend)", Description = "The number which is substracted.")]
            double y)
        {
            return x - y;
        }

        [return: OpcArgument("multiply", Description = "The result of multiplication.")]
        private double Multiply(
            [OpcArgument("x (multiplycand)", Description = "The number which is multiplied (= the first factor).")]
            double x,
            [OpcArgument("y (multiplier)", Description = "The number of times the multiplicant is multiplied (= the second factor).")]
            double y)
        {
            return x * y;
        }

        [return: OpcArgument("divide", Description = "The result of division.")]
        private double Divide(
            [OpcArgument("x (dividend)", Description = "The number which is divided.")]
            double x,
            [OpcArgument("y (divisor)", Description = "The number which divides the dividend.")]
            double y)
        {
            return x / y;
        }
    }
}
