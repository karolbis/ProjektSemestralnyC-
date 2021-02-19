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
    /// Logika interakcji dla klasy Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();

            Database1Entities db = new Database1Entities();
            var Diag = from di in db.Diagnozas
                      select new
                      {

                          IdWizyty = di.WizytaID,
                          Nazwa = di.Nazwa,
                          Opis = di.Opis,
                         



                      };
            foreach (var item in Diag)
            {
                Console.WriteLine(item.IdWizyty);
                Console.WriteLine(item.Nazwa);
                Console.WriteLine(item.Opis);
              


            }
            this.gridDiag.ItemsSource = Diag.ToList();
        }
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
    }
}
