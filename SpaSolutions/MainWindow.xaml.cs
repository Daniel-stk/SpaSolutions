using MahApps.Metro.Controls;
using System.Windows;

namespace SpaSolutions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MetroWindow accentThemeTestWindow;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (accentThemeTestWindow != null)
            {
                accentThemeTestWindow.Activate();
                return;
            }

            accentThemeTestWindow = new AccentStyleWindow();
            accentThemeTestWindow.Owner = this;
            accentThemeTestWindow.Closed += (o, args) => accentThemeTestWindow = null;
            accentThemeTestWindow.Left = this.Left + this.ActualWidth / 2.0;
            accentThemeTestWindow.Top = this.Top + this.ActualHeight / 2.0;
            accentThemeTestWindow.Show();
        }
    }
}
