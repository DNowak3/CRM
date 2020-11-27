using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public enum Stanowiska { sekretariat, dyrekcja, konsultant, sprzedawca, inne};
    public enum Plcie { K, M,Nieznana };
    public abstract class Osoba: IComparable<Osoba>
    {
        string _imie;
        string _nazwisko;
        Plcie _plec;
        Stanowiska _stanowisko;

        public string Imie { get => _imie; set => _imie = FormatZWielkiej(value); }
        public string Nazwisko { get => _nazwisko; set => _nazwisko = FormatZWielkiej(value); }
        public Plcie Plec { get => _plec; set => _plec = value; }
        public Stanowiska Stanowisko { get => _stanowisko; set => _stanowisko = value; }

        #region Konstruktory
        protected Osoba()
        {
            Imie = Nazwisko = null;
            Plec = Plcie.Nieznana;
            Stanowisko = Stanowiska.inne;
        }
        protected Osoba(string imie, string nazwisko, Plcie plec):this()
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Plec = plec;
        }
        protected Osoba(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko):this(imie, nazwisko, plec)
        {
            Stanowisko = stanowisko;
        }
        #endregion
        #region Funkcje
        string FormatZWielkiej(string s)
        {
            string wynik = null;
            if (!(s==null))
            {
                wynik += Char.ToUpper(s[0]);
                for (int i = 1; i < s.Length; ++i)
                {
                    wynik += Char.ToLower(s[i]);
                }
            }
            return wynik;
        }
        public int CompareTo(Osoba other)
        {
            int wynik = Nazwisko.CompareTo(other.Nazwisko);
            if (wynik == 0)
            {
                return Imie.CompareTo(other.Imie);
            }
            return wynik;
        }
        #endregion
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Plec} {Stanowisko}";
        }
    }
}
