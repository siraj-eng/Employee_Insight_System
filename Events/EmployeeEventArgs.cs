using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Events
{
    public class EmployeeEventArgs : EventArgs
    {
        // The employee entity involved in the event
        public int EmployeeID { get; }
        public string EmployeeName { get; }
        public string Department {  get; }
        public DateTime EventTimestamp { get; }

        //Optional metadata for analytics
        public string EventType { get; } // e.g "Onboarded" , "Terminated"
        public string? Notes { get; } // FreeForm context

        //Constructor
        public EmployeeEventArgs(
            int employeeID,
            string employeeName,
            string department,
            string eventType,
            string? notes = null)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Department = department;
            EventType = eventType;
            Notes = notes;

            EventTimestamp = DateTime.Now;
        }





    }
}
