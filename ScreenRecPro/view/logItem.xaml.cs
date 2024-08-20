using System;
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
    /// Interaction logic for logItem.xaml
    /// </summary>
    public partial class logItem : UserControl
    {
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string status { get; set; }

        public logItem()
        {
            InitializeComponent();

            DataContext = this;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("C:\\Users\\wenuj\\source\\repos\\ScreenRecPro\\ScreenRecPro\\bin\\Debug\\net8.0-windows\\upload\\Screenshot_20240820_134625.png", UriKind.Absolute);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            imgSrc.Source = bitmapImage;

        }
    }
}
