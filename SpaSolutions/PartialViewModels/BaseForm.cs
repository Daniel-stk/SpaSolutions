using SpaSolutions.Tools;
using SpaSolutions.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaSolutions.PartialViewModels
{
    public abstract class BaseForm : ViewModelBase
    {
        public ICommand CancelActionCommand { get; set; }
        public ICommand ConfirmActionCommand { get; set; }
        public ICommand DeleteActionCommand { get; set; }
        protected virtual void CreateCommands()
        {
            ConfirmActionCommand = new DelegateCommand(o => ConfirmAction());
            CancelActionCommand = new DelegateCommand(o => CancelAction());
            DeleteActionCommand = new DelegateCommand(o => DeleteAction());

        }
        protected abstract void CancelAction();
        protected abstract void ConfirmAction();
        protected abstract void DeleteAction();
    }
}
