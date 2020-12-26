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
    /// Interaction logic for PracownikWindow.xaml
    /// </summary>
    public partial class PracownikWindow : Window
    {
        Pracownik p;
        public PracownikWindow()
        {
            p = new Pracownik();
            InitializeComponent();
        }
        public PracownikWindow(Pracownik prac) : this()
        {
            p = prac;

            txtImie.Text = p.Imie;
            txtNazwisko.Text = p.Nazwisko;
            txtDataRozp.Text = p.DataRozpoczeciaPracy.ToString("dd-MM-yyyy");
            txtMail.Text = p.Mail;
            txtTelefon.Text = p.Telefon;
            txtNotatki.Text = p.Notatki;
            cmbPlec.Text = ((p.Plec) == Plcie.K) ? "Kobieta" : ((p.Plec) == Plcie.M) ? "Mężczyzna":"Nieznana";
            cmbStanowisko.SelectedItem =p.Stanowisko;
        }
        private void butZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtImie.Text != "" && txtNazwisko.Text != "" && cmbStanowisko.Text != "" && cmbPlec.Text!="")
            {
                MessageBox.Show("Działa tutaj czy nie?");
                p.Imie = txtImie.Text;
                p.Nazwisko = txtNazwisko.Text;
                DateTime.TryParseExact(txtDataRozp.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime temp);
                p.DataRozpoczeciaPracy = temp;
                p.Plec = (cmbPlec.Text == "Kobieta") ? Plcie.K : (cmbPlec.Text == "Mężczyzna") ? Plcie.M : Plcie.Nieznana;
                Enum.TryParse<Stanowiska>(cmbStanowisko.SelectedValue.ToString(), out Stanowiska pom);
                p.Stanowisko = pom;
                p.Mail = txtMail.Text;
                p.Telefon = txtTelefon.Text;
                p.Notatki = txtNotatki.Text;

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
