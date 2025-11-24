using Employee_Insight_System.Domain.Entities;
using Employee_Insight_System.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Directory
{
    public class EmployeeDirectory
    {
        public List<Employee> Employees { get; private set; } = new List<Employee>();

        
        public EmployeeDirectory()
        {
            Employees = new List<Employee>();
        }

        /*
         * Employee Retrieval and LookUp - Employee manipulation methods
         */
        //Get employee by ID method
        public Employee GetEmployeeById(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be greater than zero.");

            return Employees.SingleOrDefault(e => e.EmployeeId == employeeId);
        }

        //Get all employees
        public List<Employee> GetAllEmployees(string employeeName)
        {
            return Employees
                .Where(e => e.EmployeeName.Equals(employeeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        //Method to check if an employee exists
        public bool EmployeeExists(string employeeName) 
        {
            return Employees.Any(e => employeeName.Equals(employeeName, StringComparison.OrdinalIgnoreCase));
        }

        /*Search and Filtering Methods*/
        //1. Method to search employees by name or partial match
        public List<Employee> SearchEmployeeByName(string employeeName)
        {
            return Employees
                .Where(e => e.EmployeeName.Contains(employeeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
                
        }

        //2. Methods to filter employees by department
        public List<Employee> FilterEmployeesByDepartment(string employeeDepartment)
        {
            return Employees
                .Where(e => e.Department.Equals(employeeDepartment, StringComparison.OrdinalIgnoreCase)) 
                .ToList();
        }

        //3.Method to filter employees by job title or role
        public List<Employee> FilterEmployeesByJobTitle(string employeeJobTitle) 
        {
            return Employees
                .Where(e => e.Role.Equals(employeeJobTitle, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        //4. Method to filter employees by employment status
        public List<Employee> FilterEmployeesByEmploymentStatus(EmployeeStatus status) 
        {
            return Employees
                .Where(e => e.Status == status)
                .ToList();
        }
    }
}
