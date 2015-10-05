using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSA_Teaser.Class.ObjectIndicator
{
    public static class GraphPropertiesMethod
    {
        public static double[] CalculateAutoScaleOneAxis(double[] data)
        {
            double[] result = new double[2];
            result[0] = data.ToList<double>().Max();
            result[1] = data.ToList<double>().Min();
            return result;
        }
    }
}
