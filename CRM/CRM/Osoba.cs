using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Typ wyliczeniowy, zawiera stanowiska, na jakim są osoby kontaktowe.
    /// </summary>
    public enum Stanowiska { sekretariat, dyrekcja, konsultant, sprzedawca, inne};
    /// <summary>
    /// Typ wyliczeniowy, zawiera płcie.
    /// </summary>
    public enum Plcie { K, M,Nieznana };

    /// <summary>
    /// Klasa abstrakcyjna definiująca osobę.
    /// </summary>
    [Serializable]
    public abstract class Osoba: IComparable<Osoba>
    {
        /// <summary>
        /// Imię osoby.
        /// </summary>
        string _imie;
        /// <summary>
        /// Nazwisko osoby.
        /// </summary>
        string _nazwisko;
        /// <summary>
        /// Płeć osoby.
        /// </summary>
        Plcie _plec;
        /// <summary>
        /// Stanowisko jakie zajmuje osoba.
        /// </summary>
        Stanowiska _stanowisko;
        #region Wlasciwosci
        public string Imie { get => _imie; set => _imie = FormatZWielkiej(value); }
        public string Nazwisko { get => _nazwisko; set => _nazwisko = FormatZWielkiej(value); }
        public Plcie Plec { get => _plec; set => _plec = value; }
        public Stanowiska Stanowisko { get => _stanowisko; set => _stanowisko = value; }
        #endregion
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
        /// <summary>
        /// Funkcja gwarantująca, że wpisany napis będzie zaczynał się z wielkiej litery, a wszystkie następujące litery będą małe.
        /// </summary>
        /// <param name="napis">Napis, który chcemy zmienić.</param>
        /// <returns>Podany napis zaczynający się z wielkiej litery, a wszystkie litery potem są małe</returns>
        protected string FormatZWielkiej(string napis)
        {
            string wynik = null;
            if (!(napis==null))
            {
                wynik += Char.ToUpper(napis[0]);
                for (int i = 1; i < napis.Length; ++i)
                {
                    wynik += Char.ToLower(napis[i]);
                }
            }
            return wynik;
        }
        /// <summary>
        /// Porównuje dwie osoby ze sobą, najpierw po nazwisku, potem po imieniu. 
        /// </summary>
        /// <param name="other">Osoba, z którą porównujemy.</param>
        /// <returns>
        /// Zwraca waryość porównania nazwisk:
        /// 0 jeżeli nazwiska obydwu osób są równe
        /// mniej niż 0 jeżeli nazwisko obecnej osoby jest mniejsze niż nazwisko porównywanej osoby
        /// więcej niż 0 jeżeli nazwisko obecnej osoby jest większe niż nazwisko porównywanej osoby
        ///  Jeśli dwa nazwiska są równe zwraca wartość porównania imion.</returns>
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
        /// <summary>
        /// Funkcja tworzy i zwraca napis z danymi osoby.
        /// </summary>
        /// <returns>Napis zawierający dane osoby</returns>
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Plec} {Stanowisko}";
        }
    }
}
