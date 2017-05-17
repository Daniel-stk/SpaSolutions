using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.DataTransferObjects
{
    public class WorkspaceDto
    {
        [JsonProperty("workspace_id")]
        public int WorkspaceId { get; set; }
        [JsonProperty("workspace_name")]
        public string WorkspaceName { get; set; }
    }
}
