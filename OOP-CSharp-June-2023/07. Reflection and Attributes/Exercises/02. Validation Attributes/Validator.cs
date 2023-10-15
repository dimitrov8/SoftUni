namespace ValidationAttributes
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType
                .GetProperties()
                .Where(p => p.CustomAttributes.Any(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.AttributeType)))
                .ToArray();

            foreach (PropertyInfo property in properties)
            {
                Attribute[] customAttributes = property
                    .GetCustomAttributes()
                    .Where(ca => ca is MyValidationAttribute)
                    .ToArray();

                object propValue = property.GetValue(obj);

                foreach (Attribute attribute in customAttributes)
                {
                    MethodInfo isValidMethod = attribute
                        .GetType()
                        .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .FirstOrDefault(m => m.Name == "IsValid");

                    if (isValidMethod == null)
                        throw new InvalidOperationException("Your custom attribute doesn't have valid IsValid method!");

                    bool result = (bool)isValidMethod.Invoke(attribute, new[] { propValue });

                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}