using DataModel.InformationModels;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.PartialViewModels.Employees;
using SpaSolutions.Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    internal class EmployeesAdministrationPageViewModel : ViewModelBase
    {
        private EmployeeService _service;
        private ObservableCollection<Employee> _employees;
        private int _employeesQuantity;
        private int _todaysAppointmens;
        private ViewModelBase _currentAction;


        public TaskWatcher<ObservableCollection<Employee>> GetEmployeesTask { get; private set; }
        public ICommand ReturnToMainMenuCommand { get; private set; }

        public ObservableCollection<Employee> Employees { get { return _employees; } private set { _employees = value; } }

        public EmployeeService Service { get { return _service; } private set { _service = value; } }

        public int EmployeesQuantity
        {
            get
            {
                return _employeesQuantity;
            }
            private set
            {
                _employeesQuantity = value;
                OnPropertyChanged("EmployeesQuantity");
            }
        }

        public int TodaysAppointments
        {
            get
            {
                return _todaysAppointmens;
            }
            private set
            {
                _todaysAppointmens = value;
                OnPropertyChanged("TodaysAppointments");
            }
        }

        public ViewModelBase CurrentAction
        {
            get { return _currentAction; }
            set { _currentAction = value; EmployeesQuantity = Employees.Count; OnPropertyChanged("CurrentAction"); }
        }


        public EmployeesAdministrationPageViewModel()
        {
            ReturnToMainMenuCommand = new DelegateCommand(o => ReturnToMainMenu());
            Service = new EmployeeService();
            GetEmployeesTask = new TaskWatcher<ObservableCollection<Employee>>(Service.GetAllEmployees());
            GetEmployeesTask.Task.GetAwaiter().OnCompleted(() => PopulateBindingData());

        }

        private void PopulateBindingData()
        {
            Employees = GetEmployeesTask.Result;
            EmployeesQuantity = Employees.Count;
            CurrentAction = new EmployeesListViewModel(Employees);
        }

        private void ReturnToMainMenu()
        {
            MainWindowViewModel.Instance.Animation = "RIGHT";
            MainWindowViewModel.Instance.CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }
    }
}