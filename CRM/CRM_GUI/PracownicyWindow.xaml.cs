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
    /// Interaction logic for PracownicyWindow.xaml
    /// </summary>
    public partial class PracownicyWindow : Window
    {
        OrgProwadzacaCRM _orgCRM;

        public PracownicyWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(orgCRM.ListaPracownikow);
            txtBoxLiczbaPrac.Text = _orgCRM.PodajIloscPracownikow().ToString();
        }

        private void butDodajPrac_Click(object sender, RoutedEventArgs e)
        {
            Pracownik p = new Pracownik();
            PracownikWindow okno = new PracownikWindow(p);
            bool? dodawac = okno.ShowDialog(); //boolpytajnik to nullable bool: true, false i null
            if (dodawac == true)
            {
                _orgCRM.DodajPracownika(p); //dodajemy pracownika
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
                txtBoxLiczbaPrac.Text = _orgCRM.PodajIloscPracownikow().ToString();
            }
        }
        private void butEdytujDane_Click(object sender, RoutedEventArgs e)
        {
            if (lstPracownicy.SelectedIndex > -1)
            {
                Pracownik p = (Pracownik)lstPracownicy.SelectedItem;
                Pracownik zmieniony = (Pracownik)p.Clone();
                PracownikWindow okno = new PracownikWindow(zmieniony);
                bool? zmieniac = okno.ShowDialog();
                if (zmieniac == true)
                {
                    _orgCRM.DodajPracownika(zmieniony);
                    _orgCRM.UsunPracownika(p);
                    txtBoxLiczbaPrac.Text = _orgCRM.PodajIloscPracownikow().ToString();
                    lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
                }
            }
        }

        private void butUsunPrac_Click(object sender, RoutedEventArgs e)
        {
            if (lstPracownicy.SelectedIndex > -1)
            {
                Pracownik p = (Pracownik)lstPracownicy.SelectedItem;
                _orgCRM.UsunPracownika(p);
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
                txtBoxLiczbaPrac.Text = _orgCRM.PodajIloscPracownikow().ToString();
            }
        }

        private void butSortPrac_Click(object sender, RoutedEventArgs e)
        {
            string jakSortowac = cmbSortPrac.Text;

            if(jakSortowac == "Alfabetycznie")
            {
                _orgCRM.PracownicySortujAlfabetycznie();
            }
            else if(jakSortowac== "Po dacie zatrudnienia malejąco")
            {
                _orgCRM.PracownicySortujPoDacieZatrudnienia(false);
            }
            else if (jakSortowac == "Po dacie zatrudnienia rosnąco")
            {
                _orgCRM.PracownicySortujPoDacieZatrudnienia();
            }
            else
            {
                MessageBox.Show("Nie udało się posortować pracowników.","Błąd!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
        }

        private void butWyszukajPracPo_Click(object sender, RoutedEventArgs e)
        {
            if (cmbWyszukajPracPo.Text == "Po stanowisku")
            {
                txtStanowisko.Visibility = System.Windows.Visibility.Visible;
                cmbWyszPoStanowisku.Visibility = System.Windows.Visibility.Visible;
            }
            else if (cmbWyszukajPracPo.Text == "Po płci")
            {
                txtPlec.Visibility = System.Windows.Visibility.Visible;
                cmbWyszPoPlci.Visibility = System.Windows.Visibility.Visible;
            }
            else if (cmbWyszukajPracPo.Text == "Po dacie rozpoczęcia pracy")
            {
                txtData.Visibility = System.Windows.Visibility.Visible;
                txtBoxData.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Nie udało się wyszukać pracowników.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }
        private void butWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;
            if (cmbWyszukajPracPo.Text == "Po stanowisku")
            {
                Enum.TryParse<Stanowiska>(cmbWyszPoStanowisku.SelectedValue.ToString(), out Stanowiska pom);
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ZnajdzWszystkichPracownikowStanowisko(pom));
                cmbWyszPoStanowisku.Visibility = System.Windows.Visibility.Collapsed;
                txtStanowisko.Visibility = System.Windows.Visibility.Collapsed;

                cmbWyszPoStanowisku.Text = String.Empty;
            }
            else if (cmbWyszukajPracPo.Text == "Po płci")
            {
                Enum.TryParse<Plcie>(cmbWyszPoPlci.SelectedValue.ToString(), out Plcie pom);
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ZnajdzWszystkichPracownikowPlec(pom));
                cmbWyszPoPlci.Visibility = System.Windows.Visibility.Collapsed;
                txtPlec.Visibility = System.Windows.Visibility.Collapsed;

                cmbWyszPoPlci.Text = String.Empty;
            }
            else if (cmbWyszukajPracPo.Text == "Po dacie rozpoczęcia pracy")
            {
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ZnajdzWszystkichPracownikowPracaPrzed(txtBoxData.Text));
                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                txtData.Visibility = System.Windows.Visibility.Collapsed;
                txtBoxData.Text = String.Empty;
            }
        }

        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            if (cmbWyszukajPracPo.Text == "Po stanowisku")
            {
                cmbWyszPoStanowisku.Visibility = System.Windows.Visibility.Collapsed;
                cmbWyszPoStanowisku.Text = String.Empty;
            }
            else if (cmbWyszukajPracPo.Text == "Po płci")
            {
                cmbWyszPoPlci.Visibility = System.Windows.Visibility.Collapsed;
                cmbWyszPoPlci.Text = String.Empty;
            }
            else if (cmbWyszukajPracPo.Text == "Po dacie rozpoczęcia pracy")
            {
                txtBoxData.Visibility = System.Windows.Visibility.Collapsed;
                txtBoxData.Text = String.Empty;

            }
        }

        private void butPokazWszystkich_Click(object sender, RoutedEventArgs e)
        {
            lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
        }

        private void butUsunWszystkichPrac_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Wszyscy pracownicy zostaną usunięci, kontynuować?", "Uwaga!", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (m == MessageBoxResult.Yes)
            {
                _orgCRM.UsunWszystkichPracownikow();
                lstPracownicy.ItemsSource = new ObservableCollection<Pracownik>(_orgCRM.ListaPracownikow);
                txtBoxLiczbaPrac.Text = _orgCRM.PodajIloscPracownikow().ToString();
            }
            else if (m == MessageBoxResult.No)
            {
                MessageBox.Show("Pracownicy nie zostali usunięci","***",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
    }
}
