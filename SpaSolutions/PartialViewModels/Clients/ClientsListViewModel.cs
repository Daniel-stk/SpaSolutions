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

namespace SpaSolutions.PartialViewModels.Clients
{
    public class ClientsListViewModel:ViewModelBase
    {
        private ObservableCollection<Client> _clients;
        public ICommand AddClientCommand { get; private set; }
        public ICommand EditClientCommand { get; private set; }
        public ObservableCollection<Client> Clients { get { return _clients; } private set { _clients = value; OnPropertyChanged("Clients"); } }
        public ClientsListViewModel(ObservableCollection<Client> clients)
        {
            Clients = clients;
            AddClientCommand = new DelegateCommand(o => OpenForm());
            EditClientCommand = new DelegateCommand(o => OpenEditForm(o));
        }

        private void OpenForm()
        {
            ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsFormViewModel();
        }

        private void OpenEditForm(object data)
        {
            var client = data as Client;
            if(client != null) { 
                ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsFormViewModel(client);
            }
        }
    }
}
