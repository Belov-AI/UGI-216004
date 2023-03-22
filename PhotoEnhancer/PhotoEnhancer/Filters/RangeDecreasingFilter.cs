using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    internal class RangeDecreasingFilter : PixelFilter
    {
        public override ParameterInfo[] GetParametersInfo()
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

        public override Pixel ProcessPixel(Pixel pixel, double[] parameters)
        {
            var l1 = parameters[0];
            var l2 = parameters[1];

            if (l1 >= l2)
            {
                throw new Exception(
                    "Нижний уровент фильтра не может быть выше или равен верхнему"
                    );
            }

            return new Pixel(
                    pixel.R * (l2 - l1) + l1,
                    pixel.G * (l2 - l1) + l1,
                    pixel.B * (l2 - l1) + l1
                );
        }

        public override string ToString()
        {
            return "Сужение диапазона";
        }
    }
}
