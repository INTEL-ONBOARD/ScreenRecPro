﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenRecPro.view
{
    /// <summary>
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class report : UserControl
    {
        private MainWindow window;

        public string id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string input { get; set; }

        public report(MainWindow win)
        {
            InitializeComponent();
            this.window = win;
            DataContext = this;

        }

        private void active(object sender, MouseButtonEventArgs e)
        {
            window.info_count.Visibility = Visibility.Visible;
            window.info_title.Visibility = Visibility.Visible;
            window.info_subtitle.Visibility = Visibility.Visible;
            window.info_input.Visibility = Visibility.Visible;
            window.info_updateBtn.Visibility = Visibility.Visible;
            window.info_clearBtn.Visibility = Visibility.Visible;

            window.info_count.Content = (id +"/"+ window.total);
        }
    }
}
