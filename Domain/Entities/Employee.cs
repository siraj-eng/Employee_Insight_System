using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Domain.Entities
{
    public class Employee
    {
        //Immutable Identity
        public int EmployeeId { get; private set; }

        //Core Profile details
        public string EmployeeName {  get; private set; }
        public int EmployeeAge { get; private set; }
        public string EmployeePhone { get; private set; }

        //--Additional properties--
        public string Department {  get; private set; }
        public DateTime DateHired { get; private set; }
        public bool IsActive { get; private set; }
        
        //Constructor with validation
        public Employee(int employeeId, string employeeName, 
                        int employeeAge, string employeePhone,
                        string department, DateTime dateHired) 
        { 
            if(employeeId <= 0)
              throw new ArgumentException("Employee ID must be positive");
            if (string.IsNullOrWhiteSpace(employeeName))
                throw new ArgumentException("Employee name cannot be empty");
            if (employeeAge < 18)
                throw new ArgumentException("Employee must be atleast 18 years old");
            if (string.IsNullOrWhiteSpace(employeePhone))
                throw new ArgumentException("Employee phone number is required");

            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeAge = employeeAge;
            EmployeePhone = employeePhone;
            Department  = department;
            DateHired = dateHired;
            IsActive = true;
        }

        //Domain behavior - entities do things - Update phone number
        public void UpdatePhone(string newPhone) 
        {
            if (string.IsNullOrWhiteSpace(newPhone))
                throw new ArgumentException("Phone number cannot be empty");

            EmployeePhone = newPhone;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }



    }
}
