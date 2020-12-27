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
    /// Logika interakcji dla klasy DzialaniaWindow.xaml
    /// </summary>
    public partial class DzialaniaWindow : Window
    {
        Klient k;
        public DzialaniaWindow()
        {
            InitializeComponent();
        }

        public DzialaniaWindow(Klient klient)
        {
            InitializeComponent();
            k = klient;
        }

    }
}
