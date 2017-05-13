using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ProductAndServicesModels
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool Active { get; set; }
        public List<Product> Products { get; set; }
    }
}
