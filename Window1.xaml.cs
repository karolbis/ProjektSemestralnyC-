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
            //Testowałem tutaj czy dane z bazy danych pojawia sie w konsoli
            foreach (var item in docs)
            {
                Console.WriteLine(item.ImieDoktora);
                Console.WriteLine(item.NazwiskoDoktora);
                Console.WriteLine(item.Specjalizacja);
                Console.WriteLine(item.Odzial);

            }
            this.gridDoctors.ItemsSource = docs.ToList();
        }

        //Zamyka akutalne okno i przechodzi do głowngo menu
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
        //Dodawanie nowego doktora do bazy
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            Doktor doctorObject = new Doktor()
            {
                Imie = txtImie.Text,
                Nazwisko = txtNazwisko.Text,
                Specjalizacja = txtSpecjalizacja.Text,
                Odzial = txtOdzial.Text
            };

            db.Doktors.Add(doctorObject);
            db.SaveChanges();

        }

        //Odswieżanie danych w bazie
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            this.gridDoctors.ItemsSource = db.Doktors.ToList();


        }

        //Zeby dane sie zmienialy musimy pobierac ID danego doktora
        private int updatingDoctorID = 0;

        //Odpowiada za zaznaczenie wybranego rekordu w bazie i przekopiowanie danych
        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Doktor))
                    {
                        Doktor d = (Doktor)this.gridDoctors.SelectedItems[0];
                        this.txtImie2.Text = d.Imie;
                        this.txtNazwisko2.Text = d.Nazwisko;
                        this.txtSpecjalizacja2.Text = d.Specjalizacja;
                        this.txtOdzial2.Text = d.Odzial;
                        this.updatingDoctorID = d.ID;
                    }
                }
            }
        }

         //Zmiana danych wybranego elementu w bazie danych poprzez użycie wypełnienie textBoxow 
         //dawnymi danymi i zmiane wybranego pola
        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            var r = from d in db.Doktors
                    where d.ID == this.updatingDoctorID
                    select d;

            Doktor obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Imie = this.txtImie2.Text;
                obj.Nazwisko = this.txtNazwisko2.Text;
                obj.Specjalizacja = this.txtSpecjalizacja2.Text;
                obj.Odzial = this.txtOdzial2.Text;
            }

            db.SaveChanges();
        }
        //Przycisk do usuniecia calego rekordu z bazy, jezeli np dany doktor sie zwolnil
        //Oraz Alert czy napewno chcemy usunac dany element
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy napewno chcesz usunac danego doktora?",
                "Usun Doktora",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

            Database1Entities db = new Database1Entities();

            var r = from d in db.Doktors
                    where d.ID == this.updatingDoctorID
                    select d;

            Doktor obj = r.SingleOrDefault();
           
            if (obj != null)
            {

                db.Doktors.Remove(obj);
                db.SaveChanges();

            }

        }
    }
    
}
