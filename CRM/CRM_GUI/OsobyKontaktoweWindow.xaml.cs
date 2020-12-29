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
    /// Logika interakcji dla klasy OsobyKontaktoweWindow.xaml
    /// </summary>
    public partial class OsobyKontaktoweWindow : Window
    {
        Klient _k;

        public OsobyKontaktoweWindow(Klient klient)
        {
            InitializeComponent();
            _k = klient;
            textblockNazwaFirmy.Text = _k.Nazwa.ToUpper();
            lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
            textblockIleKontaktow.Text = _k.IleKontaktow().ToString();
        }

        private void buttonDodajKontakt_Click(object sender, RoutedEventArgs e)
        {
            OsobaKontakt o = new OsobaKontakt();
            OsobaKontaktowaWindow okno = new OsobaKontaktowaWindow(o);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _k.DodajKontakt(o);
                lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
                textblockIleKontaktow.Text = _k.IleKontaktow().ToString();
            }
        }

        private void buttonEdytujKontakt_Click(object sender, RoutedEventArgs e)
        {
            if (lstKontakty.SelectedIndex > -1)
            {
                OsobaKontakt o = (OsobaKontakt)lstKontakty.SelectedItem;
                OsobaKontakt klon = (OsobaKontakt)o.Clone();
                OsobaKontaktowaWindow okno = new OsobaKontaktowaWindow(klon);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _k.DodajKontakt(klon);
                    _k.UsunKontakt(o);
                    lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
                }
            }
        }

        private void buttonUsunKontakt_Click(object sender, RoutedEventArgs e)
        {
            if (lstKontakty.SelectedIndex > -1)
            {
                OsobaKontakt o = (OsobaKontakt)lstKontakty.SelectedItem;
                _k.UsunKontakt(o);
                lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
                textblockIleKontaktow.Text = _k.IleKontaktow().ToString();
            }
        }

        private void buttonUsunWszKontakty_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie pozycje z listy kontaktów?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _k.UsunWszystkieKontakty();
                lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
                textblockIleKontaktow.Text = _k.IleKontaktow().ToString();
            }
        }

        private void buttonSortuj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxSortuj.Text;

            switch (opcja)
            {
                case "Alfabetycznie: od A do Z":
                    _k.SortujKontakty();
                    break;

                case "Alfabetycznie: od Z do A":
                    _k.SortujKontakty(true);
                    break;

                default:
                    MessageBox.Show("Sortowanie zakończyło się niepowodzeniem.");
                    break;
            }
            lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
        }
    }
}
