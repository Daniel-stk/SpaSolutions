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
using Gateway.DataTransferObjects;

namespace Gateway
{ 
    public class ClientGateway
    {
        private HttpClient _httpClient;
        private string _baseAddress = "http://spasa.api/";
        private string _endPoint = "Clients/API";

        public async Task<Response<ClientDataSetResponse>> GetClientsList()
        {
            using (_httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("Action", "GetClientsList");
                var response = await _httpClient.GetAsync(_endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<ClientDataSetResponse>>(responseContent);
                    return result;
                }
                return null;
            }
        }

       public async Task<bool> ClientActions(ClientDto client)
       {
            using (_httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (client.ClientId != 0)
                {
                    _httpClient.DefaultRequestHeaders.Add("Action", "EditClient");
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Add("Action", "AddClient");
                }
                var payload = new StringContent(JsonConvert.SerializeObject(client),Encoding.UTF8,"application)json");
                
                var response = await _httpClient.PostAsync(_endPoint,payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SimpleResponse>(responseContent);
                    if(result.Success == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
       }

       public async Task<bool> DeleteClient(int clientId)
       {
            using (_httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new StringContent(JsonConvert.SerializeObject(new { client_id = clientId}),Encoding.UTF8,"application/json");
                var response = await _httpClient.PostAsync(_endPoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SimpleResponse>(responseContent);
                    if (result.Success == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        } 
    }
}
