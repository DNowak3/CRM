using CRM;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrgProwadzacaCRM _orgCRM;
        public MainWindow()
        {

            InitializeComponent();
        }
        private void menuWyjdz_Click(object sender, RoutedEventArgs e)
        {

            if (txtNazwa.Text != "" && cmbBranza.Text != "")
            {
                MessageBoxResult m = MessageBox.Show("Chcesz zapisać plik?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (m == MessageBoxResult.Yes)
                {
                    menuZapisz_Click(sender, e);
                }
            }
            MessageBox.Show("Dziękujemy za używanie naszego programu, do zobaczenia!");
            Close();
        }

        private void menuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            bool? wynik = dlg.ShowDialog();
            if (wynik == true)
            {
                string nazwaPliku = dlg.FileName;

                if (txtNazwa.Text != "" && cmbBranza.Text != "")
                {
                    _orgCRM = new OrgProwadzacaCRM();
                    Enum.TryParse<Branże>(cmbBranza.Text, out Branże pom);
                    _orgCRM.Nazwa = txtNazwa.Text;
                    _orgCRM.Branza=pom;
                    
                    DateTime.TryParseExact(txtDataZal.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime temp);
                    _orgCRM.DataZalozenia = temp;
                    _orgCRM.KodPocztowy = txtKodPoczt.Text;
                    _orgCRM.Kraj = txtKraj.Text;
                    _orgCRM.Miasto = txtMiasto.Text;
                    _orgCRM.Notatki = txtNotatki.Text;
                    _orgCRM.Nip = txtNIP.Text;
                    _orgCRM.Adres = txtAdres.Text;
                }
                else
                {
                    MessageBox.Show("Aby zapisać organizację, należy podać co najmniej jej nazwę i branżę", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    if (nazwaPliku.EndsWith(".xml") || nazwaPliku.EndsWith(".XML"))
                    {
                        _orgCRM.ZapiszXML(nazwaPliku);
                    }
                    else
                    {
                        _orgCRM.ZapiszJSON(nazwaPliku);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie udało się zapisać pliku...\nWpisz format .xml lub .json.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void menuWczytaj_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? wynik = dlg.ShowDialog();
            if (wynik == true)
            {
                string nazwaPliku = dlg.FileName;
                try
                {
                    if(nazwaPliku.EndsWith(".xml") || nazwaPliku.EndsWith(".XML"))
                    {
                        _orgCRM = OrgProwadzacaCRM.OdczytajXML(nazwaPliku);
                    }
                    else
                    {
                        _orgCRM = OrgProwadzacaCRM.OdczytajJSON(nazwaPliku);
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Nie udało się wczytać pliku...\nWybierz format .xml lub .json.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (_orgCRM is OrgProwadzacaCRM)
                {
                    txtNazwa.Text = _orgCRM.Nazwa;
                    cmbBranza.SelectedItem = _orgCRM.Branza;
                    txtDataZal.Text = _orgCRM.DataZalozenia.ToString("dd-MM-yyyy");
                    txtKodPoczt.Text = _orgCRM.KodPocztowy;
                    txtKraj.Text = _orgCRM.Kraj;
                    txtMiasto.Text = _orgCRM.Miasto;
                    txtNotatki.Text = _orgCRM.Notatki;
                    txtNIP.Text = _orgCRM.Nip;
                    txtAdres.Text = _orgCRM.Adres;

                   //_orgZPliku = (OrgProwadzacaCRM)_orgCRM.Clone();
                }
            }
        }

        private void butPracownicy_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                PracownicyWindow okno = new PracownicyWindow(_orgCRM);
                okno.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("Tylko zapisane do pliku, lub z niego odczytane organizacje mają dostęp do listy pracowników.", "***", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void butKlienci_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                KlienciWindow okno = new KlienciWindow(_orgCRM);
                okno.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tylko zapisane do pliku, lub z niego odczytane organizacje mają dostęp do listy klientów.", "***", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void butKonkurenci_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                KonkurenciWindow okno = new KonkurenciWindow(_orgCRM);
                okno.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tylko zapisane do pliku, lub z niego odczytane organizacje mają dostęp do listy produktów.", "***", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void butProdukty_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                ProduktyWindow okno = new ProduktyWindow(_orgCRM);
                okno.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tylko zapisane do pliku, lub z niego odczytane organizacje mają dostęp do listy produktów.", "***", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
