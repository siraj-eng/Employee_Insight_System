using Employee_Insight_System.Domain.Entities;
using Employee_Insight_System.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Services
{
    public class EmployeeService
    {
        //Stores employees Internally
        private readonly List<Employee> Employees = new List<Employee>();

        //Event broker to notify subscribers about changes
        private readonly EmployeeEventBroker _eventBroker;

        // Constructor
        public EmployeeService(EmployeeEventBroker eventBroker)
        {
            _eventBroker = eventBroker ?? throw new ArgumentNullException(nameof(eventBroker));
        }

        //Add employee - Method add employees to directory and triggers on onboarding event
        public void AddEmployee(Employee employee) 
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");
            }

            //Add employee to the internal list
            Employees.Add(employee);

            //Trigger onboard event via the eventBroker
            _eventBroker.OnEmployeeAdded(employee);

            Console.WriteLine($"Successfully added a new employee: {employee.EmployeeName}");


        }
    }
}
