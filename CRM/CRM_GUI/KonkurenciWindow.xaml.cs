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
    }
}
