using System;
using System.Text;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get => this.length;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException($"{nameof(this.Length)} cannot be zero or negative.");
                this.length = value;
            }
        }

        public double Width
        {
            get => this.width;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException($"{nameof(this.Width)} cannot be zero or negative.");
                this.width = value;
            }
        }

        public double Height
        {
            get => this.height;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException($"{nameof(this.Height)} cannot be zero or negative.");
                this.height = value;
            }
        }

        public double SurfaceArea() =>
            2 * (this.length * this.width + this.width * this.height + this.height * this.length);

        public double LateralSurfaceArea() => 2 * (this.length * this.height + this.width * this.height);

        public double Volume() => this.length * this.width * this.height;

        public override string ToString()
            => new StringBuilder()
                .AppendLine($"Surface Area - {this.SurfaceArea():f2}")
                .AppendLine($"Lateral Surface Area - {this.LateralSurfaceArea():f2}")
                .AppendLine($"Volume - {this.Volume():f2}").ToString().TrimEnd();
    }
}