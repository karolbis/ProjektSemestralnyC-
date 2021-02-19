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
    /// Logika interakcji dla klasy Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();

            Database1Entities db = new Database1Entities();
            var wiz = from w in db.Wizytas
                       select new
                       {
                           ID = w.Id,
                           IdDoktora = w.DoktorID,
                           IdPacjenta = w.PacjentID,
                           DataWizyty = w.Data_Wizyty,
                           Gabinet = w.Gabinet,
                           Pietro = w.Pietro
                           


                       };
            foreach (var item in wiz)
            {
                Console.WriteLine(item.ID);
                Console.WriteLine(item.IdDoktora);
                Console.WriteLine(item.IdPacjenta);
                Console.WriteLine(item.DataWizyty);
                Console.WriteLine(item.Gabinet);
                Console.WriteLine(item.Pietro);


            }
            this.gridWizyty.ItemsSource = wiz.ToList();
        }
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
    }
}
