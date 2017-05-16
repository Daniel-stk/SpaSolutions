using DataModel.InformationModels;
using DebuggingTools;
using Gateway.Response;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.Tools;
using SpaSolutions.ViewModel;
using System;
using System.Linq;


namespace SpaSolutions.PartialViewModels.Clients
{
    public class ClientsFormViewModel : BaseForm
    {
        private bool _isEdit;
        private bool _actionIsInProcess;
        public int ClientId { private get { return client.ClientId; } set { client.ClientId = value; }  }
        public string ClientName { get { return client.ClientName; } set { client.ClientName = value;  OnPropertyChanged("ClientName"); } }
        public string ClientAddress { get { return client.ClientAddress; } set { client.ClientAddress = value; OnPropertyChanged("ClientAddress"); } }
        public string ClientPhone { get { return client.ClientPhone; } set { client.ClientPhone = value; OnPropertyChanged("ClientPhone"); } }
        public string ClientCellPhone { get { return client.ClientCellPhone; } set { client.ClientCellPhone = value; OnPropertyChanged("ClientCellPhone"); } }
        public string ClientEmail { get { return client.ClientEmail; } set { client.ClientEmail = value; OnPropertyChanged("ClientEmail"); } }
        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; OnPropertyChanged("IsEdit"); } }
        public bool IsActionInProcess { get { return _actionIsInProcess; } set { _actionIsInProcess = value;  OnPropertyChanged("IsActionInProcess"); } }
        private Client client { get; set; }       
        public ClientService ServicePlaceHolder { get; set; }
        public TaskWatcher<SingleResponse<int>> ActionWatcher { get; private set; }
       

        public ClientsFormViewModel()
        {
            CreateCommands();
            client = new Client();
            ServicePlaceHolder = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Service;
            IsActionInProcess = false;
        }

        
        public ClientsFormViewModel(Client client)
        {
            CreateCommands();
            this.client = new Client();
            ClientId = client.ClientId;
            ClientName = client.ClientName;
            ClientPhone = client.ClientPhone;
            ClientAddress = client.ClientAddress;
            ClientCellPhone = client.ClientCellPhone;
            ClientEmail = client.ClientEmail;
            ServicePlaceHolder = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Service;
            IsEdit = true;
            IsActionInProcess = false;
        }


        protected override void CancelAction()
        {
            var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
            ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").CurrentAction = new ClientsListViewModel(originalClientsList);
        }

        protected override void ConfirmAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.InsertOrEditClient(client));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
                if (ActionWatcher.Result.Success == 1)
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
                        client.ClientId = ActionWatcher.Result.Data;
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
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.DeleteClient(client));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalClientsList = ViewModelFactory<ClientsPageViewModel>.GetView("ClientsPage").Clients;
                if (ActionWatcher.Result.Success == 1)
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
