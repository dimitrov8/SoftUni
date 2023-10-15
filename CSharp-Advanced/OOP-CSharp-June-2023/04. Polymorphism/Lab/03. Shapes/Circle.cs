namespace Shapes
{
    using System;

    public class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        protected double Radius
        {
            get => this.radius;
            set => this.radius = value;
        }

        public override double CalculatePerimeter() => 2 * Math.PI * this.Radius;
        public override double CalculateArea() => Math.PI * Math.Pow(this.Radius, 2);
        public override string Draw() => $"{base.Draw()} {nameof(Circle)}";
    }
}