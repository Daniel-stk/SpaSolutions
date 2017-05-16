using DataModel.InformationModels;
using DebuggingTools;
using Gateway.Response;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.Tools;
using SpaSolutions.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaSolutions.PartialViewModels.Employees
{
    public class EmployeesFormViewModel : BaseForm
    {
        private bool _isEdit;
        private bool _actionIsInProcess;
        public int EmployeeId { private get { return employee.EmployeeId; } set { employee.EmployeeId = value; } }
        public string EmployeeName { get { return employee.EmployeeName; } set { employee.EmployeeName = value; OnPropertyChanged("EmployeeName"); } }
        public string EmployeeAddress { get { return employee.EmployeeAddress; } set { employee.EmployeeAddress = value; OnPropertyChanged("EmployeeAddress"); } }
        public string EmployeePhone { get { return employee.EmployeeHomePhone; } set { employee.EmployeeHomePhone = value; OnPropertyChanged("EmployeePhone"); } }
        public string EmployeeCellPhone { get { return employee.EmployeeCellPhone; } set { employee.EmployeeCellPhone = value; OnPropertyChanged("EmployeeCellPhone"); } }
        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; OnPropertyChanged("IsEdit"); } }
        public bool IsActionInProcess { get { return _actionIsInProcess; } set { _actionIsInProcess = value; OnPropertyChanged("IsActionInProcess"); } }
        private Employee employee { get; set; }
        public EmployeeService ServicePlaceHolder { get; set; }
        public TaskWatcher<SingleResponse<int>> ActionWatcher { get; private set; }

        public EmployeesFormViewModel()
        {
            CreateCommands();
            employee = new Employee();
            ServicePlaceHolder = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Service;
            IsActionInProcess = false;
        }


        public EmployeesFormViewModel(Employee employee)
        {
            CreateCommands();
            this.employee = new Employee();
            EmployeeId = employee.EmployeeId;
            EmployeeName = employee.EmployeeName;
            EmployeePhone = employee.EmployeeHomePhone;
            EmployeeAddress = employee.EmployeeAddress;
            EmployeeCellPhone = employee.EmployeeCellPhone;
            ServicePlaceHolder = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Service;
            IsEdit = true;
            IsActionInProcess = false;
        }

        protected override void CancelAction()
        {
            var originalEmployeesList = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Employees;
            ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesListViewModel(originalEmployeesList);
        }

        protected override void ConfirmAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.InsertOrEditEmployee(employee));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalEmployeesList = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Employees;
                if (ActionWatcher.Result.Success == 1)
                {
                    if (IsEdit)
                    {
                        var employeeToUpdate = originalEmployeesList.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);
                        if (employeeToUpdate != null)
                        {
                            employeeToUpdate.EmployeeName = EmployeeName;
                            employeeToUpdate.EmployeeAddress = EmployeeAddress;
                            employeeToUpdate.EmployeeCellPhone = EmployeeCellPhone;
                            employeeToUpdate.EmployeeHomePhone = EmployeePhone;
                        }
                        else
                        {
                            ConsoleManager.Show();
                            Console.WriteLine("Ooppss! Error al actualizar cliente");
                        }
                    }
                    else
                    {
                        employee.EmployeeId = ActionWatcher.Result.Data;
                        ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Employees.Add(employee);
                    }
                    ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesListViewModel(originalEmployeesList);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error en edicion/adicion de cliente");
                }
                ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesListViewModel(originalEmployeesList);
            });
        }

        protected override void DeleteAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.DeleteEmployee(employee));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalEmployeesList = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Employees;
                if (ActionWatcher.Result.Success == 1)
                {
                    var employeeToRemove = ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").Employees.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);
                    originalEmployeesList.Remove(employeeToRemove);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error al eliminar cliente");
                }
                ViewModelFactory<EmployeesAdministrationPageViewModel>.GetView("EmployeesAdministrationPage").CurrentAction = new EmployeesListViewModel(originalEmployeesList);

            });
        }
    }
}
