using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    static class Pracownicy
    {
        static List<Pracownik> _listaPracownikow;
        static int _iloscPracownikow;
        //internal static List<Pracownik> ListaPracownikow { get => _listaPracownikow;}

        static Pracownicy()
        {
            _listaPracownikow = new List<Pracownik>();
            _iloscPracownikow = 0;
        }

        public static void Dodaj(Pracownik p)
        {
            _listaPracownikow.Add(p);
            _iloscPracownikow++;
        }

        public static string ToString()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Pracownicy:");
            _listaPracownikow.ForEach(p => napis.AppendLine(p.ToString()));
            return napis.ToString();
        }
    }
}
