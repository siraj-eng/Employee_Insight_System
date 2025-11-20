using Employee_Insight_System.Domain.Entities;
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


    }
}
