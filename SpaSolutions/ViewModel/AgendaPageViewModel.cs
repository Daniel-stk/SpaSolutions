using SpaSolutions.Factory;
using SpaSolutions.Tools;
using Syncfusion.UI.Xaml.Schedule;
using System.Windows;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    internal class AgendaPageViewModel : ViewModelBase
    {
        #region SfSchedule event commands

        #region Appointment Editor Opening 

        ICommand _editorOpeningCommand;

        public ICommand EditorOpeningCommand
        {
            get { return _editorOpeningCommand; }
        }
        public void editorOpening(AppointmentEditorOpeningEventArgs arg)
        {
          
        }
        #endregion

        #region AppointmentEditor Closed

        ICommand _editorClosedCommand;

        public ICommand EditorClosedCommand
        {
            get { return _editorClosedCommand; }
        }
        public void EditorClosed(AppointmentEditorClosedEventArgs arg)
        {

        }

        #endregion

        #region ContextMenu Opening Event

        ICommand _contextmenuOpening;


        public ICommand ContextMenuOpeningCommand
        {
            get { return _contextmenuOpening; }
        }
        public void ContextmenuOpening(ContextMenuOpeningEventArgs arg)
        {
            //MessageBox.Show("Triggered from VM and Context Menu Opening Event was cancelled");
        }
        #endregion

        #region Context menu Closed

        ICommand _contextmenuClosing;

        public ICommand ContextMenuClosingCommand
        {
            get { return _contextmenuClosing; }
        }
        public void ContextmenuClosing(ContextMenuClosedEventArgs arg)
        {
            //MessageBox.Show("Triggered from VM and Context Menu closed Event was Triggerd");
        }

        #endregion

        #region Visible Date Changing

        ICommand _visibleDateChanging;

        public ICommand VisibleDateChangingCommand
        {
            get { return _visibleDateChanging; }
        }
        public void VisibleDateChanging(VisibleDatesChangingEventArgs arg)
        {
            var oldValue = arg.OldValue;
            var newValue = arg.NewValue;
            if (oldValue != null) { }
                //MessageBox.Show("Visible date EventTriggered from VM");
        }

        #endregion

        #endregion

        private ScheduleType _level;
        public ScheduleType Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }

        public ICommand ReturnToMainMenuCommand { get; set; }
        public ICommand ChangeViewCommand { get; set; }

        public AgendaPageViewModel()
        {  
            _editorOpeningCommand = new Syncfusion.Windows.Shared.DelegateCommand<AppointmentEditorOpeningEventArgs>(editorOpening);
            _editorClosedCommand = new Syncfusion.Windows.Shared.DelegateCommand<AppointmentEditorClosedEventArgs>(EditorClosed);
            _contextmenuOpening = new Syncfusion.Windows.Shared.DelegateCommand<ContextMenuOpeningEventArgs>(ContextmenuOpening);
            _contextmenuClosing = new Syncfusion.Windows.Shared.DelegateCommand<ContextMenuClosedEventArgs>(ContextmenuClosing);
            _visibleDateChanging = new Syncfusion.Windows.Shared.DelegateCommand<VisibleDatesChangingEventArgs>(VisibleDateChanging);
            ReturnToMainMenuCommand = new DelegateCommand(o => ReturnToMainMenu());
            ChangeViewCommand = new DelegateCommand(o => ChangeView());
            Level = ScheduleType.Month;
        }

        private void ReturnToMainMenu()
        {
            MainWindowViewModel.Instance.Animation = "RIGHT";
            MainWindowViewModel.Instance.CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }

        private void ChangeView()
        {
            int currentViewType = (int)Level;
            if(currentViewType == 0)
            {
                Level = ScheduleType.TimeLine;
            }
            else
            {
                Level = (ScheduleType)(currentViewType - 1);
            }
        }
    }
}