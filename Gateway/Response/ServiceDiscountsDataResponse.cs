using Gateway.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Response
{
    public class ServiceDiscountsDataResponse
    {
        [JsonProperty("service_discount_id")]
        public int ServiceDiscountId { get; set; }
        [JsonProperty("discount")]
        public decimal ServiceDiscount { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("start")]
        public DateTime StartDate { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("ends")]
        public DateTime EndDate { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
