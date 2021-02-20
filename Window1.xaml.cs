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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            Database1Entities db = new Database1Entities();
            var docs = from d in db.Doktors
                       select new
                       {

                           ImieDoktora = d.Imie,
                           NazwiskoDoktora = d.Nazwisko,
                           Specjalizacja = d.Specjalizacja,
                           Odzial = d.Odzial

                       };
            foreach (var item in docs)
            {
                Console.WriteLine(item.ImieDoktora);
                Console.WriteLine(item.NazwiskoDoktora);
                Console.WriteLine(item.Specjalizacja);
                Console.WriteLine(item.Odzial);

            }
            this.gridDoctors.ItemsSource = docs.ToList();
        }
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
    }
}
 
