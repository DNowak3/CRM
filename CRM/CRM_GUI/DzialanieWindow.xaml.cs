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
    /// Logika interakcji dla klasy DzialanieWindow.xaml
    /// </summary>
    public partial class DzialanieWindow : Window
    {
        Dzialanie _d;
        public DzialanieWindow()
        {
            _d = new Dzialanie();
            InitializeComponent();
            cmbokWynik.ItemsSource = Enum.GetValues(typeof(WynikDzialania));
        }

        public DzialanieWindow(Dzialanie d) : this()
        {
            _d = d;
            txtData.Text = _d.Data.ToString();
            txtNazwa.Text = _d.Nazwa;
            cmbokWynik.SelectedItem = _d.Wynik;
            txtOpis.Text = _d.Opis;
        }

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && txtData.Text != "")
            {
                _d.Nazwa = txtNazwa.Text;
                _d.Opis = txtOpis.Text;
                Enum.TryParse<WynikDzialania>(cmbokWynik.SelectedValue.ToString(), out WynikDzialania w);
                _d.Wynik = w;
                DateTime.TryParseExact(txtData.Text, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime d);
                _d.Data = d;
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
