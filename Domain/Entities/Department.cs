using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_Insight_System.Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; private set; }
        public string DepartmentName { get; private set; }
        public string DepartmentPhoneNumber { get; private set; }

        // Navigation: employees belonging to this department
        public List<Employee> Employees { get; private set; } = new List<Employee>();

        public Department(int departmentId, string departmentName, string departmentPhoneNumber)
        {
            if (departmentId <= 0)
                throw new ArgumentException("Department ID must be positive.");

            if (string.IsNullOrWhiteSpace(departmentName))
                throw new ArgumentException("Department name is required.");

            DepartmentId = departmentId;
            DepartmentName = departmentName;
            DepartmentPhoneNumber = departmentPhoneNumber;
        }

        // Domain behavior — add employee to department
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (!Employees.Contains(employee))
                Employees.Add(employee);
        }

        // Domain behavior — remove employee
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            Employees.Remove(employee);
        }

        // Active employees count (based on IsActive)
        public int ActiveEmployees => Employees.Count(e => e.IsActive);

        // Department headcount
        public int TotalEmployees => Employees.Count;
    }
}
