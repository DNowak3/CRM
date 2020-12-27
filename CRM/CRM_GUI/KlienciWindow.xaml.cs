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
    /// Logika interakcji dla klasy KlienciWindow.xaml
    /// </summary>
    public partial class KlienciWindow : Window
    {

        OrgProwadzacaCRM _orgCRM;

        public KlienciWindow(OrgProwadzacaCRM orgCRM)
        {
            InitializeComponent();
            _orgCRM = orgCRM;
            lstKlienci.ItemsSource = new ObservableCollection<Klient>(orgCRM.ListaKlientow);
            textblockLiczbaKlientow.Text = orgCRM.PodajIloscKlientow().ToString();
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            Klient k = new Klient();
        }
    }
}
