using DataModel.ProductAndServicesModels;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.PartialViewModels.Services;
using SpaSolutions.Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    internal class ServiceAdministrationPageViewModel : ViewModelBase
    {
        private ServicesService _service;
        private ObservableCollection<Service> _services;
        private int _servicesQuantity;
        private int _todaysAppointmens;
        private int _mostPopularServices;
        private ViewModelBase _currentAction;

        public TaskWatcher<ObservableCollection<Service>> GetServicesTask { get; private set; }
        public ICommand ReturnToMainMenuCommand { get; private set; }

        public ObservableCollection<Service> Services { get { return _services; } private set { _services = value; } }

        public ServicesService Service { get { return _service; } private set { _service = value; } }

        public int ServicesQuantity
        {
            get
            {
                return _servicesQuantity;
            }
            private set
            {
                _servicesQuantity = value;
                OnPropertyChanged("ServicesQuantity");
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

        public int MostPopularServices
        {
            get
            {
                return _mostPopularServices;
            }
            private set
            {
                _mostPopularServices = value;
                OnPropertyChanged("MostPopularServices");
            }
        }

        public ViewModelBase CurrentAction
        {
            get { return _currentAction; }
            set { _currentAction = value; ServicesQuantity = Services.Count; OnPropertyChanged("CurrentAction"); }
        }


        public ServiceAdministrationPageViewModel()
        {
            ReturnToMainMenuCommand = new DelegateCommand(o => ReturnToMainMenu());
            Service = new ServicesService();
            GetServicesTask = new TaskWatcher<ObservableCollection<Service>>(Service.GetAllServices());
            GetServicesTask.Task.GetAwaiter().OnCompleted(() => PopulateBindingData());
        }

        private void PopulateBindingData()
        {
            Services = GetServicesTask.Result;
            ServicesQuantity = Services.Count;
            CurrentAction = new ServicesListViewModel(Services);
        }

        private void ReturnToMainMenu()
        {
            MainWindowViewModel.Instance.Animation = "RIGHT";
            MainWindowViewModel.Instance.CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }
    }
}