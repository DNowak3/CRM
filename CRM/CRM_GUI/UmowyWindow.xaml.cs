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
    /// Logika interakcji dla klasy UmowyWindow.xaml
    /// </summary>
    public partial class UmowyWindow : Window
    {
        Klient _k;
        OrgProwadzacaCRM _orgCRM;
        public UmowyWindow(Klient k, OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _k = k;
            _orgCRM = orgCRM;
            txtNazwaKlienta.Text = _k.Nazwa.ToUpper();
            lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
            txtLiczbaUmow.Text = _k.TransakcjeList.Count.ToString();
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);

        }

        private void buttonDodajUmowe_Click(object sender, RoutedEventArgs e)
        {
            Umowa u = new Umowa();
            UmowaWindow okno = new UmowaWindow(u, _orgCRM);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _k.DodajTransakcje(u);
                _k.StosDoListy();
                lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
                txtLiczbaUmow.Text = _k.TransakcjeList.Count.ToString();
            }
        }
        private void buttonUsunUmowe_Click(object sender, RoutedEventArgs e)
        {
            if (lstUmowy.SelectedIndex > -1)
            {
                Umowa u = (Umowa)lstUmowy.SelectedItem;
                _k.UsunTransakcje(u);
                _k.StosDoListy();
                lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
                txtLiczbaUmow.Text = _k.TransakcjeList.Count.ToString();
            }
        }
        private void buttonEdytujUmowe_Click(object sender, RoutedEventArgs e)
        {
            if (lstUmowy.SelectedIndex > -1)
            {
                Umowa u = (Umowa)lstUmowy.SelectedItem;
                Umowa u1 = (Umowa)u.Clone();
                UmowaWindow okno = new UmowaWindow(u1, _orgCRM);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _k.DodajTransakcje(u1);
                    _k.UsunTransakcje(u);
                    _k.StosDoListy();
                    lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
                }
            }
        }
        private void buttonUsunWszystkie_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie transakcje z tym klientem?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _k.TransakcjeList.Clear();
                lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
                txtLiczbaUmow.Text = _k.TransakcjeList.Count.ToString();
            }
        }
        private void buttonPokazWszystkie_Click(object sender, RoutedEventArgs e)
        {
            lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.TransakcjeList);
            txtLiczbaUmow.Text = _k.TransakcjeList.Count.ToString();
        }

        private void buttonSzukajNumer_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
            txtNumerSzukany.Text = string.Empty;
        }

        private void buttonSzukajData_Click(object sender, RoutedEventArgs e)
        {
            InputBox1.Visibility = System.Windows.Visibility.Visible;
            txtDataSzukana.Text = string.Empty;
        }

        private void buttonSzukajPracownik_Click(object sender, RoutedEventArgs e)
        {
            InputBox2.Visibility = System.Windows.Visibility.Visible;
            lstPracownicy.SelectedIndex = 0;
        }

        private void butSzukaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numer = txtNumerSzukany.Text;
                lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.ZwrocTransakcje(numer));
                txtLiczbaUmow.Text = (_k.ZwrocTransakcje(numer).Count().ToString());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Nie udało się znaleźć umowy o podanym numerze.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void butSzukaj1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string data = txtDataSzukana.Text;
                lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.ZnajdzTransakcje(data));
                txtLiczbaUmow.Text = (_k.ZnajdzTransakcje(data).Count().ToString());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Nie udało się znaleźć umowy o podanej dacie.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InputBox1.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void butAnuluj1_Click(object sender, RoutedEventArgs e)
        {
            InputBox1.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void butSzukaj2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstPracownicy.SelectedIndex > -1)
                {
                    Pracownik p = (Pracownik)lstPracownicy.SelectedItem;
                    lstUmowy.ItemsSource = new ObservableCollection<Umowa>(_k.ZwrocTransakcjePracownik(p));
                    txtLiczbaUmow.Text = (_k.ZwrocTransakcjePracownik(p).Count().ToString());
                    InputBox2.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    MessageBoxResult message = MessageBox.Show("Nie wybrano pracownika. Czy na pewno chcesz kontynuować?", "Uwaga!", MessageBoxButton.YesNo);
                    if (message == MessageBoxResult.Yes)
                    {
                        InputBox2.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Nie udało się znaleźć umowy zawartej przez podanego pracownika.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                InputBox2.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void butAnuluj2_Click(object sender, RoutedEventArgs e)
        {
            InputBox2.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
