namespace P03.Detail_Printer.Validators
{
    using System;
    using System.Linq;

    public static class NameValidator
    {
        public static void Validate(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty!");

            if (name.Any(char.IsDigit))
                throw new ArgumentException("Name cannot contain digits!");

            if (name.Any(char.IsSymbol))
                throw new ArgumentException("Name cannot contain symbols!");

            if (name.Length < 3)
                throw new ArgumentException("Name must be at least 3 characters!");
        }
    }
}