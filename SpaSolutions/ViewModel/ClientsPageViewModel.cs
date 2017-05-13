using DataModel.InformationModels;
using DebuggingTools;
using ServiceDomain;
using SpaSolutions.Tools;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpaSolutions.ViewModel
{
    internal class ClientsPageViewModel : ViewModelBase
    {
        private ClientService service;
        private int _clientsQuantity;
        private int _salesOnProcess;
        private int _todaysAppointmens;

        public TaskWatcher<ObservableCollection<Client>> GetClientsTask { get; private set; }

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


        public ClientsPageViewModel()
        {
            service = new ClientService();
            GetClientsTask = new TaskWatcher<ObservableCollection<Client>>(service.GetAllClients());
            GetClientsTask.Task.GetAwaiter().OnCompleted(() => PopulateBindingData());
        }

        private void PopulateBindingData()
        {
            var result = GetClientsTask.Result;
        }
    }
}
