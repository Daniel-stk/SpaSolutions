using DataModel.ProductAndServicesModels;
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

namespace SpaSolutions.PartialViewModels.Services
{
    public class ServicesListViewModel:ViewModelBase
    {

        private ObservableCollection<Service> _services;
        public ICommand AddServiceCommand { get; private set; }
        public ICommand EditServiceCommand { get; private set; }
        public ObservableCollection<Service> Services { get { return _services; } private set { _services = value; OnPropertyChanged("Services"); } }


        public ServicesListViewModel(ObservableCollection<Service> services)
        {
            Services = services;
            AddServiceCommand = new DelegateCommand(o => OpenForm());
            EditServiceCommand = new DelegateCommand(o => OpenEditForm(o));
        }

        private void OpenForm()
        {
            ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesFormViewModel();
        }

        private void OpenEditForm(object data)
        {
            var service = data as Service;
            if (service != null)
            {
                ViewModelFactory<ServiceAdministrationPageViewModel>.GetView("ServiceAdministratorPage").CurrentAction = new ServicesFormViewModel(service);
            }
        }
    }
}
