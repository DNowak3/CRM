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
    /// Logika interakcji dla klasy OsobyKontaktoweWindow.xaml
    /// </summary>
    public partial class OsobyKontaktoweWindow : Window
    {
        Klient _k;

        public OsobyKontaktoweWindow(Klient klient)
        {
            InitializeComponent();
            _k = klient;
            lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
        }
    }
}
