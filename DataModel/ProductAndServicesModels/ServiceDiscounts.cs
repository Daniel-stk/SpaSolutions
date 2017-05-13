using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ProductAndServicesModels
{
    public class ServiceDiscounts
    {
        public int ServiceDiscountId { get; set; }
        public decimal ServiceDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public Service Service { get; set; }
    }
}
