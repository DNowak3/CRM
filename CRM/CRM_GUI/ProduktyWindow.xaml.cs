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
    /// Logika interakcji dla klasy ProduktyWindow.xaml
    /// </summary>
    public partial class ProduktyWindow : Window
    {
        OrgProwadzacaCRM _orgCRM;
        public ProduktyWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            textblockNazwaFirmy.Text = _orgCRM.Nazwa.ToUpper();
            lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
            textblockLiczbaProduktow.Text = _orgCRM.PodajIloscProduktow().ToString();
            //cmbboxSortuj.SelectedIndex = 0;
            
        }
        private void buttonSortuj_Click(object sender, RoutedEventArgs e)
        {
            string opcja = cmbboxSortuj.Text;

            switch (opcja)
            {
                case "Kod rosnąco":
                    _orgCRM.ProduktySortujPoKodzie();
                    break;

                case "Cena rosnąco":
                    _orgCRM.ProduktySortujPoCenie();
                    break;

                case "Cena malejąco":
                    _orgCRM.ProduktySortujPoCenie(false);
                    break;

                default:
                    MessageBox.Show("Sortowanie zakończyło się niepowodzeniem.");
                    break;
            }
            lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
        }

        private void buttonDodajProdukt_Click(object sender, RoutedEventArgs e)
        {
            Produkt p = new Produkt();
            ProduktWindow okno = new ProduktWindow(p);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _orgCRM.DodajProdukt(p);
                lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
                textblockLiczbaProduktow.Text = _orgCRM.PodajIloscProduktow().ToString();
            }

        }

        private void buttonUsunProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedIndex > -1)
            {
                Produkt p = (Produkt)lstProdukty.SelectedItem;
                _orgCRM.UsunProdukt(p);
                lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
                textblockLiczbaProduktow.Text = _orgCRM.PodajIloscProduktow().ToString();
            }
        }
        private void buttonEdytujProdukt_Click(object sender, RoutedEventArgs e)
        {
            if (lstProdukty.SelectedIndex > -1)
            {
                Produkt p = (Produkt)lstProdukty.SelectedItem;
                Produkt p1 = (Produkt)p.Clone();
                ProduktWindow okno = new ProduktWindow(p1);
                bool? ret = okno.ShowDialog();
                if (ret == true)
                {
                    _orgCRM.DodajProdukt(p1);
                    _orgCRM.UsunProdukt(p);
                    lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
                }
            }
        }
        private void buttonUsunWszystkie_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie produkty?", "Uwaga!", MessageBoxButton.YesNo);
            if (message == MessageBoxResult.Yes)
            {
                _orgCRM.UsunWszystkieProdukty();
                lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
                textblockLiczbaProduktow.Text = _orgCRM.PodajIloscProduktow().ToString();
            }
        }
        private void buttonPokazWszystkie_Click(object sender, RoutedEventArgs e)
        {
            lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ListaProduktow);
            textblockLiczbaProduktow.Text = _orgCRM.PodajIloscProduktow().ToString();
        }

        private void buttonWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            
            string wybor = cmbboxSzukaj.Text;

            switch (wybor)
            {
                case "Kod":
                    txtKod.Visibility = System.Windows.Visibility.Visible;
                    txtBoxKod.Visibility= System.Windows.Visibility.Visible;
                    break;

                case "Jednostka":
                    txtJednostka.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoJednostce.Visibility = System.Windows.Visibility.Visible;
                    cmbWyszPoJednostce.ItemsSource = Enum.GetValues(typeof(Jednostki));
                    break;

                case "Cena":
                    txtCena.Visibility = System.Windows.Visibility.Visible;
                    txtBoxCena.Visibility = System.Windows.Visibility.Visible;
                    cmbTD.Visibility= System.Windows.Visibility.Visible;
                    cmbTD.Items.Add("poniżej");
                    cmbTD.Items.Add("powyżej");
                    break;

                default:
                    MessageBox.Show("Nie udało się wyszukać produktów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }
            InputBox.Visibility = System.Windows.Visibility.Visible;

        }
        private void butWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            string wybor = cmbboxSzukaj.Text;

            switch (wybor)
            {
                case "Kod":
                    try
                    {
                        string kod = txtBoxKod.Text;
                        lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ZnajdzProduktKod(kod));
                        textblockLiczbaProduktow.Text = (_orgCRM.ZnajdzProduktKod(kod).Count().ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się znaleźć produktów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    txtKod.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKod.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKod.Text = String.Empty;
                    break;

                case "Jednostka":
                    try
                    {
                        Enum.TryParse<Jednostki>(cmbWyszPoJednostce.SelectedValue.ToString(), out Jednostki j);
                        lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ZnajdzWszystkieProduktyJednostka(j));
                        textblockLiczbaProduktow.Text = (_orgCRM.ZnajdzWszystkieProduktyJednostka(j).Count().ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się znaleźć produktów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    txtJednostka.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoJednostce.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoJednostce.Text = String.Empty;
                    break;

                case "Cena":
                    try
                    {
                        double.TryParse(txtBoxCena.Text, out double c);
                        string temp = cmbTD.SelectedValue.ToString();
                        bool tansze = true;
                        if (temp.Equals("powyżej"))
                        {
                            tansze = false;
                        }
                        lstProdukty.ItemsSource = new ObservableCollection<Produkt>(_orgCRM.ZnajdzWszystkieProduktyCena(c, tansze));
                        textblockLiczbaProduktow.Text = (_orgCRM.ZnajdzWszystkieProduktyCena(c, tansze).Count().ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nie udało się znaleźć produktów.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    txtCena.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxCena.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxCena.Text = String.Empty;
                    cmbTD.Text = String.Empty;
                    cmbTD.Items.Clear();
                    break;

                default:
                    return;
            }

        }
        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            string wybor = cmbboxSzukaj.Text;

            switch (wybor)
            {
                case "Kod":
                    txtKod.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKod.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxKod.Text = String.Empty;
                    break;

                case "Jednostka":
                    txtJednostka.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoJednostce.Visibility = System.Windows.Visibility.Collapsed;
                    cmbWyszPoJednostce.Text = String.Empty;
                    break;

                case "Cena":
                    txtCena.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxCena.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxCena.Text = String.Empty;
                    cmbTD.Text = String.Empty;
                    cmbTD.Items.Clear();
                    break;

                default:
                    return;
            }
        }



    }
}