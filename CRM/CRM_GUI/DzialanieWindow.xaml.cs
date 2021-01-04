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
    /// Logika interakcji dla klasy DzialanieWindow.xaml
    /// </summary>
    public partial class DzialanieWindow : Window
    {
        Dzialanie _d;
        OrgProwadzacaCRM _orgCRM;
        Klient _k;
        public DzialanieWindow()
        {
            _d = new Dzialanie();
            _orgCRM = new OrgProwadzacaCRM();
            InitializeComponent();
            cmbokWynik.ItemsSource = Enum.GetValues(typeof(WynikDzialania));
        }

        public DzialanieWindow(Dzialanie d, OrgProwadzacaCRM org, Klient k) : this()
        {
            _d = d;
            txtData.Text = _d.Data.ToString("dd-MM-yyyy");
            txtNazwa.Text = _d.Nazwa;
            cmbokWynik.SelectedItem = _d.Wynik;
            txtOpis.Text = _d.Opis;
            _orgCRM = org;
            _k = k;
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
            lstOsobyKontaktowe.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
            if (_d.Pracownik != null)
            {
                txtPracownik.Text = _d.Pracownik.Imie + " " + _d.Pracownik.Nazwisko;
            }
            else
            {
                txtPracownik.Text = String.Empty;
            }
            if (_d.OsobaKontaktowa != null)
            {
                txtKontakt.Text = _d.OsobaKontaktowa.Imie + " " + _d.OsobaKontaktowa.Nazwisko;
            }
            else
            {
                txtKontakt.Text = String.Empty;
            }
        }

        private void buttonZmienPracownika_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
            lstPracownicy.Visibility = System.Windows.Visibility.Visible;
            txtPolecenie.Visibility = System.Windows.Visibility.Visible;

        }

        private void butAnuluj1_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstPracownicy.Visibility = System.Windows.Visibility.Collapsed;
            txtPolecenie.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void butWybierz_Click(object sender, RoutedEventArgs e)
        {
            if (lstPracownicy.SelectedIndex > -1)
            {
                _d.Pracownik = (Pracownik)lstPracownicy.SelectedItem;
                txtPracownik.Text = _d.Pracownik.Imie + " " + _d.Pracownik.Nazwisko;
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                lstPracownicy.Visibility = System.Windows.Visibility.Collapsed;
                txtPolecenie.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Nie zaznaczono żadnego pracownika", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                lstPracownicy.Visibility = System.Windows.Visibility.Collapsed;
                txtPolecenie.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void buttonZmienOsobe_Click(object sender, RoutedEventArgs e)
        {
            InputBox_Kontakt.Visibility = System.Windows.Visibility.Visible;
            lstOsobyKontaktowe.Visibility = System.Windows.Visibility.Visible;
            txtPolecenie_2.Visibility = System.Windows.Visibility.Visible;

        }

        private void butAnulujKontakt_Click(object sender, RoutedEventArgs e)
        {
            InputBox_Kontakt.Visibility = System.Windows.Visibility.Collapsed;
            lstOsobyKontaktowe.Visibility = System.Windows.Visibility.Collapsed;
            txtPolecenie_2.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void butWybierzKontakt_Click(object sender, RoutedEventArgs e)
        {
            if (lstOsobyKontaktowe.SelectedIndex > -1)
            {
                _d.OsobaKontaktowa = (OsobaKontakt)lstOsobyKontaktowe.SelectedItem;
                txtKontakt.Text = _d.OsobaKontaktowa.Imie + " " + _d.OsobaKontaktowa.Nazwisko;
                InputBox_Kontakt.Visibility = System.Windows.Visibility.Collapsed;
                lstOsobyKontaktowe.Visibility = System.Windows.Visibility.Collapsed;
                txtPolecenie_2.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Nie zaznaczono żadnej osoby kontaktowej", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                InputBox_Kontakt.Visibility = System.Windows.Visibility.Collapsed;
                lstOsobyKontaktowe.Visibility = System.Windows.Visibility.Collapsed;
                txtPolecenie_2.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && txtData.Text != "")
            {
                _d.Nazwa = txtNazwa.Text;
                _d.Opis = txtOpis.Text;
                Enum.TryParse<WynikDzialania>(cmbokWynik.SelectedValue.ToString(), out WynikDzialania w);
                _d.Wynik = w;
                try
                {
                    string[] formats = new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
                    _d.Data = DateTime.ParseExact(txtData.Text, formats, null, System.Globalization.DateTimeStyles.None);
                }
                catch (Exception)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie dzialania nie powiodło się. Niepoprawna data. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Dodawanie działania nie powiodło się. Każde działanie musi posiadać nazwę oraz datę.", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = false;
                return;
            }
            DialogResult = true;
        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
