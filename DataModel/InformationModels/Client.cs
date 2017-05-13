using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.SalesModels;

namespace DataModel.InformationModels
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public string ClientCellPhone { get; set; }
        public string ClientEmail { get; set; }
        public List<Sale> Sales { get; set; } 
        public List<Appointment> Appointments { get; set; }
    }
}
