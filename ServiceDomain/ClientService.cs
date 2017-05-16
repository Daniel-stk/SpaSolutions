using Gateway;
using System.Threading.Tasks;
using DataModel.InformationModels;
using ServiceDomain.Exceptions;
using System.Collections.ObjectModel;
using Gateway.DataTransferObjects;
using AutoMapper;
using Gateway.Response;

namespace ServiceDomain
{
    public class ClientService
    {
        private ClientGateway _gateway;
        private IMapper _mapper;

        public ClientService()
        {
            _gateway = new ClientGateway();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Client, ClientDto>();
                cfg.CreateMap<ClientDataSetResponse, Client>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        
        public async Task<ObservableCollection<Client>> GetAllClients()
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();
            var response = await _gateway.GetClientsList();
            if (response != null)
            {
                foreach (var client in response.Data)
                {
                    clients.Add(_mapper.Map<ClientDataSetResponse,Client>(client));
                }
            }else
            {
                throw new NoResponseException("El endpoint no respondio al request");
            }
            return clients;
        }

        public async Task<SingleResponse<int>> InsertOrEditClient(Client client)
        {
            var clientDto = _mapper.Map<Client, ClientDto>(client);
            return await _gateway.ClientActions(clientDto);
        }

        public async Task<SingleResponse<int>> DeleteClient(Client client)
        {
            return await _gateway.DeleteClient(client.ClientId);
        }
    }
}
