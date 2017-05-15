using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ProductAndServicesModels;

namespace DataModel.InformationModels
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentStart { get; set; }
        public TimeSpan AppointmentEnd { get; set; }
        public bool Active { get; set;}
        public int ServiceId { get; set; }
        public int EmployeeId { get; set; }
        public int WorkspaceId { get; set; }
        public int ClientId { get; set; }
        public Service Service { get; set; }
        public Employee Employee { get; set; }
        public Workspace Workspace { get; set; }
        public Client Client { get; set; }
    }
}
