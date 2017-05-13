using SpaSolutions.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaSolutions.ViewModel
{
    
    public class MainMenuViewModel:ViewModelBase
    {
        //Main Tool Pages
        public ICommand LoadClientsPageCommand { get; private set; }
        public ICommand LoadRetailPointPageCommand { get; private set; }
        public ICommand LoadAgendaPageCommand { get; private set; }
        public ICommand LoadWalkInManagerPageCommand { get; private set; }
      
        //Administratative tools
        public ICommand LoadServiceAdministrationPageCommand { get; private set; }
        public ICommand LoadInventoryAdministrationPageCommand { get; private set; }
        public ICommand LoadEmployeesAdministrationPageCommand { get; private set; }
        public ICommand LoadLoyaltySystemPageCommand { get; private set; }
        public ICommand LoadReportsSystemPageCommand { get; private set; }

        public MainMenuViewModel()
        {
            LoadClientsPageCommand = new DelegateCommand(o => LoadClientsPage());
            LoadAgendaPageCommand = new DelegateCommand(o => LoadAgendaPage());
            LoadRetailPointPageCommand = new DelegateCommand(o => LoadRetailPointPage());
            LoadWalkInManagerPageCommand = new DelegateCommand(o => LoadWalkInManagerPage());
            LoadServiceAdministrationPageCommand = new DelegateCommand(o => LoadServiceAdministrationPage());
            LoadInventoryAdministrationPageCommand = new DelegateCommand(o => LoadInventoryAdministrationPage());
            LoadLoyaltySystemPageCommand = new DelegateCommand(o => LoadReportsSystemPage());
            LoadReportsSystemPageCommand = new DelegateCommand(o => LoadReportsSystemPage());
        }

        private void LoadAgendaPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new AgendaPageViewModel();
        }

        private void LoadRetailPointPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new RetailPointPageViewModel();
        }

        private void LoadClientsPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new ClientsPageViewModel();
        }

        private void LoadWalkInManagerPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new WalkInManagerPageViewModel();
        }

        private void LoadServiceAdministrationPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new ServiceAdministrationPageViewModel();
        }

        private void LoadInventoryAdministrationPage()
        {
            MainWindowViewModel.Instance.CurrentView = new InventoryAdministrationPageViewModel();
        }

        private void LoadEmployeesAdministrationPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new EmployeesAdministrationPageViewModel();
        }

        private void LoadLoyaltySystemPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new LoyaltySystemPageViewModel();
        }

        private void LoadReportsSystemPage()
        {
            MainWindowViewModel.Instance.Animation = "Up";
            MainWindowViewModel.Instance.CurrentView = new ReportsSystemPageViewModel();
        }
    }
}
