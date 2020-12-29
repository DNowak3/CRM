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
    /// Logika interakcji dla klasy DzialaniaWindow.xaml
    /// </summary>
    public partial class DzialaniaWindow : Window
    {
        Klient _k;

        public DzialaniaWindow(Klient klient)
        {
            InitializeComponent();
            _k = klient;
            textblockNazwaFirmy.Text = _k.Nazwa.ToUpper();
            lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
            textblockIleDzialan.Text = _k.IleDzialan().ToString();
        }

        private void buttonDodajDzialanie_Click(object sender, RoutedEventArgs e)
        {
            Dzialanie d = new Dzialanie();
            DzialanieWindow okno = new DzialanieWindow(d);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _k.DodajDzialanie(d);
                _k.StosDoListy();
                lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
                textblockIleDzialan.Text = _k.IleDzialan().ToString();
            }
        }

        private void buttonEdytujDzialanie_Click(object sender, RoutedEventArgs e)
        {
            if (lstDzialania.SelectedIndex > -1)
            {
                Dzialanie d = (Dzialanie)lstDzialania.SelectedItem;
                Dzialanie klon = (Dzialanie)d.Clone();
                DzialanieWindow okno = new DzialanieWindow(klon);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _k.DodajDzialanie(klon);
                    _k.UsunDzialanie(d);
                    _k.StosDoListy();
                    lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
                }
            }
        }

        private void buttonUsunDzialanie_Click(object sender, RoutedEventArgs e)
        {
            if (lstDzialania.SelectedIndex > -1)
            {
                Dzialanie d = (Dzialanie)lstDzialania.SelectedItem;
                _k.UsunDzialanie(d);
                _k.StosDoListy();
                lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
                textblockIleDzialan.Text = _k.IleDzialan().ToString();
            }
        }

        private void buttonWszUsunDzialania_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie działania?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _k.UsunWszystkieDzialania();
                _k.StosDoListy();
                lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
                textblockIleDzialan.Text = _k.IleDzialan().ToString();
            }
        }

        private void buttonSortuj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxSortuj.Text;

            switch (opcja)
            {
                case "Data: od najnowszych":
                    _k.SortujDzialania();
                    break;

                case "Data: od najstarszych":
                    _k.SortujDzialania(false);
                    break;

                default:
                    MessageBox.Show("Sortowanie zakończyło się niepowodzeniem.");
                    break;
            }
            _k.StosDoListy();
            lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
        }

        private void buttonWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxWyszukaj.Text;

            switch (opcja)
            {
                case "Przyszłe działania":
                    lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.ZnajdzPlanowaneDzialania());
                    break;

                case "Działania od podanej daty":
                    txtBoxData.Visibility = System.Windows.Visibility.Visible;
                    txtData.Visibility = System.Windows.Visibility.Visible;
                    InputBox.Visibility = System.Windows.Visibility.Visible;
                    break;

                default:
                    MessageBox.Show("Nie udało się wyszukać działań.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
        }
        private void butWyszukaj2_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.ZnajdzDzialania(txtBoxData.Text));
            txtData.Visibility = System.Windows.Visibility.Collapsed;
            txtBoxData.Visibility = System.Windows.Visibility.Collapsed;
            txtBoxData.Text = String.Empty;
        }
        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            txtData.Visibility = System.Windows.Visibility.Collapsed;
            txtBoxData.Visibility = System.Windows.Visibility.Collapsed;
            txtBoxData.Text = String.Empty;
        }

        private void buttonPokazWsz_Click(object sender, RoutedEventArgs e)
        {
            _k.StosDoListy();
            lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
        }
    }
}
