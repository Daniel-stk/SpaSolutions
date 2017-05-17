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

        public async Task<SingleResponse<int>> WorkspaceActions(WorkspaceDto workspace)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Workspaces/API/";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (workspace.WorkspaceId == 0)
                {
                    endPoint += "AddWorkspace";
                }
                else
                {
                    endPoint += "EditWorkspace";
                }

                var payload = EncodeContent(workspace);
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

        public async Task<SingleResponse<int>> DeleteWorkspace(int workspaceId)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Workspaces/API/DeleteWorkspace";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var payload = EncodeContent(workspaceId);
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

        private FormUrlEncodedContent EncodeContent(WorkspaceDto workspace)
        {
            var content = new List<KeyValuePair<string, string>>();
            if (workspace.WorkspaceId != 0)
            {
                content.Add(new KeyValuePair<string, string>("workspace_id", workspace.WorkspaceId.ToString()));
            }
            content.Add(new KeyValuePair<string, string>("workspace_name", workspace.WorkspaceName));
            return new FormUrlEncodedContent(content);
        }

        private FormUrlEncodedContent EncodeContent(int workspaceId)
        {
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("workspace_id", workspaceId.ToString()));
            return new FormUrlEncodedContent(content);
        }
    }
}
