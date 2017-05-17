using Gateway.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Response
{
    public class ServiceDataSetResponse
    {
        [JsonProperty("service_id")]
        public int ServiceId { get; set; }
        [JsonProperty("service_discount")]
        public ServiceDiscountsDataResponse ServiceDiscount { get; set; }
        [JsonProperty("service_name")]
        public string ServiceName { get; set; }
        [JsonProperty("service_price")]
        public decimal ServicePrice { get; set; }
        [JsonConverter(typeof(BooleanConverter))]
        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
