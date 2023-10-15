namespace Shapes
{
    public class Rectangle : Shape
    {
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }

        protected double Height
        {
            get => this.height;
            set => this.height = value;
        }

        protected double Width
        {
            get => this.width;
            set => this.width = value;
        }

        public override double CalculatePerimeter() => this.Height * 2 + this.Width * 2;

        public override double CalculateArea() => this.Height * this.Width;

        public override string Draw() => $"{base.Draw()} {nameof(Rectangle)}";
    }
}