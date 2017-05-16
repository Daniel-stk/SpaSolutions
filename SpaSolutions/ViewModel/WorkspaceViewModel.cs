using DataModel.InformationModels;
using ServiceDomain;
using SpaSolutions.Factory;
using SpaSolutions.PartialViewModels.Workspaces;
using SpaSolutions.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    public class WorkspacePageViewModel: ViewModelBase
    {
        private WorkspaceService _service;
        private ObservableCollection<Workspace> _workspaces;
        private int _workspacesQuantity;
        private int _todaysAppointmens;
        private ViewModelBase _currentAction;

        public TaskWatcher<ObservableCollection<Workspace>> GetWorkspacesTask { get; private set; }
        public ICommand ReturnToMainMenuCommand { get; private set; }

        public ObservableCollection<Workspace> Workspaces { get { return _workspaces; } private set { _workspaces = value; } }

        public WorkspaceService Service { get { return _service; } private set { _service = value; } }

        public int WorkspacesQuantity
        {
            get
            {
                return _workspacesQuantity;
            }
            private set
            {
                _workspacesQuantity = value;
                OnPropertyChanged("WorkspacesQuantity");
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
            set { _currentAction = value; WorkspacesQuantity = Workspaces.Count; OnPropertyChanged("CurrentAction"); }
        }

        public WorkspacePageViewModel()
        {
            ReturnToMainMenuCommand = new DelegateCommand(o => ReturnToMainMenu());
            Service = new WorkspaceService();
            GetWorkspacesTask = new TaskWatcher<ObservableCollection<Workspace>>(Service.GetAllWorkspaces());
            GetWorkspacesTask.Task.GetAwaiter().OnCompleted(() => PopulateBindingData());
        }

        private void PopulateBindingData()
        {
            Workspaces = GetWorkspacesTask.Result;
            WorkspacesQuantity = Workspaces.Count;
            CurrentAction = new WorkspacesListViewModel(Workspaces);
        }

        private void ReturnToMainMenu()
        {
            MainWindowViewModel.Instance.Animation = "RIGHT";
            MainWindowViewModel.Instance.CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }
    }
}
