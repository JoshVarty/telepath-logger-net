using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace telepath_logger_net
{
    /// <summary>
    /// Design by Pongsakorn Poosankam
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        WebCamTracker webCamTracker;
        ScreenTracker screenTracker;
        private void mainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            webCamTracker = new WebCamTracker(this, imgVideo);
            screenTracker = new ScreenTracker();
        }

        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            screenTracker.Run();
            webCamTracker.Run();
        }
    }
}
