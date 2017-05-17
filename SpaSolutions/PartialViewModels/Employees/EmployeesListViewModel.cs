using DataModel.InformationModels;
using SpaSolutions.Factory;
using SpaSolutions.Tools;
using SpaSolutions.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaSolutions.PartialViewModels.Employees
{
    public class EmployeesListViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> _employee;
        public ICommand AddEmployeeCommand { get; private set; }
        public ICommand EditEmployeeCommand { get; private set; }
        public ObservableCollection<Employee> Employees { get { return _employee; } private set { _employee = value; OnPropertyChanged("Employees"); } }

        public EmployeesListViewModel(ObservableCollection<Employee> employees)
        {
            Employees = employees;
            AddEmployeeCommand = new DelegateCommand(o => OpenForm());
            EditEmployeeCommand = new DelegateCommand(o => OpenEditForm(o));
        }

        private void OpenForm()
        {
            ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesFormViewModel();
        }

        private void OpenEditForm(object data)
        {
            var employee = data as Employee;
            if (employee != null)
            {
                ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesFormViewModel(employee);
            }
        }
    }
}
