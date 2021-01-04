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
        Klient _k;
        public KlientWindow()
        {
            _k = new Klient();
            InitializeComponent();
            cmboxBranze.ItemsSource = Enum.GetValues(typeof(Branże));
            cmboxStatus.ItemsSource = Enum.GetValues(typeof(Status));
        }

        public KlientWindow(Klient klient) : this()
        {
            _k = klient;
            txtNazwa.Text = _k.Nazwa;
            cmboxBranze.SelectedItem = _k.Branza;
            txtNIP.Text = _k.Nip;
            txtDataZal.Text = _k.DataZalozenia.ToString("dd-MM-yyyy");
            txtKraj.Text = _k.Kraj;
            txtMiasto.Text = _k.Miasto;
            cmboxStatus.SelectedItem = _k.Status;
            txtUwagi.Text = _k.Uwagi;
            txtPlanKontakt.Text = _k.DataPlanowanegoKontaktu.ToString("dd-MM-yyyy");
        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && cmboxBranze.Text != "")
            {
                _k.Nazwa = txtNazwa.Text;
                Enum.TryParse<Branże>(cmboxBranze.SelectedValue.ToString(), out Branże b);
                _k.Branza = b;
                _k.Nip = txtNIP.Text;
                DateTime.TryParseExact(txtDataZal.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d);
                _k.DataZalozenia = d;
                _k.Kraj = txtKraj.Text;
                _k.Uwagi = txtUwagi.Text;
                _k.Miasto = txtMiasto.Text;
                Enum.TryParse<Status>(cmboxBranze.SelectedValue.ToString(), out Status s);
                _k.Status = s;
                DateTime.TryParseExact(txtPlanKontakt.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d_2);
                _k.DataPlanowanegoKontaktu = d_2;

                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
