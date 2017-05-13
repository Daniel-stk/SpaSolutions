using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Response
{
    public class Response<T>
    {
        [JsonProperty("error")]
        public int Error { get; set; }
        [JsonProperty("success")]
        public int Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
