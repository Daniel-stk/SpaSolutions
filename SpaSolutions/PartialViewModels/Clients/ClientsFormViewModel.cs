using DataModel.InformationModels;
using DebuggingTools;
using ServiceDomain;
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
    public class ClientsFormViewModel : BaseForm
    {
        private bool _isEdit;
        public string ClientName { get { return client.ClientName; } set { client.ClientName = value;  OnPropertyChanged("ClientName"); } }
        public string ClientAddress { get { return client.ClientAddress; } set { client.ClientAddress = value; OnPropertyChanged("ClientAddress"); } }
        public string ClientPhone { get { return client.ClientPhone; } set { client.ClientPhone = value; OnPropertyChanged("ClientPhone"); } }
        public string ClientCellPhone { get { return client.ClientCellPhone; } set { client.ClientCellPhone = value; OnPropertyChanged("ClientCellPhone"); } }
        public string ClientEmail { get { return client.ClientEmail; } set { client.ClientEmail = value; OnPropertyChanged("ClientEmail"); } }
        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; OnPropertyChanged("IsEdit"); } }
        private Client client { get; set; }       
        public ClientService ServicePlaceHolder { get; set; }
        public TaskWatcher<bool> ActionWatcher { get; private set; }
       

        public ClientsFormViewModel()
        {
            CreateCommands();
            ServicePlaceHolder = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Service;
        }

        
        public ClientsFormViewModel(Client client)
        {
            CreateCommands();
            ServicePlaceHolder = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Service;
            IsEdit = true;

        }

       
        protected override void CancelAction()
        {
            var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
            ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsListViewModel(originalClientsList);
        }

        protected override void ConfirmAction()
        {
            ActionWatcher = new TaskWatcher<bool>(ServicePlaceHolder.InsertOrEditClient(client));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
                if (success)
                {
                    if (IsEdit)
                    {
                        var clientToUpdate = originalClientsList.FirstOrDefault(c => c.ClientId == client.ClientId);
                        if (clientToUpdate != null)
                        {
                            clientToUpdate.ClientName = ClientName;
                            clientToUpdate.ClientAddress = ClientAddress;
                            clientToUpdate.ClientCellPhone = ClientCellPhone;
                            clientToUpdate.ClientPhone = ClientPhone;
                            clientToUpdate.ClientEmail = ClientEmail;
                        }
                        else
                        {
                            ConsoleManager.Show();
                            Console.WriteLine("Ooppss! Error al actualizar cliente");
                        }
                    }
                    else
                    {
                        ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients.Add(client);
                 
                    }
                    ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsListViewModel(originalClientsList);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error en edicion/adicion de cliente");
                }
                ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsListViewModel(originalClientsList);
            });
        }

        protected override void DeleteAction()
        {
            ActionWatcher = new TaskWatcher<bool>(ServicePlaceHolder.DeleteClient(client));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
                if (success)
                {
                    var clientToRemove = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients.FirstOrDefault(c => c.ClientId == client.ClientId);
                    originalClientsList.Remove(clientToRemove);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error al eliminar cliente");
                }
                ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsListViewModel(originalClientsList);

            });
        }
    }
}
