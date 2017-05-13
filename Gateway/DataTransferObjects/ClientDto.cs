using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.DataTransferObjects
{
    internal class ClientDto
    {
        [JsonProperty("client_name")]
        public string ClientName { get; set; }
        [JsonProperty("client_address")]
        public string ClientAddress { get; set; }
        [JsonProperty("client_homephone")]
        public string ClientPhone { get; set; }
        [JsonProperty("client_cellphone")]
        public string ClientCellPhone { get; set; }
        [JsonProperty("client_email")]
        public string ClientEmail { get; set; }

    }
}
