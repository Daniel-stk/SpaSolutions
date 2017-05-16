using AutoMapper;
using DataModel.InformationModels;
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
    public class WorkspaceService
    {
        private WorkspaceGateway _gateway;
        private IMapper _mapper;

        public WorkspaceService()
        {
            _gateway = new WorkspaceGateway();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Workspace, WorkspaceDto>();
                cfg.CreateMap<WorkspaceDataSetResponse, Workspace>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<ObservableCollection<Workspace>> GetAllWorkspaces()
        {
            ObservableCollection<Workspace> workspaces = new ObservableCollection<Workspace>();
            var response = await _gateway.GetWorkspacesList();
            if (response != null)
            {
                foreach (var workspace in response.Data)
                {
                    workspaces.Add(_mapper.Map<WorkspaceDataSetResponse, Workspace>(workspace));
                }
            }
            else
            {
                throw new NoResponseException("El endpoint no respondio al request");
            }
            return workspaces;
        }
    }
}
