using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.ProductAndServicesModels;

namespace DataModel.SalesModels
{
    public class Items
    {
        public List<Product> Products { get; set; }
        public List<Service> Services { get; set; }
    }
}
