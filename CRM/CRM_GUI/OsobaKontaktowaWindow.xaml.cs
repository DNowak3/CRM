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
using System.Windows.Shapes;

namespace CRM_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy OsobaKontaktowaWindow.xaml
    /// </summary>
    public partial class OsobaKontaktowaWindow : Window
    {
        OsobaKontakt _o;
        public OsobaKontaktowaWindow()
        {
            _o = new OsobaKontakt();
            InitializeComponent();
            cmbStanowisko.ItemsSource = Enum.GetValues(typeof(Stanowiska));
        }

        public OsobaKontaktowaWindow(OsobaKontakt osoba):this()
        {
            _o = osoba;
            txtImie.Text = _o.Imie;
            txtNazwisko.Text = _o.Nazwisko;
            cmbPlec.SelectedItem = _o.Plec;
            cmbStanowisko.SelectedItem = _o.Stanowisko;
            txtTelefon.Text = _o.Telefon;
            txtMail.Text = _o.Mail;
            txtNotatki.Text = _o.Notatki;
        }

        private void butZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtImie.Text != "" && txtNazwisko.Text != "")
            {
                _o.Imie = txtImie.Text;
                _o.Nazwisko = txtNazwisko.Text;
                Enum.TryParse<Stanowiska>(cmbStanowisko.SelectedValue.ToString(), out Stanowiska s);
                _o.Stanowisko = s;
                Enum.TryParse<Plcie>(cmbPlec.SelectedValue.ToString(), out Plcie p);
                _o.Plec = p;
                _o.Telefon = txtTelefon.Text;
                _o.Mail = txtMail.Text;
                _o.Notatki = txtNotatki.Text;
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void butAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
