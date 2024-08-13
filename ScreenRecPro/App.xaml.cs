using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;

namespace ScreenRecPro
{
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
                ToolTipText = "ScreenRecPro"
            };

            _taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_Click;
        }

        private void TaskbarIcon_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            if (MainWindow == null)
            {
                _taskbarIcon?.Dispose();
                Shutdown(); 
                return;
            }

            if (MainWindow.WindowState == WindowState.Minimized)
            {
                MainWindow.WindowState = WindowState.Normal;
            }

            MainWindow.Show();
            MainWindow.Activate();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _taskbarIcon?.Dispose();
            base.OnExit(e);
        }
    }
}
