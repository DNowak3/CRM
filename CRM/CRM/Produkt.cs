using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public enum Jednostki { szt, kg, m}
    public class Produkt:IComparable<Produkt>,ICloneable
    {
            static int _aktualnyNumer;
            string _nazwa;
            double _cena;
            string _kod;
            Jednostki _jednostka;

            public string Nazwa { get => _nazwa; set => _nazwa = value; }
            public double Cena { get => _cena; set => _cena = value; }
            public Jednostki Jednostka { get => _jednostka; set => _jednostka = value; }
            public string Kod { get => _kod; }

            static Produkt()
            {
                _aktualnyNumer = 0;
            }
            public Produkt()
            {
                Nazwa = string.Empty;
                _kod = string.Empty;
                Cena = 0;
                Jednostka = Jednostki.szt;
            }
            public Produkt(string nazwa, double cena) :this()
            {
                Nazwa = nazwa;
                Cena = cena;
                _aktualnyNumer++;
                _kod = $"{Nazwa[0]}{DateTime.Now.Year}{_aktualnyNumer}";
            }

            public Produkt(string nazwa, double cena, Jednostki jednostka) :this(nazwa, cena)
            {
                Jednostka = jednostka;
            }

            public override string ToString()
            {
                return $"{Kod} {Nazwa} {Cena:C} za {Jednostka}";
            }
        //Dodałam na szybko bo potrzebowałam do sortowania w liście: Daga
        public int CompareTo(Produkt other)
        {
            return Nazwa.CompareTo(other.Nazwa);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
