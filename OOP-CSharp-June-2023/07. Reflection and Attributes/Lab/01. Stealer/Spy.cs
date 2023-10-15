﻿namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[] requestedFields)
        {
            Type classType = Type.GetType(investigatedClass);
            FieldInfo[] fields = classType.GetFields((BindingFlags)60);

            var sb = new StringBuilder();

            object classInstance = Activator.CreateInstance(classType, new object[] { });

            sb.AppendLine($"Class under investigation: {investigatedClass}");

            foreach (FieldInfo field in fields.Where(f => requestedFields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} - {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }
    }
}