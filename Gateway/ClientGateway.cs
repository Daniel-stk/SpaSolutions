using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataModel.InformationModels;
using System.Net.Http.Headers;
using Gateway.Response;
using Newtonsoft.Json;
using DebuggingTools;

namespace Gateway
{ 
    public class ClientGateway
    {
        private HttpClient _httpClient;
        private string _baseAddress = "http://spasa.api/";

        public async Task<Response<ClientDataSetResponse>> GetClientsList()
        {
            string endPoint = "Clients/API";
            using (_httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<ClientDataSetResponse>>(responseContent);
                    return result;
                }
                return null;
            }
        }
    }
}
