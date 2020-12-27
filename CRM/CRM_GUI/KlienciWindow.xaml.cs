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
            _orgCRM.AktualizujStatusKlientow();
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
            if(lstKlienci.SelectedIndex > -1)
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
                    textblockLiczbaKlientow.Text = _orgCRM.PodajIloscKlientow().ToString();
                    lstKlienci.ItemsSource = new ObservableCollection<Klient>(_orgCRM.ListaKlientow);
                }
            }
        }

        private void buttonAktualizuj_Click(object sender, RoutedEventArgs e)
        {
            _orgCRM.AktualizujStatusKlientow();
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
                        bool? ret_1 = okno_1.ShowDialog();
                        if (ret_1 == true)
                        {
                        }
                        break;

                    case "Osoby do kontaktu":
                        OsobyKontaktoweWindow okno_2 = new OsobyKontaktoweWindow(k);
                        bool? ret_2 = okno_2.ShowDialog();
                        if (ret_2 == true)
                        {
                        }
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
