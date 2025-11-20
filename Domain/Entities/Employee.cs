using Employee_Insight_System.Enums;
using System;
using System.Collections.Generic;

namespace Employee_Insight_System.Domain.Entities
{
    public class Employee
    {
        // Immutable Identity
        public int EmployeeId { get; private set; }

        // Core Profile details
        public string EmployeeName { get; private set; }
        public int EmployeeAge { get; private set; }
        public string EmployeePhone { get; private set; }
        public EmployeeStatus Status { get; private set; }

        //--Additional properties--
        public string Department { get; private set; }
        public DateTime DateHired { get; private set; }
        public bool IsActive { get; private set; }

        // Nullable termination date for employees still active
        public DateTime? TerminationDate { get; private set; }

        // Salary, can be used for bonus calculation
        public decimal Salary { get; private set; }

        // Dynamic attributes for flexible properties
        public Dictionary<string, dynamic> DynamicAttributes { get; private set; } = new();

        // Constructor with validation
        public Employee(int employeeId, string employeeName,
                        int employeeAge, string employeePhone,
                        string department, DateTime dateHired,
                        decimal salary = 0)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be positive");
            if (string.IsNullOrWhiteSpace(employeeName))
                throw new ArgumentException("Employee name cannot be empty");
            if (employeeAge < 18)
                throw new ArgumentException("Employee must be at least 18 years old");
            if (string.IsNullOrWhiteSpace(employeePhone))
                throw new ArgumentException("Employee phone number is required");

            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeAge = employeeAge;
            EmployeePhone = employeePhone;
            Department = department;
            DateHired = dateHired;
            Salary = salary;
            IsActive = true;
        }

        // Domain behavior
        public void UpdatePhone(string newPhone)
        {
            if (string.IsNullOrWhiteSpace(newPhone))
                throw new ArgumentException("Phone number cannot be empty");
            EmployeePhone = newPhone;
        }

        public void UpdateDepartment(string newDepartment)
        {
            if (string.IsNullOrWhiteSpace(newDepartment))
                throw new ArgumentException("Department cannot be empty");
            Department = newDepartment;
        }

        public void UpdateSalary(decimal newSalary)
        {
            if (newSalary < 0)
                throw new ArgumentException("Salary cannot be negative");
            Salary = newSalary;
        }

        public void Deactivate(DateTime? terminationDate = null)
        {
            IsActive = false;
            TerminationDate = terminationDate ?? DateTime.Now;
        }

        public void Activate()
        {
            IsActive = true;
            TerminationDate = null;
        }

        /*
         *Domain Methods that matches the Employee Statuses
         */

        public void MarkAsFired () => Status = EmployeeStatus.Fired;
        public void MarkAsOnLeave () => Status = EmployeeStatus.OnLeave;
        public void MarkAsDemoted () => Status = EmployeeStatus.Demoted;
        public void Offboard() => Status = EmployeeStatus.Offboarded;


        // Calculated property: tenure in months
        public int TenureInMonths => ((TerminationDate ?? DateTime.Now) - DateHired).Days / 30;

        // Add dynamic attribute
        public void AddOrUpdateAttribute(string key, dynamic value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Attribute key cannot be empty");

            DynamicAttributes[key] = value;
        }

        // Get dynamic attribute safely
        public dynamic GetAttribute(string key)
        {
            return DynamicAttributes.ContainsKey(key) ? DynamicAttributes[key] : null;
        }
    }
}
