using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    internal class RangeDecreasingParameters : IParameters
    {
        public double UpperLevel { get; set; }
        public double LowerLevel { get; set; }



        public ParameterInfo[] GetDecription()
        {
            return new[]
            {
                new ParameterInfo()
                {
                    Name = "Нижний уровень",
                    MinValue = 0,
                    MaxValue = 1,
                    DefaultValue = 0,
                    Increment = 0.01
                },
                new ParameterInfo()
                {
                    Name = "Верхний уровень",
                    MinValue = 0,
                    MaxValue = 1,
                    DefaultValue = 1,
                    Increment = 0.01
                }
            };
        }

        public void SetValues(double[] values)
        {
            var l1 = values[0];
            var l2 = values[1];
            if (l1 >= l2)
            {
                throw new Exception(
                    "Нижний уровент фильтра не может быть выше или равен верхнему"
                    );
            }
            LowerLevel = l1;
            UpperLevel = l2;
        }

    }
}

