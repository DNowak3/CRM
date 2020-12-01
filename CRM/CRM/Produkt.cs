using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class Produkt
    {
            static int _aktualnyNumer;
            string _nazwa;
            double _cena;
            string _kod;
            // enum jednostka

            public string Nazwa { get => _nazwa; set => _nazwa = value; }
            public double Cena { get => _cena; set => _cena = value; }
            public string Kod { get => _kod; }

            static Produkt()
            {
                _aktualnyNumer = 0;
            }
            // konstruktor nieparametryczny
            public Produkt(string nazwa, double cena)
            {
                Nazwa = nazwa;
                Cena = cena;
                _aktualnyNumer++;
                _kod = $"{Nazwa[0]}{DateTime.Now.Year}{_aktualnyNumer}";
            }

            public override string ToString()
            {
                return $"{Kod} {Nazwa} {Cena:C}";
            }


        
    }
}
