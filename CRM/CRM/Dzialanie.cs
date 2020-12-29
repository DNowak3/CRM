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
    public enum WynikDzialania { skontaktowano, umowiono, ukonczono, anulowano, zaplanowano, zaplata, wygrana, przegrana }

    /// <summary>
    /// Klasa definiujaca dzialania wobec klientow.
    /// </summary>
    public class Dzialanie : ICloneable, IComparable<Dzialanie>
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

        #region wlasciwosci i konstruktory
        /// <summary>
        /// Wlasciwosci.
        /// </summary>
        public string Nazwa { get => _nazwa; set => _nazwa = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public string Opis { get => _opis; set => _opis = value; }
        public Pracownik Pracownik { get => _pracownik; set => _pracownik = value; }
        public OsobaKontakt OsobaKontaktowa { get => _osobaKontaktowa; set => _osobaKontaktowa = value; }
        public WynikDzialania Wynik { get => _wynik; set => _wynik = value; }

        /// <summary>
        /// konstruktor nieparametryczny
        /// </summary>
        public Dzialanie()
        {

        }

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
        /// Konstruktor z dodatkowym parametrem wynik dzialania
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="data"></param>
        /// <param name="wynik"></param>
        /// /// <param name="opis"></param>
        public Dzialanie(string nazwa, string data, WynikDzialania wynik, string opis) : this(nazwa, data)
        {
            Wynik = wynik;
            Opis = opis;
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
        #endregion

        #region ToString, Clone i CompareTo
        /// <summary>
        /// Meotda tworzy czytelna, tekstowa reprezentacje dzialania.
        /// </summary>
        /// <returns>Zwraca ciag opisujacy biezace dzialanie</returns>
        public override string ToString()
        {
            string napis = $"{Data.ToString("dd.MM.yyyy")} - {Nazwa}";
            //napis += Pracownik == null ? null : $"\n             Pracownik: {Pracownik.ToString()}";
            //napis += OsobaKontaktowa == null ? null : $"\n             Skontaktowano z: {OsobaKontaktowa.ToString()}";
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
            d.Wynik = Wynik;
            if (Opis != null)
            {
                d.Opis = Opis;
            }
            if (Pracownik != null) 
            {
                d.Pracownik = (Pracownik)Pracownik.Clone();
                d.OsobaKontaktowa = (OsobaKontakt)OsobaKontaktowa.Clone();
            }
            return d;
        }

        /// <summary>
        /// Metoda porownuje dwa obiekty klasy dzialanie na podsatwie dat wykonania.
        /// </summary>
        /// <param name="other">Drugie dzialanie, ktore ma zostac porownane z biezacym obiektem</param>
        /// <returns>
        /// Zwraca liczbe ze znakiem, wskazujaca na kolejnosc porownywanych dzialan.
        ///  Wartosc mniejsza od 0 oznacza, ze biezace dzialanie bylo wczesniej niz other.
        ///  Wartosc rowna 0 oznacza, ze dzialania maja te sama date.
        ///  Wartosc wieksza od 0 oznacza, ze dzialanie other bylo pozniej niz aktualne.
        /// </returns>
        public int CompareTo(Dzialanie other)
        {
            return DateTime.Compare(Data, other.Data);
        }
        #endregion
    }
}
