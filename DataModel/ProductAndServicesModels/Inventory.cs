using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ProductAndServicesModels
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int InStock { get; set; }
        public int LowerLimit { get; set; }
        public Product Product { get; set; }
    }
}
