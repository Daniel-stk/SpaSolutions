using Newtonsoft.Json;

namespace Gateway.DataTransferObjects
{
    public class ClientDto
    {
        [JsonProperty("client_id")]
        public int ClientId { get; set; }
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
