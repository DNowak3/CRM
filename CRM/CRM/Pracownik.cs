using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CRM
{

    /// <summary>
    /// Klasa definiująca pracownika firmy, która prowadzi CRM. Dziedziczy po klasie OsobaKontakt.
    /// </summary>
    [Serializable]
    public class Pracownik:OsobaKontakt,IZapisywalna
    {
        /// <summary>
        /// Data, kiedy pracownik zaczął pracę w firmie, którra prowadzi CRM.
        /// </summary>
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
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia, string telefon, string mail) : this(imie, nazwisko, plec, stanowisko, dataRozpoczecia)
        {
            Telefon = telefon;

            Mail = mail;
        }
        public Pracownik(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string dataRozpoczecia, string telefon, string mail, string notatki) : this(imie, nazwisko, plec, stanowisko, dataRozpoczecia, telefon, mail)
        {
            Notatki = notatki;
        }

        #endregion
        #region Zapis/Odczyt
        /// <summary>
        /// Funkcja zapisuje dane pracownika do pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku do którego zapisujemy dane, musi się kończyć na ".xml"</param>
        public override void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Pracownik));
                xml.Serialize(sw, this);
            }
        }
        /// <summary>
        /// Funkcja odczytująca dane pracownika z pliku XML.
        /// </summary>
        /// <param name="nazwa">nazwa pliku z którego odczytujemy dane, musi się kończyć na ".xml"</param>
        /// <returns>Odczytany plik jako obiekt klasy Pracownik</returns>
        public static Pracownik OdczytajXML(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Pracownik));
                return (Pracownik)xml.Deserialize(sr);
            }
        }
        /// <summary>
        /// Funckja zapisująca dane pracownika do pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        public void ZapiszJSON(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                sw.WriteLine(json.Serialize(this));
            }
        }
        /// <summary>
        /// Funkcja odczytująca dane pracownika z pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        /// <returns></returns>
        public static Pracownik OdczytajJSON(string nazwa)
        {
            if (!File.Exists(nazwa))
                return null;
            using (StreamReader sw = new StreamReader(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                string z = sw.ReadToEnd();
                return (Pracownik)json.Deserialize(z, typeof(Pracownik));
            }
        }
        #endregion
        /// <summary>
        /// Funkcja tworzy i zwraca napis z danymi pracownika.
        /// </summary>
        /// <returns>Napis zawierający dane pracownika</returns>
        public override string ToString()
        {
            return $"({DataRozpoczeciaPracy.ToString("dd-MM-yyyy")}) "+base.ToString();
        }

    }
}
