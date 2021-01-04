﻿using CRM;
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
    /// Logika interakcji dla klasy ProduktWindow.xaml
    /// </summary>
    public partial class ProduktWindow : Window
    {
        Produkt _p;
        public ProduktWindow()
        {
            InitializeComponent();
            cmbJednostka.ItemsSource = Enum.GetValues(typeof(Jednostki));
            
        }
        public ProduktWindow(Produkt p):this()
        {
            _p = p;
            if(!_p.Kod.Equals(string.Empty))
            {
                txtKod.Text = _p.Kod;
                txtNazwa.Text = _p.Nazwa;
                txtCena.Text = _p.Cena.ToString();
                cmbJednostka.SelectedItem = _p.Jednostka;
            }
            else
            {
                txtKod.Visibility = System.Windows.Visibility.Collapsed;
                labKod.Visibility = System.Windows.Visibility.Collapsed;
                txtKod.Text = String.Empty;
                txtNazwa.Text = String.Empty;
                txtCena.Text = String.Empty;

            }                   
            
        }
        

        private void buttonZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtNazwa.Text != "" && txtCena.Text != "" && !cmbJednostka.SelectedIndex.Equals(-1))
            {
                string nazwa = txtNazwa.Text;
                double cena=0;
                Jednostki j = Jednostki.szt;
                try
                {
                    cena = Convert.ToDouble(txtCena.Text);
                }
                catch(FormatException)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie produktu nie powiodło się. Niepoprawna cena. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                    }
                    else
                    {
                        return;
                    }
                    
                }
                catch (OverflowException)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie produktu nie powiodło się. Niepoprawna cena. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                    }
                    else
                    {
                        return;
                    }
                }
                try
                {
                    Enum.TryParse<Jednostki>(cmbJednostka.SelectedValue.ToString(), out j);
                }
                catch(ArgumentException)
                {
                    MessageBoxResult m = MessageBox.Show("Dodawanie produktu nie powiodło się. Niepoprawna jednostka. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (m == MessageBoxResult.Yes)
                    {
                        DialogResult = false;
                    }
                    else
                    {
                        return;
                    }
                }
                if (!_p.Kod.Equals(string.Empty))
                {
                    _p.Nazwa = nazwa;
                    _p.Cena = cena;
                    _p.Jednostka = j;
                }
                else
                {
                    _p.Nazwa = nazwa;
                    _p.Cena = cena;
                    _p.Jednostka = j;
                    _p.Kod = $"{_p.Nazwa[0]}{DateTime.Now.Year}{Produkt.AktualnyNumer}";
                }
                DialogResult = true;
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("Dodawanie produktu nie powiodło się. Brakuje kluczowych informacji. Czy chcesz kontynuować mimo to?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (m == MessageBoxResult.Yes)
                {
                    DialogResult = false;
                }
                else
                {
                    return;
                }
            }

        }

        private void buttonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}