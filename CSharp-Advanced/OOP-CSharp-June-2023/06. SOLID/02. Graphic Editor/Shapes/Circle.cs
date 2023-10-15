namespace P02.Graphic_Editor.Shapes
{
    using Models.Contracts;

    public class Circle : IShape
    {
        public string Draw() => $"I'm {this.GetType().Name}";
    }
}