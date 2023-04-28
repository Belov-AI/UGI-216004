using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters.Transform
{
    public class PerspectiveTransformer : ITransformer<PerspectiveParameters>
    {
        public Size ResultSize { get; set; }
        public double N { get; set; }
        public void Initialize(Size size, PerspectiveParameters parameters)
        {
            ResultSize = size;
            N = parameters.NCoefficient;
        }
        public Point? MapPoint(Point p)
        {
            var yProgress = (double)p.Y / ResultSize.Height;
            var cutRatio = ((double)1.0 - N) * ((double)1.0 - yProgress); 
            // общий процент от строки растра, который надо вырезать
            var currStart = (int)((cutRatio) * ResultSize.Width / 2);
            var currFinish = ResultSize.Width - currStart;
            // начало и конец отрисовки на строке
            if (p.X <= currStart || p.X >= currFinish)
            {
                return new Point(0, 0); 
                // оставляем пустыми, так как находятся вне пределов отрисовки (вырезаны)
            }
            var xRatio = (double)(p.X - currStart) / (double)(currFinish - currStart);
            // позиция текущей точки относительно интервала отрисовки
            var nextX = Convert.ToInt32(ResultSize.Width * xRatio);
            // адрес отрисовываемого пикселя, взятый относительно абсолютной длины оригинала
            return new Point(nextX, p.Y);
        }
    }
}
