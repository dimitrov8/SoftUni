namespace P03.Detail_Printer.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DocumentsValidator
    {
        public static void Validate(IReadOnlyCollection<string> documents)
        {
            if (!documents.Any())
                throw new ArgumentException("Documents cannot be empty!");
        }
    }
}