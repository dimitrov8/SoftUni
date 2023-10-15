namespace P02.Graphic_Editor.Editors
{
    using Models.Contracts;
    using System;

    public class GraphicEditor
    {
        public void DrawShape(IShape shape) => Console.WriteLine(shape.Draw());
    }
}