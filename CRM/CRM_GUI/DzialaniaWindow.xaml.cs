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
    /// Logika interakcji dla klasy DzialaniaWindow.xaml
    /// </summary>
    public partial class DzialaniaWindow : Window
    {
        Klient _k;

        public DzialaniaWindow(Klient klient)
        {
            InitializeComponent();
            _k = klient;
            textblockNazwaFirmy.Text = _k.Nazwa.ToUpper();
            lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
        }

        private void buttonDodajDzialanie_Click(object sender, RoutedEventArgs e)
        {
            Dzialanie d = new Dzialanie();
            DzialanieWindow okno = new DzialanieWindow(d);
            bool? ret = okno.ShowDialog();
            if (ret == true)
            {
                _k.DodajDzialanie(d);
                lstDzialania.ItemsSource = new ObservableCollection<Dzialanie>(_k.DzialaniaList);
            }
        }
    }
}
