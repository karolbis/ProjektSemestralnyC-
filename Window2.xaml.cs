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
using System.Windows.Shapes;

namespace ProjektSem
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

            Database1Entities db = new Database1Entities();
            var pacj = from p in db.Pacjents
                       select new
                       {

                           ImiePacjenta = p.Imie,
                           NazwiskoPacjenta = p.Nazwisko,
                           WiekPacjenta = p.Wiek,
                           PeselPacjenta = p.Pesel,
                           ChorobaPacjenta = p.Choroba
                           

                       };
            foreach (var item in pacj)
            {
                Console.WriteLine(item.ImiePacjenta);
                Console.WriteLine(item.NazwiskoPacjenta);
                Console.WriteLine(item.WiekPacjenta);
                Console.WriteLine(item.PeselPacjenta);
                Console.WriteLine(item.ChorobaPacjenta);
                

            }
            this.gridPacjenci.ItemsSource = pacj.ToList();
        }
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
    }
}
