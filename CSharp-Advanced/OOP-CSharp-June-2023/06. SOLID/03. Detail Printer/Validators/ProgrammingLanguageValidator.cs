namespace P03.Detail_Printer.Validators
{
    using System;
    using System.Collections.Generic;

    public static class ProgrammingLanguageValidator
    {
        private static readonly HashSet<string> UsedProgrammingLanguages = new HashSet<string>
        {
            "C#",
            "C++",
            "Java",
            "JavaScript",
            "Python",
            "PHP",
            "Ruby"
        };

        public static void Validate(string programmingLanguage)
        {
            if (!UsedProgrammingLanguages.Contains(programmingLanguage))
                throw new ArgumentException("Programming language is not supported!");
        }
    }
}