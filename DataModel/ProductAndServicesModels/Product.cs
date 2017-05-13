using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ProductAndServicesModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public Brand Brand { get; set; }
        public Inventory Inventory { get; set; }
        public ProductDiscounts ProductDiscount { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Active { get; set; }

    }
}
