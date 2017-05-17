using AutoMapper;
using DataModel.ProductAndServicesModels;
using Gateway;
using Gateway.DataTransferObjects;
using Gateway.Response;
using ServiceDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDomain
{
    public class ServicesService
    {
        private ServiceGateway _gateway;
        private IMapper _mapper;

        public ServicesService()
        {
            _gateway = new ServiceGateway();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Service, ServiceDto>();
                cfg.CreateMap<ServiceDiscounts,ServiceDiscountDto>();
                cfg.CreateMap<ServiceDataSetResponse, Service>();
                cfg.CreateMap<ServiceDiscountsDataResponse,ServiceDiscounts>();

            });
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<ObservableCollection<Service>> GetAllServices()
        {
            ObservableCollection<Service> services = new ObservableCollection<Service>();
            var response = await _gateway.GetServicesList();
            if (response != null)
            {
                foreach (var service in response.Data)
                {
                    services.Add(_mapper.Map<ServiceDataSetResponse, Service>(service));
                }
            }
            else
            {
                throw new NoResponseException("El endpoint no respondio al request");
            }
            return services;
        }

        public async Task<SingleResponse<ServiceDiscountActionsResponse>> InsertOrEditService(Service service)
        {
            var serviceDto = _mapper.Map<Service, ServiceDto>(service);
            return await _gateway.ServiceActions(serviceDto);
        }

        public async Task<SingleResponse<ServiceDiscountActionsResponse>> DeleteService(Service service)
        {
            return await _gateway.DeleteService(service.ServiceId);
        }


    }
}
