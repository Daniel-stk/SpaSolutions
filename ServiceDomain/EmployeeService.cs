using AutoMapper;
using DataModel.InformationModels;
using Gateway;
using Gateway.DataTransferObjects;
using Gateway.Response;
using ServiceDomain.Exceptions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ServiceDomain
{
    public class EmployeeService
    {
        private EmployeeGateway _gateway;
        private IMapper _mapper;

        public EmployeeService()
        {
            _gateway = new EmployeeGateway();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDataSetResponse, Employee>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<ObservableCollection<Employee>> GetAllEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            var response = await _gateway.GetEmployeesList();
            if (response != null)
            {
                foreach (var employee in response.Data)
                {
                    employees.Add(_mapper.Map<EmployeeDataSetResponse, Employee>(employee));
                }
            }
            else
            {
                throw new NoResponseException("El endpoint no respondio al request");
            }
            return employees;
        }

        public async Task<SingleResponse<int>> InsertOrEditEmployee(Employee employee)
        {
            var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
            return await _gateway.EmployeesActions(employeeDto);
        }

        public async Task<SingleResponse<int>> DeleteEmployee(Employee employee)
        {
            return await _gateway.DeleteEmployee(employee.EmployeeId);
        }

    }
}
