using DataModel.InformationModels;
using SpaSolutions.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaSolutions.PartialViewModels.Workspaces
{
    public class WorkspacesListViewModel:ViewModelBase
    {
        public WorkspacesListViewModel(ObservableCollection<Workspace> workspaces)
        {
        }
    }
}
