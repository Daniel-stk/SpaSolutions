using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Response
{
    public class ServiceDiscountActionsResponse
    {
        [JsonProperty("service_id")]
        public int ServiceId { get; set; }
        [JsonProperty("service_discount_id")]
        public int ServiceDiscountId { get; set; }
    }
}
