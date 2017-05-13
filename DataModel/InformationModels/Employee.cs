using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InformationModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set;}
        public string EmployeeAddress { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeCellPhone { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
