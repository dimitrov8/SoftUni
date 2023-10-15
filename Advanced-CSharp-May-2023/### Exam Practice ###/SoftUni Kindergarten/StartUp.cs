using System;

namespace SoftUniKindergarten
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Kindergarten kindergarten = new Kindergarten("Space S", 8);
            Child childOne = new Child("Greta", "Garbo", 3, "Karl Gustafsson", "0468 888 888");
            Child childTwo = new Child("Elona", "Muskova", 4, "Maye Musk", " 1 888 518 3752");
            Child childThree = new Child("George", "Bush", 5, " George Bush Sr.", "xx xxx xxxxxx");
            Child childFour = new Child("Ruzha", "Ignatova", 6, "Veska Ignatova", "+49 30901820");
            Child childFive = new Child("Greta", "Thinberg", 3, "Allen White", "541-754-3010");
            Child childSix = new Child("T", "Rex", 2, "Steven Spielberg", "63 001 09 93");
            Child childSeven = new Child("S", "Rex", 2, " Steven Spielberg ", "63 001 09 93");
            Child childEight = new Child("Greta", "Thunberg", 3, "Pablo Gaviria", "0888 888888");
            Child childNine = new Child("Tim", "Duncan", 6, "William Duncan", "(555) 555-1234");
           
            Console.WriteLine(kindergarten.AddChild(childOne));
            Console.WriteLine(kindergarten.AddChild(childTwo));
            Console.WriteLine(kindergarten.AddChild(childThree));
            Console.WriteLine(kindergarten.AddChild(childFour));
            Console.WriteLine(kindergarten.AddChild(childFive));
            Console.WriteLine(kindergarten.AddChild(childSix));
            Console.WriteLine(kindergarten.AddChild(childSeven));
            Console.WriteLine(kindergarten.AddChild(childEight));
            Console.WriteLine(kindergarten.AddChild(childNine));
            Console.WriteLine(kindergarten.RemoveChild("Ruzha Ignatova"));
            Console.WriteLine(kindergarten.RemoveChild("George Bush"));
            Console.WriteLine(kindergarten.RemoveChild("Elona Muskova"));
            Console.WriteLine(kindergarten.RemoveChild("Ruzha Ignatova"));
            Console.WriteLine(kindergarten.RemoveChild("Tim Duncan"));
            Console.WriteLine(kindergarten.ChildrenCount);
            Console.WriteLine(kindergarten.GetChild("S Rex"));
            Console.WriteLine(kindergarten.RegistryReport());
        }
    }
}
