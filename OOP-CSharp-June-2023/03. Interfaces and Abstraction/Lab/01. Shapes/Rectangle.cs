﻿namespace Shapes
{
    using System;

    public class Rectangle : IDrawable
    {
        private readonly int width;
        private readonly int height;

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        private void DrawLine(int width, char end, char mid)
        {
            Console.Write(end);
            for (int i = 1; i < width - 1; ++i)
            {
                Console.Write(mid);
            }

            Console.WriteLine(end);
        }


        public void Draw()
        {
            this.DrawLine(this.width, '*', '*');
            for (int i = 1; i < this.height - 1; ++i)
            {
                this.DrawLine(this.width, '*', ' ');
            }

            this.DrawLine(this.width, '*', '*');
        }
    }
}