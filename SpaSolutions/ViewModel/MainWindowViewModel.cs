using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SpaSolutions;
using System.ComponentModel;

namespace SpaSolutions.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private static readonly MainWindowViewModel _instance = new MainWindowViewModel();
        private MainMenuViewModel _menuInstance;

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

        public static MainWindowViewModel Instance
        {
            get
            {
                return _instance;
            }
        }

        public void ReturnToHomePage()
        {
            CurrentView = _menuInstance;
        }

        static MainWindowViewModel()
        {

        }

        private MainWindowViewModel()
        {
            _menuInstance = new MainMenuViewModel();
            CurrentView = _menuInstance;
        }

    }
}

