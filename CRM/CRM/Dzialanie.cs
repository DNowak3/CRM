using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    //sortowanie wg daty?
    //klonowanie
    enum WynikDzialania { skontaktowano, umowiono, ukonczono, anulowano, zaplanowano, zaplata, wygrana, przegrana }
    class Dzialanie
    {
        string _nazwa;
        DateTime _data;
        Pracownik _pracownik;
        OsobaKontakt _osobaKontaktowa;
        WynikDzialania _wynik;
        string _opis;

        public string Nazwa { get => _nazwa; set => _nazwa = value; }
        public DateTime Data { get => _data; set => _data = value; }
        public string Opis { get => _opis; set => _opis = value; }
        internal Pracownik Pracownik { get => _pracownik; set => _pracownik = value; }
        internal OsobaKontakt OsobaKontaktowa { get => _osobaKontaktowa; set => _osobaKontaktowa = value; }
        internal WynikDzialania Wynik { get => _wynik; set => _wynik = value; }

        public Dzialanie(string nazwa, string data)
        {
            Nazwa = nazwa;
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(data, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            Data = d;
        }

        public Dzialanie(string nazwa, string data, Pracownik pracownik, OsobaKontakt osobaKontaktowa, WynikDzialania wynik) : this(nazwa, data)
        {
            Pracownik = pracownik;
            OsobaKontaktowa = osobaKontaktowa;
            Wynik = wynik;
        }

        public Dzialanie(string nazwa, string data, Pracownik pracownik, OsobaKontakt osobaKontaktowa, WynikDzialania wynik, string opis) : this(nazwa, data, pracownik, osobaKontaktowa, wynik)
        {
            Opis = opis;
        }

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
    }
}
