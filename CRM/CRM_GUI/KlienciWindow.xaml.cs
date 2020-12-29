using CRM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CRM_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy KlienciWindow.xaml
    /// </summary>
    public partial class KlienciWindow : Window
    {

        OrgProwadzacaCRM _orgCRM;

        public KlienciWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
            textblockLiczbaKlientow.Text = _orgCRM.PodajIloscKlientow().ToString();
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            Klient k = new Klient();
            KlientWindow okno = new KlientWindow(k);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _orgCRM.DodajKlienta(k);
                lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                textblockLiczbaKlientow.Text = _orgCRM.PodajIloscKlientow().ToString();
            }

        }

        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstKlienci.SelectedIndex > -1)
            {
                Klient k = (Klient)lstKlienci.SelectedItem;
                _orgCRM.UsunKlienta(k);
                lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                textblockLiczbaKlientow.Text = _orgCRM.PodajIloscKlientow().ToString();
            }
        }

        private void buttonUsunWszystkich_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkich klientów?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _orgCRM.UsunWszystkichKlientow();
                lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                textblockLiczbaKlientow.Text = _orgCRM.PodajIloscKlientow().ToString();
            }
        }

        private void buttonPokazWszystkich_Click(object sender, RoutedEventArgs e)
        {
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
        }

        private void buttonEdytuj_Click(object sender, RoutedEventArgs e) //trzeba dopisać clone do klienta umowy produktu i dopiero bedzie dzialac OK
        {
            if (lstKlienci.SelectedIndex > -1)
            {
                Klient k = (Klient)lstKlienci.SelectedItem;
                Klient klon = (Klient)k.Clone();
                KlientWindow okno = new KlientWindow(klon);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _orgCRM.DodajKlienta(klon);
                    _orgCRM.UsunKlienta(k);
                    lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                }
            }
        }

        private void buttonAktualizuj_Click(object sender, RoutedEventArgs e)
        {
            _orgCRM.AktualizujStatusKlientow();
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
        }

        private void buttonAktualizujDate_Click(object sender, RoutedEventArgs e)
        {
            _orgCRM.AktualizujDatyPlanowanychKontaktow();
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
        }

        private void buttonSortuj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxSortuj.Text;

            switch (opcja)
            {
                case "Alfabetycznie":
                    _orgCRM.KlienciSortujAlfabetycznie();
                    break;

                case "Data planowanego kontaktu":
                    _orgCRM.KlienciSortujDataPlanowanegoKontaktu();
                    break;

                case "Data ostatniego kontaktu":
                    _orgCRM.KlienciSortujDataOstatniegoKontaktu();
                    break;

                default:
                    MessageBox.Show("Sortowanie zakończyło się niepowodzeniem.");
                    break;
            }
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
        }

        private void buttonWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxSzukaj.Text;

            switch (opcja)
            {
                case "Status":
                    txtStatus.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoStatusie.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoStatusie.ItemsSource = Enum.GetValues(typeof(Status));
                    break;

                case "Planowany kontakt":
                    txtDniPlanKontakt.Visibility = System.Windows.Visibility.Visible;
                    txtBoxDniPlanKontakt.Visibility = System.Windows.Visibility.Visible;
                    break;

                case "Ostatni kontakt":
                    txtDniOstatniKontakt.Visibility = System.Windows.Visibility.Visible;
                    txtBoxDniOstatniKontakt.Visibility = System.Windows.Visibility.Visible;
                    break;

                default:
                    MessageBox.Show("Nie udało się wyszukać klientów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void butWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            string opcja = cmbboxSzukaj.Text;
            switch (opcja)
            {
                case "Status":
                    Enum.TryParse<Status>(cmbWyszPoStatusie.SelectedValue.ToString(), out Status s);
                    lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ZnajdzWszystkichKlientowStatus(s));

                    txtStatus.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoStatusie.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoStatusie.Text = String.Empty;
                    break;

                case "Planowany kontakt":
                    int.TryParse(txtBoxDniPlanKontakt.Text, out int p);
                    lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ZnajdzWszystkichKlientowPlanowanyKontakt(p));

                    txtDniPlanKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniPlanKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniPlanKontakt.Text = String.Empty;
                    break;

                case "Ostatni kontakt":
                    int.TryParse(txtBoxDniOstatniKontakt.Text, out int o);
                    lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ZnajdzWszystkichKlientowOstatniKontakt(o));

                    txtDniOstatniKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniOstatniKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniOstatniKontakt.Text = String.Empty;
                    break;

                default:
                    return;
            }
        }

        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            string opcja = cmbboxSzukaj.Text;
            switch (opcja)
            {
                case "Status":
                    txtStatus.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoStatusie.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoStatusie.Text = String.Empty;
                    break;

                case "Planowany kontakt":
                    txtDniPlanKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniPlanKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniPlanKontakt.Text = String.Empty;
                    break;

                case "Ostatni kontakt":
                    txtDniOstatniKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniOstatniKontakt.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxDniOstatniKontakt.Text = String.Empty;
                    break;

                default:
                    return;
            }
        }

        private void buttonSzczegoly_Click(object sender, RoutedEventArgs e)
        {
            if (lstKlienci.SelectedIndex > -1)
            {
                string opcja = cmbboxSzczegoly.Text;

                Klient k = (Klient)lstKlienci.SelectedItem;
                switch (opcja)
                {
                    case "Działania":
                        DzialaniaWindow okno_1 = new DzialaniaWindow(k);
                        okno_1.ShowDialog();
                        lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                        break;

                    case "Osoby do kontaktu":
                        OsobyKontaktoweWindow okno_2 = new OsobyKontaktoweWindow(k);
                        okno_2.ShowDialog();
                        lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                        break;

                    case "Transakcje":

                        break;

                    default:
                        MessageBox.Show("Nie wybrano żadnej opcji.");
                        break;
                }

            }
        }
    }
}
