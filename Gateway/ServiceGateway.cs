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
    public class ServiceGateway
    {
        private HttpClient _httpClient;
        private string _baseAddress = "http://spasa.api/";

        public async Task<Response<ServiceDataSetResponse>> GetServicesList()
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Services/API/GetServices";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<ServiceDataSetResponse>>(responseContent);
                    return result;
                }
                return null;
            }
        }

        public async Task<SingleResponse<ServiceDiscountActionsResponse>> ServiceActions(ServiceDto service)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Services/API/";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                if (service.ServiceId == 0)
                {
                    endPoint += "AddService";
                }
                else
                {
                    endPoint += "EditService";
                }

                var payload = EncodeContent(service);
                var response = await _httpClient.PostAsync(endPoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SingleResponse<ServiceDiscountActionsResponse>>(responseContent);
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

        public async Task<SingleResponse<ServiceDiscountActionsResponse>> DeleteService(int workspaceId)
        {
            using (_httpClient = new HttpClient())
            {
                var endPoint = "Services/API/DeleteService";
                _httpClient.BaseAddress = new Uri(_baseAddress);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var payload = EncodeContent(workspaceId);
                var response = await _httpClient.PostAsync(endPoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SingleResponse<ServiceDiscountActionsResponse>>(responseContent);
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

        private FormUrlEncodedContent EncodeContent(ServiceDto service)
        {
            var content = new List<KeyValuePair<string, string>>();
            if (service.ServiceId != 0)
            {
                content.Add(new KeyValuePair<string, string>("service_id", service.ServiceId.ToString()));
            }
            content.Add(new KeyValuePair<string, string>("service_name", service.ServiceName));
            content.Add(new KeyValuePair<string, string>("service_price", service.ServicePrice.ToString()));
            if (service.Active) { 
                content.Add(new KeyValuePair<string, string>("active_service", service.Active.ToString()));
            }
            if (service.ServiceDiscount.ServiceDiscount > 0)
            {
                if(service.ServiceDiscount.ServiceDiscountId != 0)
                {
                    content.Add(new KeyValuePair<string, string>("service_discount_id", service.ServiceDiscount.ServiceDiscountId.ToString()));
                }
                content.Add(new KeyValuePair<string, string>("discount", service.ServiceDiscount.ServiceDiscount.ToString()));
                content.Add(new KeyValuePair<string, string>("start", service.ServiceDiscount.StartDate.ToString("yyyy-MM-dd")));
                content.Add(new KeyValuePair<string, string>("end", service.ServiceDiscount.EndDate.ToString("yyyy-MM-dd")));
                if (service.ServiceDiscount.Active)
                {
                    content.Add(new KeyValuePair<string, string>("active_discount", service.ServiceDiscount.Active.ToString()));
                }
            }
            return new FormUrlEncodedContent(content);
        }

        private FormUrlEncodedContent EncodeContent(int serviceId)
        {
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("service_id", serviceId.ToString()));
            return new FormUrlEncodedContent(content);
        }

    }
}
