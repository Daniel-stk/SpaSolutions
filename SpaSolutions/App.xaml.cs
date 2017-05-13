using MahApps.Metro;
using SpaSolutions.Tools;
using SpaSolutions.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SpaSolutions
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/SpaSolutions;component/CustomAccents/CustomAccent1.xaml"));
            ThemeManager.AddAccent("CustomAccent2", new Uri("pack://application:,,,/SpaSolutions;component/CustomAccents/CustomAccent2.xaml"));
            ThemeManager.AddAppTheme("CustomTheme", new Uri("pack://application:,,,/SpaSolutions;component/CustomAccents/CustomTheme.xaml"));

            // create custom accents
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);
            var window = new MainWindow() { DataContext = MainWindowViewModel.Instance };
            window.Show();
        }
    }
}
