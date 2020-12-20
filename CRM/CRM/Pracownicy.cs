using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public static class Pracownicy
    {
        static List<Pracownik> _listaPracownikow;
        static int _iloscPracownikow;

        static Pracownicy()
        {
            _listaPracownikow = new List<Pracownik>();
            _iloscPracownikow = 0;
        }
        #region Funkcje
        public static void Dodaj(Pracownik p)
        {
            _listaPracownikow.Add(p);
            _iloscPracownikow++;
        }
        public static void Usun(string imie, string nazwisko, Stanowiska stanowisko)
        {
            if (_iloscPracownikow != 0 && JestPracownikiem(imie, nazwisko, stanowisko))
            {
                _listaPracownikow.RemoveAll(p => (p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko));
                _iloscPracownikow--;
            }
        }
        public static void Usun(Pracownik p)
        {
            if (_iloscPracownikow != 0 && JestPracownikiem(p))
            {
                _listaPracownikow.Remove(p);
                _iloscPracownikow--;
            }
        }
        public static void UsunWszystkich()
        {
            _listaPracownikow.Clear();
            _iloscPracownikow = 0;
        }
        public static int PodajIloscPracownikow()
        {
            return _iloscPracownikow;
        }
        public static bool JestPracownikiem (Pracownik p)
        {
            return _listaPracownikow.Contains(p);
        }
        public static bool JestPracownikiem(string imie, string nazwisko, Stanowiska stanowisko)
        {
            return _listaPracownikow.Exists(p => p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko);
        }
        public static List<Pracownik> ZnajdzWszystkichNaStanowisku(Stanowiska stanowisko)
        {
            return _listaPracownikow.FindAll(p => p.Stanowisko == stanowisko);
        }
        public static List<Pracownik> ZnajdzWszystkichOPlci(Plcie plec)
        {
            return _listaPracownikow.FindAll(p => p.Plec == plec);
        }
        public static List<Pracownik> ZnajdzWszystkichKtorzyZaczeliPracePrzed(string dataRozpoczecia)
        {
            DateTime DataRozpoczecia;
            DateTime.TryParseExact(dataRozpoczecia, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DataRozpoczecia);

            return _listaPracownikow.FindAll(p => p.DataRozpoczeciaPracy<DataRozpoczecia);
        }
        public static void Sortuj()
        {
            _listaPracownikow.Sort();
        }

        #endregion
        public static string ToString()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Pracownicy:");
            _listaPracownikow.ForEach(p => napis.AppendLine(p.ToString()));
            return napis.ToString();
        }
    }
}
