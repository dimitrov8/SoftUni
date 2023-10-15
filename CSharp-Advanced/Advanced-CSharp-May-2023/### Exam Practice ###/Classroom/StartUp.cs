namespace ClassroomProject
{
    using System;

    public class StartUp
    {
        static void Main(string[] args)
        {
            Classroom classroom = new Classroom(10);
            Student student = new Student("Peter", "Parker", "Geometry");
            Student studentTwo = new Student("Sarah", "Smith", "Algebra");
            Student studentThree = new Student("Sam", "Winchester", "Algebra");
            Student studentFour = new Student("Dean", "Winchester", "Music");
            Console.WriteLine(student);
            string register = classroom.RegisterStudent(student);
            Console.WriteLine(register);
            string registerTwo = classroom.RegisterStudent(studentTwo);
            string registerThree = classroom.RegisterStudent(studentThree);
            string registerFour = classroom.RegisterStudent(studentFour);
            string dismissed = classroom.DismissStudent("Peter", "Parker");
            Console.WriteLine(dismissed);
            string dismissedTwo = classroom.DismissStudent("Ellie", "Goulding");
            Console.WriteLine(dismissedTwo);
            string subjectInfo = classroom.GetSubjectInfo("Algebra");
            Console.WriteLine(subjectInfo);
            string anotherInfo = classroom.GetSubjectInfo("Art");
            Console.WriteLine(anotherInfo);
            Console.WriteLine(classroom.GetStudentsCount());
            Console.WriteLine(classroom.GetStudent("Dean", "Winchester"));
        }
    }
}