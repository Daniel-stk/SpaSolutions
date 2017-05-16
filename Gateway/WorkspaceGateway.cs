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
    public class WorkspaceGateway
    {
        private HttpClient _httpClient;
        private string _baseAddress = "http://spasa.api/";

        public async Task<Response<WorkspaceDataSetResponse>> GetWorkspacesList()
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Workspaces/API/GetWorkspaces";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<WorkspaceDataSetResponse>>(responseContent);
                    return result;
                }
                return null;
            }
        }
    }
}
