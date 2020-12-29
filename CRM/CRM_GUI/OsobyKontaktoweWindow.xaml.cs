﻿using CRM;
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
            textblockNazwaFirmy.Text = _k.Nazwa.ToUpper();
            lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
        }

        private void buttonDodajKontakt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonEdytujKontakt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonUsunKontakt_Click(object sender, RoutedEventArgs e)
        {
            if (lstKontakty.SelectedIndex > -1)
            {
                OsobaKontakt o = (OsobaKontakt)lstKontakty.SelectedItem;
                _k.UsunKontakt(o);
                lstKontakty.ItemsSource = new ObservableCollection<OsobaKontakt>(_k.ListaKontaktow);
            }
        }

        private void buttonUsunWszKontakty_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSortuj_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
