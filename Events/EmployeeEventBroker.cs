using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Insight_System.Events
{
    public class EmployeeEventBroker
    {
        //Typical events handlers 
        public event EventHandler<EmployeeEventArgs> EmployeeOnboarded;
        public event EventHandler<EmployeeEventArgs> EmployeeOffboarded;
        public event EventHandler<EmployeeEventArgs> EmployeeTerminated;
        public event EventHandler<EmployeeEventArgs> EmployeePromoted;
        public event EventHandler<EmployeeEventArgs> EmployeeDemoted;
        public event EventHandler<EmployeeEventArgs> EmployeeWentOnLeave;
        public event EventHandler<EmployeeEventArgs> EmployeeReturnedFromLeave;
        public event EventHandler<EmployeeEventArgs> EmployeeRoleChanged;
        public event EventHandler<EmployeeEventArgs> EmployeeDepartmentChanged;
        public event EventHandler<EmployeeEventArgs> EmployeeSalaryUpdated;

        /*
         * EmployeeEventBroker Methods - Method for subscriber to register or un register
         */
        //Method for subscribing to the event for multiple handlers
        private readonly ConcurrentDictionary<string, EventHandler<EmployeeEventArgs>> _eventHandlers =
            new ConcurrentDictionary<string, EventHandler<EmployeeEventArgs>>();

        //Method to subscribe to the event
        public void Subscribe(EventHandler<EmployeeEventArgs> handler, string eventType) 
        {
            if(_eventHandlers.ContainsKey(eventType)) 
                _eventHandlers[eventType] += handler;
            else
                _eventHandlers[eventType] = handler;
            Console.WriteLine($"Successfully subscriped to{eventType}");       
        }

        //Method to unsubscribe to the event
        public void Unsubscribe(EventHandler<EmployeeEventArgs> handler, string eventType)
        {
            if (_eventHandlers.ContainsKey(eventType))
            {
                _eventHandlers[eventType] -= handler;

                // Remove the entry if no subscribers left
                if (_eventHandlers[eventType] == null)
                    _eventHandlers.TryRemove(eventType, out _);
            }
        }



    }
}
