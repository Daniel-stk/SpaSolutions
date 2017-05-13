using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ProductAndServicesModels
{
    public class Service
    {
        public int ServiceId { get; set; }
        public ServiceDiscounts ServiceDisocunt { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public bool Active { get; set; }
        public ServiceDiscounts ServiceDiscount { get; set; }
    }
}
