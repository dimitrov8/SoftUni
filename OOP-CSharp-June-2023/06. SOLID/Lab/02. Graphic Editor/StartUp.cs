namespace P02.Graphic_Editor
{
    using Models.Contracts;
    using Shapes;
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main()
        {
            List<IShape> shapes = new List<IShape>
            {
                new Circle(),
                new Rectangle(),
                new Square()
            };

            foreach (IShape shape in shapes)
            {
                Console.WriteLine(shape.Draw());
            }
        }
    }
}