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
using CRM;
using static CRM.Konkurent;

namespace CRM_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy KonkurentWindow.xaml
    /// </summary>
    public partial class KonkurentWindow : Window
    {
        Konkurent k;
        public KonkurentWindow()
        {
            k = new Konkurent();
            InitializeComponent();
            comboBoxBranze.SelectedIndex = 7;
            comboBoxZagrozenie.SelectedIndex = 2;
        }

        public KonkurentWindow(Konkurent konkurent) : this()
        {
            k = konkurent;

            txtNazwa.Text = k.Nazwa;
            comboBoxBranze.SelectedItem = k.Branza;
            txtNIP.Text = k.Nip;
            txtDataZal.Text = k.DataZalozenia.ToString("dd-MM-yyyy");
            txtKraj.Text = k.Kraj;
            txtMiasto.Text = k.Miasto;
            txtKodPocztowy.Text = k.KodPocztowy;
            comboBoxZagrozenie.SelectedItem = k.Zagrozenie;
            txtNotatki.Text = k.Notatki;
        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && comboBoxBranze.Text != "")
            {
                k.Nazwa = txtNazwa.Text;
                Enum.TryParse(comboBoxBranze.SelectedValue.ToString(), out Branże b);
                k.Branza = b;
                k.Nip = txtNIP.Text;
                DateTime.TryParseExact(txtDataZal.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d);
                k.DataZalozenia = d;
                k.Kraj = txtKraj.Text;
                k.Miasto = txtMiasto.Text;
                k.KodPocztowy = txtKodPocztowy.Text;
                Enum.TryParse(comboBoxZagrozenie.SelectedValue.ToString(), out StopienZagrozenia z);
                k.Zagrozenie = z;
                k.Notatki = txtNotatki.Text;

                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}

