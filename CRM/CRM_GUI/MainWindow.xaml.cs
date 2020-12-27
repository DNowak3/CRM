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
        OrgProwadzacaCRM _orgCRM = new OrgProwadzacaCRM();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void menuWyjdz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            bool? wynik = dlg.ShowDialog();
            if (wynik == true)
            {
                string nazwaPliku = dlg.FileName;
                MessageBox.Show("Zaraz sprobujemy zapisać do pliku: " + nazwaPliku);

                _orgCRM.Nazwa = txtNazwa.Text;
                Enum.TryParse<Branże>(cmbBranza.SelectedValue.ToString(), out Branże pom);
                _orgCRM.Branza = pom;
                DateTime.TryParseExact(txtDataZal.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime temp);
                _orgCRM.DataZalozenia = temp;
                _orgCRM.KodPocztowy = txtKodPoczt.Text;
                _orgCRM.Kraj = txtKraj.Text;
                _orgCRM.Miasto = txtMiasto.Text;
                _orgCRM.Notatki = txtNotatki.Text;
                _orgCRM.Nip = txtNIP.Text;
                _orgCRM.Adres = txtAdres.Text;
                //lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(_zespol.Czlonkowie);
                _orgCRM.ZapiszXML(nazwaPliku);
                MessageBox.Show("Teoretycznie udało się zaposać do pliku");
                _orgCRM = OrgProwadzacaCRM.OdczytajXML(nazwaPliku);


            }
        }
        private void menuWczytaj_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? wynik = dlg.ShowDialog();
            if (wynik == true)
            {
                string nazwaPliku = dlg.FileName;
                _orgCRM = OrgProwadzacaCRM.OdczytajXML(nazwaPliku);
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
                    
                    //lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(_zespol.Czlonkowie);

                }
            }
        }

        private void butPracownicy_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                PracownicyWindow okno = new PracownicyWindow(_orgCRM);
                bool? dodawac = okno.ShowDialog(); //boolpytajnik to nullable bool: true, fals i null

            }
        }

        private void butKlienci_Click(object sender, RoutedEventArgs e)
        {
            if (_orgCRM is OrgProwadzacaCRM)
            {
                KlienciWindow okno = new KlienciWindow(_orgCRM);
                okno.ShowDialog();
            }
        }
    }
}
