using DataModel.ProductAndServicesModels;
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

namespace SpaSolutions.PartialViewModels.Services
{
    public class ServicesFormViewModel : BaseForm
    {
        private bool _isEdit;
        private bool _actionIsInProcess;
        private bool _addDiscount;

        public int ServiceId { private get { return service.ServiceId; } set { service.ServiceId = value; } }
        public string ServiceName { get { return service.ServiceName; } set { service.ServiceName = value; OnPropertyChanged("ServiceName"); } }
        public decimal ServicePrice { get { return service.ServicePrice; } set { service.ServicePrice = value; OnPropertyChanged("ServicePrice"); } }
        public bool ServiceActive { get { return service.Active; } set { service.Active = value; if (AddDiscount) { AddDiscount = value; } OnPropertyChanged("ServiceActive"); } }
        public bool AddDiscount { get { return _addDiscount; } set { _addDiscount = value; ServiceDiscountActive = value; OnPropertyChanged("AddDiscount"); } }
        public int ServiceDisocuntId { get { return service.ServiceDiscount.ServiceDiscountId; } set { service.ServiceDiscount.ServiceDiscountId = value; } }
        public decimal ServiceDiscount { get { return service.ServiceDiscount.ServiceDiscount; } set { service.ServiceDiscount.ServiceDiscount = value; OnPropertyChanged("ServiceDiscount"); } }
        public DateTime StartDiscountDate { get { return service.ServiceDiscount.StartDate; } set { service.ServiceDiscount.StartDate = value; OnPropertyChanged("StartDiscountDate"); } }
        public DateTime EndDiscountDate { get { return service.ServiceDiscount.EndDate; }  set { service.ServiceDiscount.EndDate = value;  OnPropertyChanged("EndDiscountDate"); } }
        public bool ServiceDiscountActive { get { return service.ServiceDiscount.Active; } set { service.ServiceDiscount.Active = value;  OnPropertyChanged("ServiceDiscountActive"); } }

        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; OnPropertyChanged("IsEdit"); } }
        public bool IsActionInProcess { get { return _actionIsInProcess; } set { _actionIsInProcess = value; OnPropertyChanged("IsActionInProcess"); } }
        private Service service { get; set; }
        public ServicesService ServicePlaceHolder { get; set; }
        public TaskWatcher<SingleResponse<ServiceDiscountActionsResponse>> ActionWatcher { get; private set; }


        public ServicesFormViewModel()
        {
            CreateCommands();
            service = new Service();
            service.ServiceDiscount = new ServiceDiscounts();
            ServicePlaceHolder = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Service;
            ServiceActive = true;
            AddDiscount = false;
            StartDiscountDate = DateTime.Today;
            EndDiscountDate = DateTime.Today.AddDays(1);
            IsActionInProcess = false;
        }

        public ServicesFormViewModel(Service service)
        {
            CreateCommands();
            this.service = new Service();
            this.service.ServiceDiscount = new ServiceDiscounts();
            ServiceId = service.ServiceId;
            ServiceName = service.ServiceName;
            ServicePrice = service.ServicePrice;
            ServiceActive = service.Active;
            StartDiscountDate = DateTime.Today;
            EndDiscountDate = DateTime.Today.AddDays(1);
            if (service.ServiceDiscount != null)
            {
                AddDiscount = service.ServiceDiscount.Active;
                ServiceDisocuntId = service.ServiceDiscount.ServiceDiscountId;
                ServiceDiscount = service.ServiceDiscount.ServiceDiscount;
                StartDiscountDate = service.ServiceDiscount.StartDate;
                EndDiscountDate = service.ServiceDiscount.EndDate;
                ServiceDiscountActive = service.ServiceDiscount.Active;
            } 
            ServicePlaceHolder = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Service;
            IsEdit = true;
            IsActionInProcess = false;

        }

        protected override void CancelAction()
        {
            var originalServicesList = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Services;
            ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesListViewModel(originalServicesList);
        }

        protected override void ConfirmAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<ServiceDiscountActionsResponse>>(ServicePlaceHolder.InsertOrEditService(service));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalServicesList = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Services;
                if (ActionWatcher.Result.Success == 1)
                {
                    if (IsEdit)
                    {
                        var serviceToUpdate = originalServicesList.FirstOrDefault(c => c.ServiceId == service.ServiceId);
                        if (serviceToUpdate != null)
                        {
                            serviceToUpdate.ServiceName = ServiceName;
                            serviceToUpdate.ServicePrice = ServicePrice;
                            serviceToUpdate.Active = ServiceActive;
                            if (ServiceDiscount > 0)
                            {
                                if(serviceToUpdate.ServiceDiscount == null)
                                {
                                    serviceToUpdate.ServiceDiscount = new ServiceDiscounts();
                                }
                                if(serviceToUpdate.ServiceDiscount.ServiceDiscountId == 0)
                                {
                                    serviceToUpdate.ServiceDiscount.ServiceDiscountId = ActionWatcher.Result.Data.ServiceDiscountId;
                                }
                                serviceToUpdate.ServiceDiscount.ServiceDiscount = ServiceDiscount;
                                serviceToUpdate.ServiceDiscount.StartDate = StartDiscountDate;
                                serviceToUpdate.ServiceDiscount.EndDate = EndDiscountDate;
                                serviceToUpdate.ServiceDiscount.Active = ServiceDiscountActive; 
                            }
                        }
                        else
                        {
                            ConsoleManager.Show();
                            Console.WriteLine("Ooppss! Error al actualizar cliente");
                        }
                    }
                    else
                    {
                        service.ServiceId = ActionWatcher.Result.Data.ServiceId;
                        service.ServiceDiscount.ServiceDiscountId = ActionWatcher.Result.Data.ServiceDiscountId;
                        ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Services.Add(service);
                    }
                    ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesListViewModel(originalServicesList);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error en edicion/adicion de cliente");
                }
                ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesListViewModel(originalServicesList);
            });
        }

        protected override void DeleteAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<ServiceDiscountActionsResponse>>(ServicePlaceHolder.DeleteService(service));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalServicesList = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Services;
                if (ActionWatcher.Result.Success == 1)
                {
                    var serviceToRemove = ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").Services.FirstOrDefault(c => c.ServiceId == service.ServiceId);
                    originalServicesList.Remove(serviceToRemove);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error al eliminar cliente");
                }
                ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesListViewModel(originalServicesList);

            });
        }
    }
}
