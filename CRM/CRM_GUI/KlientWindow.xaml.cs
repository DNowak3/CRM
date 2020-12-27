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
    /// Logika interakcji dla klasy KlientWindow.xaml
    /// </summary>
    public partial class KlientWindow : Window
    {
        Klient k;
        public KlientWindow()
        {
            k = new Klient();
            InitializeComponent();
        }

        public KlientWindow(Klient klient) : this()
        {
            k = klient;
            txtNazwa.Text = k.Nazwa;
            cmboxBranze.SelectedItem = k.Branza;
            txtNIP.Text = k.Nip;
            txtDataZal.Text = k.DataZalozenia.ToString();
            txtKraj.Text = k.Kraj;
            txtMiasto.Text = k.Miasto;
            cmboxStatus.SelectedItem = k.Status;
            txtUwagi.Text = k.Uwagi;
            txtPlanKontakt.Text = k.DataPlanowanegoKontaktu.ToString();
        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && cmboxBranze.Text != "")
            {
                k.Nazwa = txtNazwa.Text;
                Enum.TryParse<Branże>(cmboxBranze.SelectedValue.ToString(), out Branże b);
                k.Branza = b;
                k.Nip = txtNIP.Text;
                DateTime.TryParseExact(txtDataZal.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d);
                k.DataZalozenia = d;
                k.Kraj = txtKraj.Text;
                k.Uwagi = txtUwagi.Text;
                k.Miasto = txtMiasto.Text;
                Enum.TryParse<Status>(cmboxBranze.SelectedValue.ToString(), out Status s);
                k.Status = s;
                DateTime.TryParseExact(txtPlanKontakt.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d_2);
                k.DataPlanowanegoKontaktu = d_2;

                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
