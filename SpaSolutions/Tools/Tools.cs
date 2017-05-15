using MahApps.Metro;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Globalization;
using DebuggingTools;
using System.Windows.Controls;

namespace SpaSolutions.Tools
{
    public static class Configurations
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static Configuration ReadConfiguration(string configFilePath)
        {
            var absolutePath = Path.Combine(AssemblyDirectory, configFilePath);
            var configMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = absolutePath
            };
            return ConfigurationManager.OpenMappedExeConfiguration(configMap,ConfigurationUserLevel.None);
        }
    }

    public static class Clock
    {
      
    }

    public static class ThemeManagerHelper
    {
        public static void CreateAppStyleBy(System.Windows.Media.Color color, bool changeImmediately = false)
        {
            // create a runtime accent resource dictionary

            var resourceDictionary = new ResourceDictionary();

            resourceDictionary.Add("HighlightColor", color);
            resourceDictionary.Add("AccentBaseColor", color);
            resourceDictionary.Add("AccentColor", Color.FromArgb((byte)(204), color.R, color.G, color.B));
            resourceDictionary.Add("AccentColor2", Color.FromArgb((byte)(153), color.R, color.G, color.B));
            resourceDictionary.Add("AccentColor3", Color.FromArgb((byte)(102), color.R, color.G, color.B));
            resourceDictionary.Add("AccentColor4", Color.FromArgb((byte)(51), color.R, color.G, color.B));

            resourceDictionary.Add("HighlightBrush", GetSolidColorBrush((Color)resourceDictionary["HighlightColor"]));
            resourceDictionary.Add("AccentBaseColorBrush", GetSolidColorBrush((Color)resourceDictionary["AccentBaseColor"]));
            resourceDictionary.Add("AccentColorBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));
            resourceDictionary.Add("AccentColorBrush2", GetSolidColorBrush((Color)resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("AccentColorBrush3", GetSolidColorBrush((Color)resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("AccentColorBrush4", GetSolidColorBrush((Color)resourceDictionary["AccentColor4"]));
            resourceDictionary.Add("WindowTitleColorBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));

            resourceDictionary.Add("ProgressBrush", new LinearGradientBrush(
                new GradientStopCollection(new[]
                {
                    new GradientStop((Color)resourceDictionary["HighlightColor"], 0),
                    new GradientStop((Color)resourceDictionary["AccentColor3"], 1)
                }),
                new Point(0.001, 0.5), new Point(1.002, 0.5)));

            resourceDictionary.Add("CheckmarkFill", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));
            resourceDictionary.Add("RightArrowFill", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));

            resourceDictionary.Add("IdealForegroundColor", IdealTextColor(color));
            resourceDictionary.Add("IdealForegroundColorBrush", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("IdealForegroundDisabledBrush", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"], 0.4));
            resourceDictionary.Add("AccentSelectedColorBrush", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"]));

            resourceDictionary.Add("MetroDataGrid.HighlightBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.HighlightTextBrush", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"]));
            resourceDictionary.Add("MetroDataGrid.MouseOverHighlightBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor3"]));
            resourceDictionary.Add("MetroDataGrid.FocusBorderBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightBrush", GetSolidColorBrush((Color)resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("MetroDataGrid.InactiveSelectionHighlightTextBrush", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"]));

            resourceDictionary.Add("MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchBrush.Win10", GetSolidColorBrush((Color)resourceDictionary["AccentColor"]));
            resourceDictionary.Add("MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchMouseOverBrush.Win10", GetSolidColorBrush((Color)resourceDictionary["AccentColor2"]));
            resourceDictionary.Add("MahApps.Metro.Brushes.ToggleSwitchButton.ThumbIndicatorCheckedBrush.Win10", GetSolidColorBrush((Color)resourceDictionary["IdealForegroundColor"]));

            // applying theme to MahApps

            var resDictName = string.Format("ApplicationAccent_{0}.xaml", color.ToString().Replace("#", string.Empty));
            var fileName = Path.Combine(Path.GetTempPath(), resDictName);
            using (var writer = System.Xml.XmlWriter.Create(fileName, new System.Xml.XmlWriterSettings { Indent = true }))
            {
                System.Windows.Markup.XamlWriter.Save(resourceDictionary, writer);
                writer.Close();
            }

            resourceDictionary = new ResourceDictionary() { Source = new Uri(fileName, UriKind.Absolute) };

            var newAccent = new Accent { Name = resDictName, Resources = resourceDictionary };
            ThemeManager.AddAccent(newAccent.Name, newAccent.Resources.Source);

            if (changeImmediately)
            {
                var application = Application.Current;
                //var applicationTheme = ThemeManager.AppThemes.First(x => string.Equals(x.Name, "BaseLight"));
                // detect current application theme
                Tuple<AppTheme, Accent> applicationTheme = ThemeManager.DetectAppStyle(application);
                ThemeManager.ChangeAppStyle(application, newAccent, applicationTheme.Item1);
            }
        }

        /// <summary>
        /// Determining Ideal Text Color Based on Specified Background Color
        /// http://www.codeproject.com/KB/GDI-plus/IdealTextColor.aspx
        /// </summary>
        /// <param name = "color">The bg.</param>
        /// <returns></returns>
        private static Color IdealTextColor(Color color)
        {
            const int nThreshold = 105;
            var bgDelta = System.Convert.ToInt32((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        private static SolidColorBrush GetSolidColorBrush(Color color, double opacity = 1d)
        {
            var brush = new SolidColorBrush(color) { Opacity = opacity };
            brush.Freeze();
            return brush;
        }
    }

    public class DelegateCommand : ICommand
    {

        private Action<object> commandToExecute;
        private Predicate<object> canExecuteCommand;

       
        public DelegateCommand(Action<object> execute):this(execute,null)
        {

        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            commandToExecute = execute;
            canExecuteCommand = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            return canExecuteCommand == null ? true : canExecuteCommand(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("The command can't be executed");
            }
            commandToExecute(parameter);
        }
    }

    public sealed class TaskWatcher<TResult> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Task<TResult> Task { get; private set; }
        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
            }
        }
        public TaskStatus Status { get { return Task.Status; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsNotCompleted { get { return !Task.IsCompleted; } }
        public bool IsSuccessfullyCompleted
        {
            get
            {
                return Task.Status == TaskStatus.RanToCompletion;
            }
        }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException
        {
            get
            {
                return (Exception == null) ? null : Exception.InnerException;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return (InnerException == null) ? null : InnerException.Message;
            }
        }

        public TaskWatcher(Task<TResult> TaskToWatch)
        {
            Task = TaskToWatch;
            if (!TaskToWatch.IsCompleted)
            {
                var _ = WatchTaskAsync(TaskToWatch);
            }
        }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch(Exception e)
            {
                ConsoleManager.Show();
                Console.WriteLine(e.Message);
            }
            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs("Status"));
            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));
            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                propertyChanged(this,
                  new PropertyChangedEventArgs("InnerException"));
                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
            else
            {
                propertyChanged(this,
                  new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("Result"));
            }
        }
    }

    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;
            if (IsVisibilityInverted(parameter))
            {
                isVisible = !isVisible;
            }
            return (isVisible ? Visibility.Visible : Visibility.Collapsed);
        }

        private bool IsVisibilityInverted(object parameter)
        {
            return (GetVisibilityMode(parameter) == Visibility.Collapsed);
        }

        private Visibility GetVisibilityMode(object parameter)
        {
            Visibility mode = Visibility.Visible;
            if(parameter != null)
            {
                if(parameter is Visibility)
                {
                    mode = (Visibility)parameter;
                }
                else
                {
                    try
                    {
                        mode = (Visibility)Enum.Parse(typeof(Visibility), parameter.ToString(), true);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException("Invalid Visibility specified as the ConverterParameter.  Use Visible or Collapsed.", e);
                    }
                }
            }

            return mode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
