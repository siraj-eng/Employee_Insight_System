using Employee_Insight_System.Domain.Entities;
using Employee_Insight_System.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Authentication;
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

        /*
         * Grouping and Aggregation - To compute values containing salary and all that
         */
        // 1. Group employees by department
        public IEnumerable<IGrouping<string, Employee>> GroupEmployeesByDepartment()
        {
            return Employees
                .GroupBy(e => e.Department);
        }

        // 2. Group employees by role
        public IEnumerable<IGrouping<string, Employee>> GroupEmployeesByRole()
        {
            return Employees
                .GroupBy(e => e.Role);
        }

        //3. Group employees by status
        public IGrouping<EmployeeStatus, Employee>? GroupEmployeesByStatus(EmployeeStatus status)
        {
            return Employees
                .GroupBy(e => e.Status)
                .FirstOrDefault(g => g.Key == status);
        }

        //4. Method to compute total headcount
        public int TotalHeadcount()
        {
            return Employees.Count();
        }

        //5. Method to compute Active employee count
        public int ActiveEmployees()
        {
            return Employees.Count(e => e.Status ==  EmployeeStatus.Active);
        }

        //6. Method to compute employees hired within a date range
        public IEnumerable<Employee> GetEmployeesHiredWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return Employees
                .Where(e => e.DateHired >= startDate && e.DateHired <= endDate);
        }

        /* Department-Level Insights -- These are some of the impactful methods to manipulate employees departmental structure */
        public void EmployeesDepartment(string departmentName) 
        {
            var employeesInDept = Employees
                .Where(e => e.Department.Equals(departmentName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine($"---Department: {departmentName} | Headcount: {employeesInDept.Count}");

            foreach(var e in employeesInDept)
            {
                Console.WriteLine($"{e.EmployeeName} | {e.Role} | {e.Status}");
            }
        }

        /*
         *Analytics & High-Level Metrics --> These are higher level metrics that will be used to endure the process In Hr auditing possibly
         *
         */

        //1. Method to calculate overall tenure distribution
        public Dictionary<string, double> GetEmployeesTenure()
        {
            var now = DateTime.Now;

            return Employees.ToDictionary(
                e => e.EmployeeName,
                e => Math.Round((now - e.DateHired).TotalDays / 365, 2)
            );
        }

    }

}
