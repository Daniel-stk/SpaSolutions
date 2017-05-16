using DataModel.InformationModels;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.PartialViewModels.Clients;
using SpaSolutions.Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    internal class ClientsPageViewModel : ViewModelBase
    {
        private ClientService _service;
        private ObservableCollection<Client> _clients;
        private int _clientsQuantity;
        private int _salesOnProcess;
        private int _todaysAppointmens;
        private ViewModelBase _currentAction;

        public TaskWatcher<ObservableCollection<Client>> GetClientsTask { get; private set; }
        public ICommand ReturnToMainMenuCommand { get; private set; }

        public ObservableCollection<Client> Clients { get { return _clients; } private set { _clients = value; } }
        
        public ClientService Service { get { return _service; } private set { _service = value; } }
        public int ClientsQuantity
        {
            get
            {
                return _clientsQuantity;
            }
            private set
            {
                _clientsQuantity = value;
                OnPropertyChanged("ClientsQuantity");
            }
        }

        public int SalesOnProcess
        {
            get
            {
                return _salesOnProcess;
            }
            private set
            {
                _salesOnProcess = value;
                OnPropertyChanged("SalesOnProcess");
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
            set { _currentAction = value; ClientsQuantity = Clients.Count; OnPropertyChanged("CurrentAction"); }
        }


        public ClientsPageViewModel()
        {
            ReturnToMainMenuCommand = new DelegateCommand(o => ReturnToMainMenu());
            Service = new ClientService();
            GetClientsTask = new TaskWatcher<ObservableCollection<Client>>(Service.GetAllClients());
            GetClientsTask.Task.GetAwaiter().OnCompleted(() => PopulateBindingData());
        }

        private void PopulateBindingData()
        {
            Clients = GetClientsTask.Result;
            ClientsQuantity = Clients.Count;
            CurrentAction = new ClientsListViewModel(Clients);
        }

        private void ReturnToMainMenu()
        {
            MainWindowViewModel.Instance.Animation = "RIGHT";
            MainWindowViewModel.Instance.CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }

       
    }
}
