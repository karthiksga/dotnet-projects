using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomComponents
{
    /// <summary>
    /// Summary description for Math
    /// </summary>
    public class Math
    {
        public Math()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public double Divide(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException();
            return x / y;
        }
    }
}
