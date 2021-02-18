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

namespace ProjektSem
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //displays window whenever application is running
            InitializeComponent();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            Window1 sW = new Window1();
            sW.Show();
            this.Close();         
        }
        private void btnThird_Click(object sender, RoutedEventArgs e)
        {
            Window2 sW = new Window2();
            sW.Show();
            this.Close();
        }
        private void btnForth_Click(object sender, RoutedEventArgs e)
        {
            Window3 sW = new Window3();
            sW.Show();
            this.Close();
        }
        private void btnFifth_Click(object sender, RoutedEventArgs e)
        {
            Window4 sW = new Window4();
            sW.Show();
            this.Close();
        }
    }
}
