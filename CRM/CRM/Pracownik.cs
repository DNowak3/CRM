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
        public DateTime DataRozpoczeciaPracy { get => _dataRozpoczeciaPracy; set => _dataRozpoczeciaPracy = value; }

        #region Konstruktory
        public Pracownik() : base()
        {
            Telefon = Mail = Notatki = String.Empty;
            DataRozpoczeciaPracy = DateTime.Today;
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko) : base(imie, nazwisko, plec, stanowisko)
        { }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia) : this(imie, nazwisko, plec, stanowisko)
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
            return base.ToString()+$" ({DataRozpoczeciaPracy.ToString("dd-MM-yyyy")})";
        }

    }
}
