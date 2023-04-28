using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PhotoEnhancer
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();


            mainForm.AddFilter(new PixelFilter<RangeDecreasingParameters>(
                "Сужение диапазона",
                (pixel, parameters) =>
                {
                    var l1 = parameters.LowerLevel;
                    var l2 = parameters.UpperLevel;
                    return new Pixel(
                        pixel.R * (l2 - l1) + l1,
                        pixel.G * (l2 - l1) + l1,
                        pixel.B * (l2 - l1) + l1
                    );
                }));
            mainForm.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/затемнение",
                (pixel, parameters) => pixel * parameters.Coefficient));

            mainForm.AddFilter(new PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (pixel, parameters) =>
                {
                    var lightness = 0.3 * pixel.R + 0.6 * pixel.G + 0.1 * pixel.B;
                    return new Pixel(lightness, lightness, lightness);
                }));
            mainForm.AddFilter(new TransformFilter(
                "Квадратура",
                (size) =>
                {
                    var quadra = Math.Min(size.Height, size.Width);
                    return new Size(quadra, quadra);
                },
                (point, size) =>
                {
                    if (size.Width >= size.Height)
                    {
                        double ratio = (double)size.Width / (double)size.Height;
                        return new Point(Convert.ToInt32((double)point.X * ratio), point.Y);
                    }
                    else
                    {
                        double ratio = (double)size.Height / (double)size.Width;
                        return new Point(point.X, Convert.ToInt32((double)point.X / ratio));
                    }
                }
                ));

            mainForm.AddFilter(new TransformFilter(
                "Отражение по горизонтали",
                size => size,
                (point, size) => new Point(size.Width - point.X - 1, point.Y)
                ));

            mainForm.AddFilter(new TransformFilter(
                "Поворот на 90° против ч. с.",
                size => new Size(size.Height, size.Width),
                (point, size) => new Point(size.Width - point.Y - 1, point.X)
                ));

            mainForm.AddFilter(new TransformFilter<RotationParameters>(
                "Поворот на произвольный угол",
                new RotateTransformer()
                ));

            Application.Run(mainForm);
        }
    }
}
