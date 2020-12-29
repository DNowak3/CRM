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
        }

        public OsobaKontaktowaWindow(OsobaKontakt osoba):this()
        {
            _o = osoba;
        }
    }
}
