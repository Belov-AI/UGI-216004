﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        public Point TopLeft { get; set; }
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }

        public double Area { get => Width * Height; }   

        public Rectangle(double x, double y, double width, double height) 
        {
            TopLeft = new Point(x, y);
            Width = width;
            Height = height;
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}