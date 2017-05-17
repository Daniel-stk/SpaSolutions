using DataModel.InformationModels;
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

namespace SpaSolutions.PartialViewModels.Workspaces
{
    public class WorkspaceFormViewModel : BaseForm
    {
        private bool _isEdit;
        private bool _actionIsInProcess;
        public int WorkspaceId { private get { return workspace.WorkspaceId; } set { workspace.WorkspaceId = value; } }
        public string WorkspaceName { get { return workspace.WorkspaceName; } set { workspace.WorkspaceName = value; OnPropertyChanged("WorkspaceName"); } }
        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; OnPropertyChanged("IsEdit"); } }
        public bool IsActionInProcess { get { return _actionIsInProcess; } set { _actionIsInProcess = value; OnPropertyChanged("IsActionInProcess"); } }
        private Workspace workspace { get; set; }
        public WorkspaceService ServicePlaceHolder { get; set; }
        public TaskWatcher<SingleResponse<int>> ActionWatcher { get; private set; }


        public WorkspaceFormViewModel()
        {
            CreateCommands();
            workspace = new Workspace();
            ServicePlaceHolder = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Service;
            IsActionInProcess = false;
        }

        public WorkspaceFormViewModel(Workspace workspace)
        {
            CreateCommands();
            this.workspace = new Workspace();
            WorkspaceId = workspace.WorkspaceId;
            WorkspaceName = workspace.WorkspaceName;
            ServicePlaceHolder = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Service;
            IsEdit = true;
            IsActionInProcess = false;

        }

        protected override void CancelAction()
        {
            var originalWorkspaceList = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Workspaces;
            ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspacesListViewModel(originalWorkspaceList);
        }

        protected override void ConfirmAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.InsertOrEditWorkspace(workspace));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalWorkspacesList = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Workspaces;
                if (ActionWatcher.Result.Success == 1)
                {
                    if (IsEdit)
                    {
                        var workspaceToUpdate = originalWorkspacesList.FirstOrDefault(c => c.WorkspaceId == workspace.WorkspaceId);
                        if (workspaceToUpdate != null)
                        {
                            workspaceToUpdate.WorkspaceName = WorkspaceName;
                        }
                        else
                        {
                            ConsoleManager.Show();
                            Console.WriteLine("Ooppss! Error al actualizar cliente");
                        }
                    }
                    else
                    {
                        workspace.WorkspaceId = ActionWatcher.Result.Data;
                        ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Workspaces.Add(workspace);
                    }
                    ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspacesListViewModel(originalWorkspacesList);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error en edicion/adicion de cliente");
                }
                ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspacesListViewModel(originalWorkspacesList);
            });
        }

        protected override void DeleteAction()
        {
            ActionWatcher = new TaskWatcher<SingleResponse<int>>(ServicePlaceHolder.DeleteWorkspace(workspace));
            ActionWatcher.Task.GetAwaiter().OnCompleted(() => {
                var success = ActionWatcher.Result;
                var originalWorkspacesList = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Workspaces;
                if (ActionWatcher.Result.Success == 1)
                {
                    var employeeToRemove = ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").Workspaces.FirstOrDefault(c => c.WorkspaceId == workspace.WorkspaceId);
                    originalWorkspacesList.Remove(employeeToRemove);
                }
                else
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Ooppss! Error al eliminar cliente");
                }
                ViewModelFactory<WorkspacePageViewModel>.GetView("WorkspacePage").CurrentAction = new WorkspacesListViewModel(originalWorkspacesList);

            });
        }
    }
}
