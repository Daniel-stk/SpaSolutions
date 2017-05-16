using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.DataTransferObjects
{
    public class EmployeeDto
    {
        [JsonProperty("employee_id")]
        public int EmployeeId { get; set; }
        [JsonProperty("employee_name")]
        public string EmployeeName { get; set; }
        [JsonProperty("employee_homephone")]
        public string EmployeeHomePhone { get; set; }
        [JsonProperty("employee_cellphone")]
        public string EmployeeCellPhone { get; set; }
        [JsonProperty("employee_address")]
        public string EmployeeAddress { get; set; }
    }
}
