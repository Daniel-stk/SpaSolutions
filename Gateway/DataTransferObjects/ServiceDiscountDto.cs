using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.DataTransferObjects
{
    public class ServiceDiscountDto
    {
        [JsonProperty("service_discount_id")]
        public int ServiceDiscountId { get; set; }
        [JsonProperty("discount")]
        public decimal ServiceDiscount { get; set; }
        [JsonProperty("start")]
        public DateTime StartDate { get; set; }
        [JsonProperty("ends")]
        public DateTime EndDate { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
