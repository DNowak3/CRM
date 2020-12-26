using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CRM
{
    /// <summary>
    /// Typ wyliczeniowy, zawiera rodzaje branży w których specjalizują się organizacje
    /// </summary>
    public enum Branże { IT, Finanse, Elektronika, Telekomunikacja, Motoryzacja, Media, Energetyka, Inne }
    /// <summary>
    /// Klasa abstrakcyjna, po której dziedziczą klasy takiej jak Konkurent, Klient
    /// </summary>
    public abstract class Organizacja : IComparable<Organizacja>, IEquatable<Organizacja>, ICloneable, IZapisywalna
    {
        /// <summary>
        /// Wyrażenie regularne wskazujące na poprawny format numeru NIP
        /// </summary>
        Regex wzorNip = new Regex(@"^\d{3}-\d{3}-\d{2}-\d{2}$");


        string nazwa;
        Branże branza;
        string nip;
        DateTime dataZalozenia;
        string kraj;
        string miasto;
        string adres;
        string kodPocztowy;
        string notatki;

        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: nip
        /// </summary>
        public string Nip
        {
            get
            {
                return nip;
            }
            set
            {
                if (wzorNip.IsMatch(value))
                    nip = value;
                else
                    nip = "000-000-00-00";
            }
        }  
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: nazwa
        /// </summary>
        public string Nazwa { get => nazwa; set => nazwa = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: branza
        /// </summary>
        public Branże Branza { get => branza; set => branza = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: kraj
        /// </summary>
        public string Kraj { get => kraj; set => kraj = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: miasto
        /// </summary>
        public string Miasto { get => miasto; set => miasto = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: adres
        /// </summary>
        public string Adres { get => adres; set => adres = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: kodPocztowy
        /// </summary>
        public string KodPocztowy { get => kodPocztowy; set => kodPocztowy = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: notatki
        /// </summary>
        public string Notatki { get => notatki; set => notatki = value; }
        /// <summary>
        /// Metoda akcesorowa do prywatnego pola: dataZalozenia
        /// </summary>
        public DateTime DataZalozenia { get => dataZalozenia; set => dataZalozenia = value; }

        /// <summary>
        /// Konstruktor domyślny klasy
        /// </summary>
        public Organizacja()
        {
            Nazwa = string.Empty;
            Branza = Branże.Inne;
            Adres = string.Empty;
            KodPocztowy = string.Empty;
            Notatki = string.Empty;
        }

        /// <summary>
        /// Konstruktor parametryczny klasy dziedziczący po konstruktorze bazowym
        /// </summary>
        /// <param name="nazwa"> nazwa organizacji </param>
        /// <param name="branza"> nazwa branzy organizacji </param>
        public Organizacja(string nazwa, Branże branza) : this()
        {
            Nazwa = nazwa;
            Branza = branza;
        }

        /// <summary>
        /// Konstruktor z większą liczbą parametrów, dziedziczy po poprzednim konstruktorze parametrycznym
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="branza"></param>
        /// <param name="nip"></param>
        /// <param name="kraj"></param>
        /// <param name="miasto"></param>
        /// <param name="dataZalozenia"></param>
        public Organizacja(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia) : this(nazwa, branza)
        {
            Nip = nip;
            Kraj = kraj;
            Miasto = miasto;
            string[] formatDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DataZalozenia = DateTime.ParseExact(dataZalozenia, formatDaty, null, System.Globalization.DateTimeStyles.None);
        }

        /// <summary>
        /// Metoda porównująca obiekty klas dziedziczących po klasie Organizacja po nazwie (alfabetycznie)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Organizacja other) 
        {
            return Nazwa.CompareTo(other.Nazwa);
        }

        /// <summary>
        /// Metoda sprawdzająca czy dwie organizacje "są równe" - przyjmujemy że organizacje są równe gdy posiadają
        /// takie same nazwy, branże i adresy NIP
        /// </summary>
        /// <param name="other"> parametrem jest inny obiekt tej samej klasy </param>
        /// <returns></returns>
        public bool Equals(Organizacja other)      
        {
            if (Nazwa.Equals(other.Nazwa) && Branza.Equals(other.Branza) && Nip.Equals(other.Nazwa))
                return true;
            return false;
        }

        /// <summary>
        /// Metoda, która umożliwia utworzenie kopii obiektu
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Metoda umożliwiająca zapis danych do pliku XML - nadpisywana w klasie Konkurent
        /// </summary>
        /// <param name="nazwa"> parametr jest nazwą pliku xml </param>
        public virtual void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Organizacja));
                xml.Serialize(sw, this);
            }
        }

        public virtual void ZapiszJSON(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                sw.WriteLine(json.Serialize(this));
            }
        }

        /// <summary>
        /// Tworzy czytelny opis obiektu organizacja, metoda nadpisywana zostaje w klasie Konkurent
        /// </summary>
        /// <returns> Zwraca opis obiektu </returns>
        public override string ToString()
        {
            return $"{Nazwa.ToUpper()} ({Branza}) {Nip} {Kraj}: {Miasto}, data założenia: {DataZalozenia.ToShortDateString()}";
        }
    }
}
