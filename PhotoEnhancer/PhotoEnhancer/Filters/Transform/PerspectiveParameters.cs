using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters.Transform
{
    public class PerspectiveParameters : IParameters
    {
        public double NCoefficient { get; set; }
        public ParameterInfo[] GetDecription()
        {
            return new[]
            {
                new ParameterInfo()
                {
                    Name = "N-параметр (ширина суженой верхней части)",
                    MinValue = 0,
                    MaxValue = 1.0,
                    DefaultValue = 1.0,
                    Increment = 0.05
                }
            };
        }

        public void SetValues(double[] values)
        {
            NCoefficient = values[0];
        }
    }
}
