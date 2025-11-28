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
        public void GetEmployeesTenure(string employeeName, DateTime dateHired)
        {
            var now = DateTime.Now;

            var employeesTenure = Employees
                .Where(e => e.EmployeeName == employeeName && e.DateHired == dateHired)
                .Select(e => now - e.DateHired);

            foreach (var tenure in employeesTenure)
            {
                Console.WriteLine($"{employeeName} | {tenure.TotalDays / 365:0.00} years");
            }
        }

        // 2. Method to Longest-serving employees
        public List<Employee> GetTheLongestServingEmployees(int count = 5)
        {
            return Employees
                .OrderBy(e => e.DateHired)
                .Take(count)
                .ToList();
                
        }

        //3. Method to identify recent Hires
        public List<Employee> GetTheRecentHires(int count = 5) 
        {
            return Employees
                 .OrderByDescending(e => e.DateHired)
                 .Take(count)
                 .ToList();     
        }

        // 3 (a) - Method to identify recent hires within custom range date
        public List<Employee> GetRecentHiresWithinRange(DateTime start, DateTime end)
        {
            return Employees
                .Where(e => e.DateHired >= start && e.DateHired <= end)
                .OrderByDescending(e => e.DateHired)
                .ToList();
        }

        // 4. Compute attrition trends - if termination date does not exist
        
        public Dictionary<int, int> ComputeAttritionTrends()
        {
            return Employees
                .Where(e => e.TerminationDate != null)
                .GroupBy(e => e.TerminationDate.Value.Year)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // 5 - Surface Anomalies -> inactive with no termination date - missing department
        public List<Employee> GetAnomalies()
        {
            return Employees
                .Where(e =>
                    (!e.IsActive && e.TerminationDate == null) ||   // inactive with no termination
                    e.Department == null ||                         // missing department
                    string.IsNullOrWhiteSpace(e.Role)               // missing role
                )
                .ToList();
        }

        // Register - Add employee 
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (Employees.Any(e => e.EmployeeId == employee.EmployeeId))
                throw new InvalidOperationException("Employee already exists.");

            Employees.Add(employee);
        }

        //Unregistrer - Remove employee
        public bool RemoveEmployee(int employeeId)
        {
            var employee = Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
                return false;

            Employees.Remove(employee);
            return true;
        }

        //Refresh - cache (build directory)
        public void RefreshDirectory(List<Employee> freshEmployees)
        {
            if (freshEmployees == null)
                throw new ArgumentNullException(nameof(freshEmployees));

            Employees = freshEmployees;
        }
    }

}
