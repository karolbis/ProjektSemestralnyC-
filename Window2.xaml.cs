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
            // Test konsolowy z polaczeniem z baza 
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
        //Zamyka akutalne okno i przechodzi do głowngo menu
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
        //Odswieżanie danych w bazie
        private void BtnUpdate2_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            this.gridPacjenci.ItemsSource = db.Pacjents.ToList();
        }
        //Dodawanie nowego pacjenta do bazy
        private void BtnAdd2_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            Pacjent pacjentObject = new Pacjent()
            {
                Imie = txtImieP.Text,
                Nazwisko = txtNazwiskoP.Text,
                Wiek = txtWiekP.Text,
                Pesel = txtPeselP.Text,
                Choroba = txtChorobaP.Text
            };

            db.Pacjents.Add(pacjentObject);
            db.SaveChanges();

        }
        //Zmiana danych wybranego elementu w bazie danych poprzez użycie wypełnienie textBoxow 
        //dawnymi danymi i zmiane wybranego pola
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            var r = from p in db.Pacjents
                    where p.Id == this.updatingPacjentId
                    select p;

            Pacjent obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Imie = this.txtImieP2.Text;
                obj.Nazwisko = this.txtNazwiskoP2.Text;
                obj.Wiek = this.txtWiekP2.Text;
                obj.Pesel = this.txtPeselP2.Text;
                obj.Choroba = this.txtChorobaP2.Text;

            }

            db.SaveChanges();
        }
        //Przycisk do usuniecia calego rekordu z bazy, jezeli np dany pacjent zostal wypisany

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            {
                MessageBoxResult msgBoxResult = MessageBox.Show("Czy napewno chcesz usunac danego pacjenta?",
                "Usun Pacjenta",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

                Database1Entities db = new Database1Entities();

                var r = from p in db.Pacjents
                        where p.Id == this.updatingPacjentId
                        select p;

                Pacjent obj = r.SingleOrDefault();

                if (obj != null)
                {

                    db.Pacjents.Remove(obj);
                    db.SaveChanges();

                }

            }
        }
        //Zeby dane sie zmienialy musimy pobierac ID danego pacjenta
        private int updatingPacjentId = 0;


        //Odpowiada za zaznaczenie wybranego rekordu w bazie i przekopiowanie danych
        private void gridPacjenci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridPacjenci.SelectedIndex >= 0)
            {
                if (this.gridPacjenci.SelectedItems.Count >= 0)
                {
                    if (this.gridPacjenci.SelectedItems[0].GetType() == typeof(Pacjent))
                    {
                        Pacjent p = (Pacjent)this.gridPacjenci.SelectedItems[0];
                        this.txtImieP2.Text = p.Imie;
                        this.txtNazwiskoP2.Text = p.Nazwisko;
                        this.txtWiekP2.Text = p.Wiek;
                        this.txtPeselP2.Text = p.Pesel;
                        this.txtChorobaP2.Text = p.Choroba;
                        this.updatingPacjentId = p.Id;

                    }
                }
            }
        }
    }
}
