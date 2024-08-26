﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using ScreenRecPro.view;

namespace ScreenRecPro

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private DispatcherTimer _timer;
        private TimeSpan _timeSpan;
        private bool _isRunning;

        List<Process> processList = new List<Process>();
        private bool multipleRunCount;
        Process[] processes;
        private bool isScreenshotActive;



        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            _timeSpan = TimeSpan.Zero;
            _isRunning = false;

            UpdateTimeLabel();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(1));
                UpdateTimeLabel();
            }
        }

        private void UpdateTimeLabel()
        {
            timeLabel.Content = _timeSpan.ToString(@"hh\:mm\:ss");
        }

        private void exitEvent(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void minimizeEvent(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void resetStatusFromLoading(object sender, DependencyPropertyChangedEventArgs e)
        {
            statusLabel.Content = "";
            statusLabel.Visibility = Visibility.Collapsed;
        }

        private async void readyRun(object sender, RoutedEventArgs e)
        {
            do
            {
                try
                {
                    Ping myPing = new Ping();
                    string host = "www.nibmworldwide.com";
                    byte[] buffer = new byte[32];
                    int timeout = 1000;
                    PingOptions pingOptions = new PingOptions();
                    PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                    System.Diagnostics.Debug.WriteLine("done");
                    statusLabel.Visibility = Visibility.Visible;
                    statusLabel.Content = "Checking Connection...";
                    await Task.Delay(3000);

                    statusLabel.Visibility = Visibility.Hidden;
                    loadingScreen.Visibility = Visibility.Hidden;
                    introScreen.Visibility = Visibility.Visible;
                    await Task.Delay(7000);
                    introScreen.Visibility = Visibility.Hidden;
                    await Task.Delay(000);
                    loginScreen.Visibility = Visibility.Visible;

                    break;
                }
                catch (Exception)
                {
                    welcomeScreen.Visibility = Visibility.Hidden;
                    statusLabel.Visibility = Visibility.Visible;
                    statusLabel.Content = "Something went wrong... Try again!";
                }
            } while (true);

        }

        private void clickHome(object sender, RoutedEventArgs e)
        {
            homeActive.Visibility = Visibility.Visible;
            SettingsActive.Visibility = Visibility.Hidden;
            infoActive.Visibility = Visibility. Hidden;

            homePane.Visibility = Visibility.Visible;
            settingsPane.Visibility = Visibility.Hidden;
            infoPane.Visibility = Visibility.Hidden;

            headTile.Content = "Home";
            infoTile.Text = "Welcome to SnapShot! Capture, organize, and share your screen effortlessly. Enhance your productivity with powerful features designed for a seamless screenshot experience.";

        }

        private void clickSettings(object sender, RoutedEventArgs e)
        {
            homeActive.Visibility = Visibility.Hidden;
            SettingsActive.Visibility = Visibility.Visible;
            infoActive.Visibility = Visibility.Hidden;

            homePane.Visibility = Visibility.Hidden;
            settingsPane.Visibility = Visibility.Visible;
            infoPane.Visibility = Visibility.Hidden;

            headTile.Content = "Settings";
            infoTile.Text = "Welcome to SnapShot! Capture, organize, and share your screen effortlessly. Enhance your productivity with powerful features designed for a seamless screenshot experience.";

        }

        private void clickinfo(object sender, RoutedEventArgs e)
        {
            homeActive.Visibility = Visibility.Hidden;
            SettingsActive.Visibility = Visibility.Hidden;
            infoActive.Visibility = Visibility.Visible;

            homePane.Visibility = Visibility.Hidden;
            settingsPane.Visibility = Visibility.Hidden;
            infoPane.Visibility = Visibility.Visible;

            headTile.Content = "Info";
            infoTile.Text = "Welcome to SnapShot! Capture, organize, and share your screen effortlessly. Enhance your productivity with powerful features designed for a seamless screenshot experience.";

        }

        private void playAction(object sender, RoutedEventArgs e)
        {
            if (play.Visibility == Visibility.Visible) { 
                play.Visibility = Visibility.Hidden;
                pause.Visibility = Visibility.Visible;
                timerStatus.Content = "Recording";

                getRunnigProgramms();
                if (multipleRunCount) { processLabel.Content = "Multiple Programms are running..."; } else { processLabel.Content = processes[0].MainWindowTitle; }
                StartScreenshotProcess(10,true);


                //panelView.Children.Clear();


                BlinkingEllipse.Fill = new SolidColorBrush(Colors.Red);
                bgEc.Fill = new SolidColorBrush(Colors.Transparent);

                if (!_isRunning)
                {
                    _isRunning = true;
                    _timer.Start();
                }
            }

        }

        private void pauseAction(object sender, RoutedEventArgs e)
        {
            if (pause.Visibility == Visibility.Visible) {
                pause.Visibility = Visibility.Hidden; 
                play.Visibility = Visibility.Visible;
                timerStatus.Content = "Paused";
                StartScreenshotProcess(0, false);
                BlinkingEllipse.Fill = new SolidColorBrush(Colors.Orange);
                bgEc.Fill = new SolidColorBrush(Colors.Orange);

                if (_isRunning)
                {
                    _isRunning = false;
                    _timer.Stop();
                }
            }

        }

        private async void stopAction(object sender, RoutedEventArgs e)
        {


            if ((pause.Visibility == Visibility.Visible && play.Visibility == Visibility.Hidden) || (pause.Visibility == Visibility.Hidden && play.Visibility == Visibility.Visible)) { 
                pause.Visibility = Visibility.Hidden; 
                play.Visibility = Visibility.Visible;
                _isRunning = false;
                _timer.Stop();
                _timeSpan = TimeSpan.Zero;

                UpdateTimeLabel();
                timerStatus.Content = "Stopped";
                StartScreenshotProcess(0, false);
                BlinkingEllipse.Fill = new SolidColorBrush(Colors.Gray);
                bgEc.Fill = new SolidColorBrush(Colors.Gray);
                await Task.Delay(1000);
            }

        }



        private void getRunnigProgramms() {
            processes = Process.GetProcesses();
            if (processes.Length > 1) { multipleRunCount = true; } else { multipleRunCount = false; }
            foreach (Process p in processes)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                { 
                    System.Diagnostics.Debug.WriteLine(p.MainWindowTitle);
                }
            }
        }

        private void update(string data)
        {
            logItem lg = new logItem();
            logView.Content = panelView;
            lg.filePath = data;
            lg.fileName = System.IO.Path.GetFileName(data);
            lg.status = "Unsent";
            lg.setImage(data);
            panelView.Children.Add(lg);

            
        }

        private async void StartScreenshotProcess(int intervalSeconds, bool initialState)
        {
            isScreenshotActive = initialState;
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(intervalSeconds)
            };

            timer.Tick += async (sender, args) =>
            {
                if (isScreenshotActive)
                {
                    string path = await TakeScreenshot(); 
                    update(path);
                }
                else
                {
                    timer.Stop(); 
                    timer.Tick -= Timer_Tick; 
                }
            };

            timer.Start();
        }

        private async Task<string> TakeScreenshot()
        {

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");
            string filename = System.IO.Path.Combine(folderPath, $"Screenshot_{timestamp}.png");


            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            var screenWidth = (int)SystemParameters.PrimaryScreenWidth;
            var screenHeight = (int)SystemParameters.PrimaryScreenHeight;

            using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
            {
      
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                }

    
                await Task.Run(() => bitmap.Save(filename, ImageFormat.Png));
                System.Diagnostics.Debug.WriteLine("Saved :::::" + filename);

            }


            return filename;
        }

        private void login(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "hold on tight.. Logging....";
            loginScreen.Visibility = Visibility.Hidden;
            welcomeScreen.Visibility = Visibility.Visible;
        }

        private void logout(object sender, RoutedEventArgs e)
        {

        }
    }
}