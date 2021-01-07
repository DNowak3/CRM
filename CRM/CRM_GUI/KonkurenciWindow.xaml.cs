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
using CRM;
using static CRM.Konkurent;

namespace CRM_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy KonkurenciWindow.xaml
    /// </summary>
    public partial class KonkurenciWindow : Window
    {
        OrgProwadzacaCRM _orgCRM;
        public KonkurenciWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
            textblockLiczbaKonkurentow.Text = _orgCRM.PodajIloscKonkurentow().ToString();
            comboBoxSortuj.SelectedIndex = 0;
            comboBoxWyszukaj.SelectedIndex = 1;
            cmbWyszPoZagrozeniu.SelectedIndex = 3;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Konkurent k = new Konkurent();
            KonkurentWindow okno = new KonkurentWindow(k);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _orgCRM.DodajKonkurenta(k);
                lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
                textblockLiczbaKonkurentow.Text = _orgCRM.PodajIloscKonkurentow().ToString();
            }
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstKonkurenci.SelectedIndex > -1)
            {
                Konkurent k = (Konkurent)lstKonkurenci.SelectedItem;
                _orgCRM.UsunKonkurenta(k);
                lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
                textblockLiczbaKonkurentow.Text = _orgCRM.PodajIloscKonkurentow().ToString();
            }
        }

        private void btnWyczysc_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkich konkurentów?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _orgCRM.UsunWszystkichKonkurentow();
                lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
                textblockLiczbaKonkurentow.Text = _orgCRM.PodajIloscKonkurentow().ToString();
            }
        }

        private void btnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if (lstKonkurenci.SelectedIndex > -1)
            {
                Konkurent k = (Konkurent)lstKonkurenci.SelectedItem;
                Konkurent kopia = (Konkurent)k.Clone();
                KonkurentWindow okno = new KonkurentWindow(kopia);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _orgCRM.DodajKonkurenta(kopia);
                    _orgCRM.UsunKonkurenta(k);
                    textblockLiczbaKonkurentow.Text = _orgCRM.PodajIloscKonkurentow().ToString();
                    lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
                }
            }
        }

        private void btnSortuj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = comboBoxSortuj.Text;

            switch (opcja)
            {
                case "Sortuj po nazwie":
                    _orgCRM.KonkurenciSortujAlfabetycznie();
                    break;
                case "Sortuj po dacie założenia":
                    _orgCRM.KonkurenciSortujDataZalozenia();
                    break;
                default:
                    MessageBox.Show("Sortowanie konkurentów zakończyło się niepowodzeniem.");
                    break;
            }
            lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ListaKonkurentow);
        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = comboBoxWyszukaj.Text;

            switch (opcja)
            {
                case "Wyszukaj po stopniu zagrożenia":
                    txtZagrozenie.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoZagrozeniu.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoZagrozeniu.ItemsSource = Enum.GetValues(typeof(StopienZagrozenia));
                    break;

                case "Wyszukaj po kraju":
                    txtKraj.Visibility = System.Windows.Visibility.Visible;
                    txtBoxKraj.Visibility = System.Windows.Visibility.Visible;
                    break;

                default:
                    MessageBox.Show("Nie udało się wyszukać konkurentów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void butWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            string opcja = comboBoxWyszukaj.Text;
            switch (opcja)
            {
                case "Wyszukaj po stopniu zagrożenia":
                    try
                    {
                        Enum.TryParse<StopienZagrozenia>(cmbWyszPoZagrozeniu.SelectedValue.ToString(), out StopienZagrozenia z);
                        lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ZnajdzWszystkichKonkurentowZagrozenie(z));
                        textblockLiczbaKonkurentow.Text = _orgCRM.ZnajdzWszystkichKonkurentowZagrozenie(z).Count().ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się znaleźć klientów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    txtZagrozenie.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoZagrozeniu.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoZagrozeniu.Text = String.Empty;
                    break;

                case "Wyszukaj po kraju":
                    try
                    {
                        string kraj = txtBoxKraj.Text;
                        lstKonkurenci.ItemsSource = new ObservableCollection<Konkurent>(_orgCRM.ZnajdzWszystkichKonkurentowKraj(kraj));
                        textblockLiczbaKonkurentow.Text = _orgCRM.ZnajdzWszystkichKonkurentowKraj(kraj).Count().ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się znaleźć klientów. Upewnij się, że podałeś prawdiłową wartość.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    txtKraj.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKraj.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKraj.Text = String.Empty;
                    break;

                default:
                    MessageBox.Show("Nie udało się wyszukać konkurentów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
        }

        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            string opcja = comboBoxWyszukaj.Text;
            switch (opcja)
            {
                case "Wyszukaj po zagrożeniu":
                    txtZagrozenie.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoZagrozeniu.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoZagrozeniu.Text = String.Empty;
                    break;

                case "Wyszukaj po kraju":
                    txtKraj.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKraj.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKraj.Text = String.Empty;
                    break;

                default:
                    return;
            }
        }
    }
}