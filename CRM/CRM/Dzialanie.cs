using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Typ wyliczeniowy, zawiera stale wartosci bedace wynikiem dzialania z klientem.
    /// </summary>
    enum WynikDzialania { skontaktowano, umowiono, ukonczono, anulowano, zaplanowano, zaplata, wygrana, przegrana }
   
    /// <summary>
    /// Klasa definiujaca dzialania wobec klientow.
    /// </summary>
    class Dzialanie : ICloneable, IComparable<Dzialanie>
    {
        /// <summary>
        /// Nazwa dzialania.
        /// </summary>
        string _nazwa;
        /// <summary>
        /// Data dzialania.
        /// </summary>
        DateTime _data;
        /// <summary>
        /// Praciwnik, ktory wykonal dzialanie.
        /// </summary>
        Pracownik _pracownik;
        /// <summary>
        /// Osoba reprezentujaca klienta, z ktora sie skontaktowano.
        /// </summary>
        OsobaKontakt _osobaKontaktowa;
        /// <summary>
        /// Ogolny rezultat podjetego dzialania.
        /// </summary>
        WynikDzialania _wynik;
        /// <summary>
        /// Opis dzialania.
        /// </summary>
        string _opis;

        /// <summary>
        /// Wlasciwosci.
        /// </summary>
        public string Nazwa { get => _nazwa; set => _nazwa = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public string Opis { get => _opis; set => _opis = value; }
        internal Pracownik Pracownik { get => _pracownik; set => _pracownik = value; }
        internal OsobaKontakt OsobaKontaktowa { get => _osobaKontaktowa; set => _osobaKontaktowa = value; }
        internal WynikDzialania Wynik { get => _wynik; set => _wynik = value; }

        /// <summary>
        /// Podstawowy konstruktor parametryczny.
        /// </summary>
        /// <param name="nazwa">Nazwa dzialania</param>
        /// <param name="data">Data dzialania</param>
        public Dzialanie(string nazwa, string data)
        {
            Nazwa = nazwa;
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(data, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            Data = d;
        }

        /// <summary>
        /// Bardziej szczegolowy konstruktor, wywoluje poprzedni.
        /// </summary>
        /// <param name="nazwa">Nazwa dzialania</param>
        /// <param name="data">Data dzialania</param>
        /// <param name="pracownik">Pracownik, ktory wykonal dzialanie</param>
        /// <param name="osobaKontaktowa">Osoba, z ktora skontaktowal sie pracownik</param>
        /// <param name="wynik">Ogolny rezultat dzialania</param>
        public Dzialanie(string nazwa, string data, Pracownik pracownik, OsobaKontakt osobaKontaktowa, WynikDzialania wynik) : this(nazwa, data)
        {
            Pracownik = pracownik;
            OsobaKontaktowa = osobaKontaktowa;
            Wynik = wynik;
        }

        /// <summary>
        /// Konstruktor, ktory ustawia wartosci dla wszystkich pol i wywoluje poprzedni konstruktor.
        /// </summary>
        /// <param name="nazwa">Nazwa dzialania</param>
        /// <param name="data">Data dzialania</param>
        /// <param name="pracownik">Pracownik, ktory wykonal dzialanie</param>
        /// <param name="osobaKontaktowa">Osoba, z ktora skontaktowal sie pracownik</param>
        /// <param name="wynik">Ogolny rezultat dzialania</param>
        /// <param name="opis">Opis dzialania</param>
        public Dzialanie(string nazwa, string data, Pracownik pracownik, OsobaKontakt osobaKontaktowa, WynikDzialania wynik, string opis) : this(nazwa, data, pracownik, osobaKontaktowa, wynik)
        {
            Opis = opis;
        }

        /// <summary>
        /// Meotda tworzy czytelna, tekstowa reprezentacje dzialania.
        /// </summary>
        /// <returns>Zwraca ciag opisujacy biezace dzialanie</returns>
        public override string ToString()
        {
            string napis = $"{Data.ToString("dd.MM.yyyy")} - {Nazwa}";
            if(Pracownik == null)
            {
                return napis;
            }
            napis += Pracownik == null ? null : $"\n             Pracownik: {Pracownik.ToString()}";
            napis += OsobaKontaktowa == null ? null : $"\n             Skontaktowano z: {OsobaKontaktowa.ToString()}";
            napis += Opis == null ? null : $"\n             Opis: {Opis}";
            napis += $"\n             Wynik: {Wynik}";
            return napis;
        }

        /// <summary>
        /// Metoda tworzy obiekt klasy dzialanie bedacy kopia biezacego dzialania.
        /// </summary>
        /// <returns>Zwraca nowe dzialanie bedace kopia danego dzialania</returns>
        public object Clone()
        {
            Dzialanie d = new Dzialanie(Nazwa, Data.ToString("dd.MM.yyyy"));
            if (Pracownik == null) { }
            else
            {
                d.Pracownik = (Pracownik)Pracownik.Clone();
                d.OsobaKontaktowa = (OsobaKontakt)OsobaKontaktowa.Clone();
                d.Wynik = Wynik;
                if(Opis != null)
                {
                    d.Opis = Opis;
                }
            }
            return d;
        }

        /// <summary>
        /// Metoda porownuje dwa obiekty klasy dzialanie na podsatwie dat wykonania.
        /// </summary>
        /// <param name="other">Drugie dzialanie, ktore ma zostac porownane z biezacym obiektem</param>
        /// <returns>
        /// Zwraca liczbe ze znakiem, wskazujaca na kolejnosc porownywanych dzialan.
        ///  Wartosc < 0 oznacza, ze dzialanie other nastapilo po biezacym dzialaniu.
        ///  Wartosc = 0 oznacza, ze dzialania maja te sama date.
        ///  Wartosc > 0 oznacza, ze dzialanie other poprzedzilo biezace dzialanie.
        /// </returns>
        public int CompareTo(Dzialanie other)
        {
            return Data.CompareTo(other.Data);
        }

    }
}
