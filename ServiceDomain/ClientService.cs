using Gateway;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.InformationModels;
using ServiceDomain.Exceptions;
using System.Collections.ObjectModel;

namespace ServiceDomain
{
    public class ClientService
    {
        private ClientGateway _gateway;

        public ClientService()
        {
            _gateway = new ClientGateway();
        }
        
        public async Task<ObservableCollection<Client>> GetAllClients()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            var response = await _gateway.GetClientsList();
            if (response != null)
            {
                foreach (var client in response.Data)
                {
                    clients.Add(new Client()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName,
                        ClientCellPhone = client.ClientCellPhone,
                        ClientPhone = client.ClientPhone,
                        ClientAddress = client.ClientAddress,
                        ClientEmail = client.ClientEmail
                    });
                }
            }else
            {
                throw new NoResponseException("El endpoint no respondio al request");
            }
            return clients;
        }
    }
}
