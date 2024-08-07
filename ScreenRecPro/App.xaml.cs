using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;

namespace ScreenRecPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _taskbarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "view", "cancel.ico");

            _taskbarIcon = new TaskbarIcon
            {
                Icon = new System.Drawing.Icon(iconPath), 
                ToolTipText = "Your Application Name"
            };

            _taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_Click;
        }

        private void TaskbarIcon_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
