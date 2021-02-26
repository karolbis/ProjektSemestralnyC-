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
            // Test consolowy z polaczeniem z baza 
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
        //Zamykanie aktualnego okna i przejscie do menu glownego
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
    
        //Aktualizowanie danych
        private void BtnUpdate3_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            this.gridWizyty.ItemsSource = db.Wizytas.ToList();

        }
        //Dodawanie nowegej wizyty do bazy
        private void BtnAdd3_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            Wizyta wizytaObject = new Wizyta()
            {
                DoktorID = txtDoktorW.SelectionStart,
                PacjentID = txtPacjentW.SelectionStart,
                Data_Wizyty = DataW.DisplayDate,
                Gabinet = txtGabinetW.Text,
                Pietro = txtPietroW.Text
            };

            db.Wizytas.Add(wizytaObject);
            db.SaveChanges();

        }
        //Zmiana danych wybranego elementu w bazie danych poprzez użycie wypełnienie textBoxow 
        //dawnymi danymi i zmiane wybranego pola
        private void ZmienDane_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            var r = from w in db.Wizytas
                    where w.Id == this.updatingWizytaId
                    select w;

            Wizyta obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.DoktorID = this.txtDoktorW2.CaretIndex;
                obj.PacjentID = this.txtPacjentW2.CaretIndex;
                obj.Data_Wizyty = this.DataW2.DisplayDate;
                obj.Gabinet = this.txtGabinetW2.Text;
                obj.Pietro = this.txtPietroW2.Text;

            }

            db.SaveChanges();
        }
        //Przycisk do usuniecia calego rekordu z bazy, jezeli np wizyta zostala anulowana
        private void UsunWizyte_Click(object sender, RoutedEventArgs e)
        {
            {
                {
                    MessageBoxResult msgBoxResult = MessageBox.Show("Czy napewno chcesz usunac dana wizyte?",
                "Usun Wizyte",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

                    Database1Entities db = new Database1Entities();

                    var r = from w in db.Wizytas
                            where w.Id == this.updatingWizytaId
                            select w;

                    Wizyta obj = r.SingleOrDefault();

                    if (obj != null)
                    {

                        db.Wizytas.Remove(obj);
                        db.SaveChanges();

                    }

                }
            }
        }

        //Zeby dane sie zmienialy musimy pobierac ID danej wizyty
        private int updatingWizytaId = 0;


        //Odpowiada za zaznaczenie wybranego rekordu w bazie i przekopiowanie danych
        private void gridWizyty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridWizyty.SelectedIndex >= 0)
            {
                if (this.gridWizyty.SelectedItems.Count >= 0)
                {
                    if (this.gridWizyty.SelectedItems[0].GetType() == typeof(Wizyta))
                    {
                        Wizyta w = (Wizyta)this.gridWizyty.SelectedItems[0];                       
                        this.txtDoktorW2.Text = w.DoktorID.ToString();
                        this.txtPacjentW2.Text = w.PacjentID.ToString();
                        this.DataW2.SelectedDate = w.Data_Wizyty;
                        this.txtGabinetW2.Text = w.Gabinet;
                        this.txtPietroW2.Text = w.Pietro;
                        this.updatingWizytaId = w.Id;

                    }
                }
            }
        }

        
    }
}
