using SpaSolutions.Factory;

namespace SpaSolutions.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private static MainWindowViewModel _instance = new MainWindowViewModel();
        private ViewModelBase _currentView;
        private string _animation;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }
    
        public string Animation
        {
            get { return _animation; }
            set
            {
                _animation = value;
                OnPropertyChanged("Animation");
            }
        }

        static MainWindowViewModel()
        {

        }
 
        private MainWindowViewModel()
        {
            CurrentView = ViewModelFactory<MainMenuViewModel>.GetView("MainMenu");
        }

        public static MainWindowViewModel Instance { get { return _instance; } }

    }
}

