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
            // Test consolowy z polaczeniem z baza 
            foreach (var item in Diag)
            {
                Console.WriteLine(item.IdWizyty);
                Console.WriteLine(item.Nazwa);
                Console.WriteLine(item.Opis);
              


            }
            this.gridDiag.ItemsSource = Diag.ToList();
        }
        //Zamkniecie aktualnego okna i przejscie do glownego
        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            MainWindow sW = new MainWindow();
            sW.Show();
            this.Close();
        }
        //Aktualizacja danych w bazie
        private void BtnUpdate4_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();
            this.gridDiag.ItemsSource = db.Diagnozas.ToList();
        }

        //Dodanie nowej diagnozy
        private void BtnAdd4_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            Diagnoza DiagnozaObject = new Diagnoza()
            {
                WizytaID = txtWizytaIdW.SelectionStart,
                Nazwa = txtNazwaW.Text,
                Opis = txtOpisW.Text,
                
            };

            db.Diagnozas.Add(DiagnozaObject);
            db.SaveChanges();
        }
        //Przycisk odpowiedzialny za usuniecie rekordu oraz alert box 
        private void BtnUsun4_Click(object sender, RoutedEventArgs e)
        {
            {
                {
                    MessageBoxResult msgBoxResult = MessageBox.Show("Czy napewno chcesz usunac dana diagnoze?",
              "Usun Diagnoze",
              MessageBoxButton.YesNo,
              MessageBoxImage.Warning,
              MessageBoxResult.No
              );
                    Database1Entities db = new Database1Entities();

                    var r = from di in db.Diagnozas
                            where di.ID == this.updatingDiagnozaId
                            select di;

                    Diagnoza obj = r.SingleOrDefault();

                    if (obj != null)
                    {

                        db.Diagnozas.Remove(obj);
                        db.SaveChanges();

                    }

                }
            }
        }
        //Zmiana danych wybranego elementu w bazie danych poprzez użycie wypełnienie textBoxow 
        //dawnymi danymi i zmiane wybranego pola
        private void BtnZmienDane4_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities db = new Database1Entities();

            var r = from di in db.Diagnozas
                    where di.ID == this.updatingDiagnozaId
                    select di;

            Diagnoza obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.WizytaID = this.txtWizytaIdW_Copy.CaretIndex;
                obj.Nazwa = this.txtNazwaW_Copy.Text;
                obj.Opis = this.txtOpisW_Copy.Text;
              

            }

            db.SaveChanges();
        }
        private int updatingDiagnozaId = 0;

        //Odpowiada za zaznaczenie wybranego rekordu w bazie i przekopiowanie danych
        private void gridDiag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        
            {
                if (this.gridDiag.SelectedIndex >= 0)
                {
                    if (this.gridDiag.SelectedItems.Count >= 0)
                    {
                        if (this.gridDiag.SelectedItems[0].GetType() == typeof(Diagnoza))
                        {
                            Diagnoza di = (Diagnoza)this.gridDiag.SelectedItems[0];
                            this.txtWizytaIdW_Copy.Text = di.WizytaID.ToString();
                            this.txtNazwaW_Copy.Text = di.Nazwa;
                            this.txtOpisW_Copy.Text = di.Opis;                          
                            this.updatingDiagnozaId = di.ID;

                        }
                    }
                }
            }
        
    }
}
