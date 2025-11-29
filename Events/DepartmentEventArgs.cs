using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Events
{
    public class DepartmentEventArgs : EventArgs
    {
        // Identity of the department involved in the event
        public int DepartmentId { get; }
        public string DepartmentName { get; }

        // Contextual metadata about the event trigger
        public string EventType { get; }      // e.g. "DepartmentCreated", "DepartmentUpdated", "DepartmentClosed"
        public string? Notes { get; }

        // When the event happened (UTC for distributed systems)
        public DateTime EventTimestamp { get; }

        public DepartmentEventArgs(
            int departmentId,
            string departmentName,
            string eventType,
            string? notes = null)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
            EventType = eventType;
            Notes = notes;

            EventTimestamp = DateTime.UtcNow;
        }

    }
}
