using System;
using System.Windows;

namespace ScreenRecPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Hardcodet.Wpf.TaskbarNotification.TaskbarIcon _taskbarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            _taskbarIcon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon
            {
                Icon = new System.Drawing.Icon("pack://application:,,,/ScreenRecPro;view/cancel.ico"), // Set your icon path
                ToolTipText = "Your Application Name"
            };

            _taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_Click;
        }

        private void TaskbarIcon_Click(object sender, RoutedEventArgs e)
        {
            // Handle double-click on the tray icon (e.g., show main window)
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            // Show your main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
