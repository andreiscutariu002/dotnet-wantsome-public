namespace Abstraction
{
    using System;

    internal abstract class Figure
    {
        public Figure()
        {
        }

        public Figure(double radius)
        {
            this.Radius = radius;
        }

        public Figure(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public virtual double Width { get; set; }
        public virtual double Height { get; set; }
        public virtual double Radius { get; set; }
    }

    internal abstract class Figure2
    {
        public abstract double CalcSurface();
        public abstract double CalcPerimeter();
    }

    internal class Circle2 : Figure2
    {
        private readonly double radius;

        public Circle2(double radius)
        {
            this.radius = radius;
        }

        public override double CalcSurface()
        {
            throw new NotImplementedException();
        }

        public override double CalcPerimeter()
        {
            throw new NotImplementedException();
        }
    }

    internal class Rectangle2 : Figure2
    {
        private readonly double x;
        private readonly double y;

        public Rectangle2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override double CalcSurface()
        {
            throw new NotImplementedException();
        }

        public override double CalcPerimeter()
        {
            throw new NotImplementedException();
        }
    }
}
