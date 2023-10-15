using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BakeryOpenning
{
    public class Bakery
    {
        public Bakery(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Employees = new HashSet<Employee>(capacity);
        }
        
        public HashSet<Employee> Employees { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public int Count => this.Employees.Count;

        public void Add(Employee employee)
        {
            if (this.Count < this.Capacity)
            {
                this.Employees.Add(employee);
            }
        }

        public bool Remove(string name)
        {
            Employee employeeToRemove = this.Employees.FirstOrDefault(e => e.Name == name);
            return this.Employees.Remove(employeeToRemove);
        }

        public Employee GetOldestEmployee()
        {
            return this.Employees.OrderByDescending(e => e.Age).FirstOrDefault();
        }

        public Employee GetEmployee(string name)
        {
            return this.Employees.FirstOrDefault(e => e.Name == name);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Employees working at Bakery {this.Name}:");
            foreach (var employee in this.Employees)
            {
                sb.AppendLine(employee.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}