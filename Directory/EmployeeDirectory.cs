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
        public void getEmployeeID(string employeeName, int employeeId )
        {
            Console.Write("Enter the employee Id");
            string employerId = string.Parse( employeeId.ToInt() );

            //verification steps - step 1
            if(employeeId == 0 || employeeId == null)
            {
                throw new ArgumentException("employee cannot be null or zero");
                
            }

            //get the method employee name
            var employeeName = Employees.Where(employeeId => employerId);

            Employee employee = Employees.SingleOrDefault( employeeName );

            Console.WriteLine($"{employerId} {employeeName}");

        }
    }
}
