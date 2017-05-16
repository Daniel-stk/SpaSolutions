using DebuggingTools;
using Gateway.DataTransferObjects;
using Gateway.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public class EmployeeGateway
    {
        private HttpClient _httpClient;
        private string _baseAddress = "http://spasa.api/";


        public async Task<Response<EmployeeDataSetResponse>> GetEmployeesList()
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Employees/API/GetEmployees";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<EmployeeDataSetResponse>>(responseContent);
                    return result;
                }
                return null;
            }
        }

        public async Task<SingleResponse<int>> EmployeesActions(EmployeeDto employee)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Employees/API/";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (employee.EmployeeId == 0)
                {
                    endPoint += "AddEmployee";
                }
                else
                {
                    endPoint += "EditEmployee";
                }

                var payload = EncodeContent(employee);
                var response = await _httpClient.PostAsync(endPoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SingleResponse<int>>(responseContent);
                    if (result.Success == 1)
                    {
                        return result;
                    }
                    ConsoleManager.Show();
                    Console.WriteLine(result.Message);
                    Console.ReadKey();
                }
                return null;
            }
        }

        public async Task<SingleResponse<int>> DeleteEmployee(int clientId)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Employees/API/DeleteEmployee";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var payload = EncodeContent(clientId);
                var response = await _httpClient.PostAsync(endPoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SingleResponse<int>>(responseContent);
                    if (result.Success == 1)
                    {
                        return result;
                    }
                    ConsoleManager.Show();
                    Console.WriteLine(result.Message);
                }
                return null;
            }
        }

        private FormUrlEncodedContent EncodeContent(EmployeeDto employee)
        {
            var content = new List<KeyValuePair<string, string>>();
            if (employee.EmployeeId != 0)
            {
                content.Add(new KeyValuePair<string, string>("employee_id", employee.EmployeeId.ToString()));
            }
            content.Add(new KeyValuePair<string, string>("employee_name", employee.EmployeeName));
            content.Add(new KeyValuePair<string, string>("employee_homephone", employee.EmployeeHomePhone));
            content.Add(new KeyValuePair<string, string>("employee_cellphone", employee.EmployeeCellPhone));
            content.Add(new KeyValuePair<string, string>("employee_address", employee.EmployeeAddress));
            return new FormUrlEncodedContent(content);
        }

        private FormUrlEncodedContent EncodeContent(int employeeId)
        {
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("employee_id", employeeId.ToString()));
            return new FormUrlEncodedContent(content);
        }
    }
}
