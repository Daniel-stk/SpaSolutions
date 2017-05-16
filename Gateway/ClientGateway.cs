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
        
        public async Task<Response<ClientDataSetResponse>> GetClientsList()
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Clients/API/GetClients";
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

       public async Task<SingleResponse<int>> ClientActions(ClientDto client)
       {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Clients/API/";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (client.ClientId == 0)
                {
                    endPoint += "AddClient";  
                }
                else
                {
                    endPoint += "EditClient";
                }
              
                var payload = EncodeContent(client); 
                var response = await _httpClient.PostAsync(endPoint,payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SingleResponse<int>>(responseContent);
                    if(result.Success == 1)
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

       public async Task<SingleResponse<int>> DeleteClient(int clientId)
       {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Clients/API/DeleteClient";
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
        
        private FormUrlEncodedContent EncodeContent(ClientDto client) 
        {
            var content = new List<KeyValuePair<string, string>>();
            if(client.ClientId != 0)
            {
                content.Add(new KeyValuePair<string, string>("client_id", client.ClientId.ToString()));
            }
            content.Add(new KeyValuePair<string, string>("client_name", client.ClientName));
            content.Add(new KeyValuePair<string, string>("client_address", client.ClientAddress));
            content.Add(new KeyValuePair<string, string>("client_homephone", client.ClientPhone));
            content.Add(new KeyValuePair<string, string>("client_cellphone", client.ClientCellPhone));
            content.Add(new KeyValuePair<string, string>("client_email", client.ClientEmail));
            return new FormUrlEncodedContent(content);
        }

        private FormUrlEncodedContent EncodeContent(int clientId)
        {
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("client_id", clientId.ToString()));
            return new FormUrlEncodedContent(content);
        }
    }
}
