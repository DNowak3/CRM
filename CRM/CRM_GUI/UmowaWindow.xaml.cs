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
    /// Logika interakcji dla klasy UmowaWindow.xaml
    /// </summary>
    public partial class UmowaWindow : Window
    {
        Umowa _u;
        OrgProwadzacaCRM _orgCRM;
        public UmowaWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
            lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
        }
        public UmowaWindow(Umowa u, OrgProwadzacaCRM orgCRM) : this(orgCRM)
        {
            _u = u;
            if (!_u.NrUmowy.Equals(string.Empty))
            {
                txtNumer.Text = _u.NrUmowy;
                txtData.Text = _u.DataUmowy.ToString("dd-MM-yyyy");
                txtPracownik.Text = _u.PracownikOdp.Imie + " " + _u.PracownikOdp.Nazwisko;
                _u.SlownikDoListy();
                lstKupioneProdukty.ItemsSource = new ObservableCollection<Produkt>(_u.ListaKupionychProduktow);
                lstKupioneIlosci.ItemsSource = new ObservableCollection<double>(_u.IlosciKupionychProduktow);


            }
            else
            {
                txtNumer.Visibility = System.Windows.Visibility.Collapsed;
                labNumer.Visibility = System.Windows.Visibility.Collapsed;
                txtNumer.Text = String.Empty;
                txtData.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtPracownik.Text = String.Empty;

            }
        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtData.Text != "" && txtPracownik.Text != "" && lstKupioneProdukty.Items.Count > 0 && lstKupioneIlosci.Items.Count > 0)
            {
                try
                {
                    DateTime.TryParseExact(txtData.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d);
                    if (d <= DateTime.Today)
                    {
                        _u.DataUmowy = d;
                    }
                    else
                    {
                        _u.DataUmowy = DateTime.Today;
                    }

                }
                catch (ArgumentException)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie umowy nie powiodło się. Niepoprawna data. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                    }
                    else
                    {
                        return;
                    }
                }
                foreach (Pracownik p in _orgCRM.ListaPracownikow)
                {
                    string temp = $"{p.Imie} {p.Nazwisko}";
                    if (txtPracownik.Text.Equals(temp))
                    {
                        _u.PracownikOdp = p;
                    }
                }
                if (_u.PracownikOdp == null)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie umowy nie powiodło się. Niepoprawne dane pracownika. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                    }
                    else
                    {
                        return;
                    }
                }
                if (_u.NrUmowy.Equals(string.Empty))
                {
                    _u.NrUmowy = $"U/{_u.DataUmowy.ToShortDateString()}/{Umowa.AktualnyNumer}";
                }

                DialogResult = true;
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("Dodawanie umowy nie powiodło się. Żadne z pól nie powinno pozostać puste. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (m == MessageBoxResult.Yes)
                {
                    DialogResult = false;
                }
                else
                {
                    return;
                }
            }

        }
        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            InputBox1.Visibility = System.Windows.Visibility.Visible;
            txtIloscNowego.Text = string.Empty;
        }
        private void butAnuluj2_Click(object sender, RoutedEventArgs e)
        {
            InputBox1.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void butDodaj1_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedIndex > -1)
            {
                Produkt p = (Produkt)lstProdukty.SelectedItem;
                if (!txtIloscNowego.Text.Equals(string.Empty))
                {
                    double ile = Convert.ToDouble(txtIloscNowego.Text);
                    _u.DodajProdukt(p, ile);
                }
                else
                {
                    _u.DodajProdukt(p);
                }
                _u.SlownikDoListy();
                lstKupioneProdukty.ItemsSource = new ObservableCollection<Produkt>(_u.ListaKupionychProduktow);
                lstKupioneIlosci.ItemsSource = new ObservableCollection<double>(_u.IlosciKupionychProduktow);
            }
            else
            {
                MessageBox.Show("Nie zaznaczono żadnego produktu", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InputBox1.Visibility = System.Windows.Visibility.Collapsed;

        }
        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            if (lstKupioneProdukty.SelectedIndex > -1)
            {
                Produkt p = (Produkt)lstKupioneProdukty.SelectedItem;
                _u.UsunProdukt(p);
                _u.SlownikDoListy();
                lstKupioneProdukty.ItemsSource = new ObservableCollection<Produkt>(_u.ListaKupionychProduktow);
                lstKupioneIlosci.ItemsSource = new ObservableCollection<double>(_u.IlosciKupionychProduktow);
            }

        }
        private void buttonZmien_Click(object sender, RoutedEventArgs e)
        {
            if (lstKupioneProdukty.SelectedIndex > -1)
            {
                InputBox2.Visibility = System.Windows.Visibility.Visible;
                txtNowaIlosc.Text = string.Empty;
            }
        }
        private void butAnuluj3_Click(object sender, RoutedEventArgs e)
        {
            InputBox2.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void butZmienIlosc_Click(object sender, RoutedEventArgs e)
        {
            Produkt p = (Produkt)lstKupioneProdukty.SelectedItem;
            if (!txtNowaIlosc.Text.Equals(string.Empty))
            {
                double ile = Convert.ToDouble(txtNowaIlosc.Text);
                _u.ZmienIlosc(p, ile);
            }
            _u.SlownikDoListy();
            lstKupioneProdukty.ItemsSource = new ObservableCollection<Produkt>(_u.ListaKupionychProduktow);
            lstKupioneIlosci.ItemsSource = new ObservableCollection<double>(_u.IlosciKupionychProduktow);
            InputBox2.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void buttonZmienP_Click(object sender, RoutedEventArgs e)
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
                _u.PracownikOdp = (Pracownik)lstPracownicy.SelectedItem;
                txtPracownik.Text = _u.PracownikOdp.Imie + " " + _u.PracownikOdp.Nazwisko;
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

    }
}
