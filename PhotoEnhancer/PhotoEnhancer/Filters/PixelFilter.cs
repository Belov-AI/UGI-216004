using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public abstract class PixelFilter : IFilter
    {
        public abstract ParameterInfo[] GetParametersInfo();

        public Photo Process(Photo original, double[] parameters)
        {
            var newPhoto = new Photo(original.Width, original.Height);
            try
            {
                for (var x = 0; x < original.Width; x++)
                    for (var y = 0; y < original.Height; y++)
                        newPhoto[x, y] = ProcessPixel(original[x, y], parameters);
            }
            catch (Exception ex)
            {
                var alert = MessageBox.Show(
                   ex.Message,
                   "Ошибка обработки; Фильтр не был применён",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation
                   );
                return original;
            }
            return newPhoto;
        }

        public abstract Pixel ProcessPixel(Pixel pixel, double[] parameters);
    }
}
