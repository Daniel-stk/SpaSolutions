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

namespace SpaSolutions.PartialViewModels.Workspaces
{
    public class WorkspacesListViewModel:ViewModelBase
    {
        private ObservableCollection<Workspace> _workspaces;
        public ICommand AddWorkspaceCommand { get; private set; }
        public ICommand EditWorkspaceCommand { get; private set; }
        public ObservableCollection<Workspace> Workspaces { get { return _workspaces; } private set { _workspaces = value; OnPropertyChanged("Workspaces"); } }

        public WorkspacesListViewModel(ObservableCollection<Workspace> workspaces)
        {
            Workspaces = workspaces;
            AddWorkspaceCommand = new DelegateCommand(o => OpenForm());
            EditWorkspaceCommand = new DelegateCommand(o => OpenEditForm(o));

        }

        private void OpenForm()
        {
            ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspaceFormViewModel();
        }

        private void OpenEditForm(object data)
        {
            var workspace = data as Workspace;
            if (workspace != null)
            {
                ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspaceFormViewModel(workspace);
            }
        }

        
    }
}
