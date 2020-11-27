using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class Pracownik:OsobaKontakt
    {
        DateTime _dataRozpoczeciaPracy;
        #region Konstruktory
        public Pracownik() : base()
        {
            Telefon = Mail = Notatki = String.Empty;
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia) : base(imie, nazwisko, plec, stanowisko)
        {
            DateTime.TryParseExact(dataRozpoczecia, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out _dataRozpoczeciaPracy);
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia, string telefon) : this(imie, nazwisko, plec, stanowisko, dataRozpoczecia)
        {
            Telefon = telefon;
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia, string telefon, string mail) : this(imie, nazwisko, plec, stanowisko, dataRozpoczecia, telefon)
        {
            Mail = mail;
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia, string telefon, string mail, string notatki) : this(imie, nazwisko, plec, stanowisko, dataRozpoczecia, telefon, mail)
        {
            Notatki = notatki;
        }
        #endregion

        public override string ToString()
        {
            return "Data rozpoczecia pracy: "+_dataRozpoczeciaPracy.ToString("dd-MM-yyyy")+" "+base.ToString();
        }
    }
}
