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
    /// Klasa dziedzicząca po klasie Organizacja, poza polami i metodami z klasy Organizacja dodatkowo niesie
    /// informację o stopniu zagrożenia ze strony konkretnego konkurenta
    /// </summary>
    public class Konkurent : Organizacja
    {
        /// <summary>
        /// Typ wyliczeniowy, zawiera informację o stopniu zagrożenia
        /// </summary>
        public enum StopienZagrozenia { Niski, BardzoNiski, Średni, Wysoki, BardzoWysoki }
        StopienZagrozenia zagrozenie;
        public StopienZagrozenia Zagrozenie { get => zagrozenie; set => zagrozenie = value; }
        /// <summary>
        /// Konstruktor domyślny, dziedziczący po konstruktorze domyślnym z klasy bazowej
        /// </summary>
        public Konkurent() : base() 
        {
            Zagrozenie = StopienZagrozenia.Średni;
        }
        /// <summary>
        /// Konstruktor parametryczny, dziedziczący po konstruktorze z dwoma parametrami z klasy bazowej
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="branza"></param>
        public Konkurent(string nazwa, Branże branza) : base(nazwa, branza) { }
        /// <summary>
        /// Konstruktor parametryczny, dziedziczący po kosntruktorze z dwoma parametrami z klasy bazowej
        /// </summary>
        /// <param name="nazwa"></param>
        /// <param name="branza"></param>
        /// <param name="nip"></param>
        /// <param name="kraj"></param>
        /// <param name="miasto"></param>
        /// <param name="dataZalozenia"></param>
        /// <param name="zagrozenie"></param>
        public Konkurent(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia, StopienZagrozenia zagrozenie) : base(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            Zagrozenie = zagrozenie;
        }
        /// <summary>
        /// Metoda, która korzystając z funkcji Equals zdefiniowanej z klasie Organizacja, sprawdza, czy dwa obiekty
        /// klasy Konkurent (dwie organizacje będące konkurentami) są równe - czy to ta sama firma
        /// </summary>
        /// <param name="k1"> pierwszy obiekt klasy Konkurent </param>
        /// <param name="k2"> drugi obiekt klasy Konkurent </param>
        /// <returns></returns>
        public static bool CzyToTeSameFirmy(Konkurent k1, Konkurent k2)
        {
            if (k1.Equals(k2))
                return true;
            return false;
        }
        /// <summary>
        /// Metoda  umożliwiająca zapis obiektu do pliku xml
        /// </summary>
        /// <param name="nazwa"> parametr jest nazwą pliku xml </param>
        public override void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Konkurent));
                xml.Serialize(sw, this);
            }
        }
        /// <summary>
        /// Metoda pozwalająca na odczyt danych z pliku xml o podanej nazwie
        /// </summary>
        /// <param name="nazwa"> parametr jest nazwą pliku xml </param>
        /// <returns></returns>
        public static Konkurent OdczytajXML(string nazwa) 
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Konkurent));
                return (Konkurent)xml.Deserialize(sr);
            }
        }
        /// <summary>
        /// Metoda zapisująca dane o konkurencie w pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        public override void ZapiszJSON(string nazwa)
        {
            base.ZapiszJSON(nazwa);
        }
        /// <summary>
        /// Metoda odczytująca dane o konkurencie z pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        /// <returns></returns>
        public static Konkurent OdczytajJSON(string nazwa)
        {
            if (!File.Exists(nazwa))
                return null;
            using (StreamReader sw = new StreamReader(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                string z = sw.ReadToEnd();
                return (Konkurent)json.Deserialize(z, typeof(Konkurent));
            }
        }
        /// <summary>
        /// Metoda klonująca obiekt klasy Konkurent
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            Konkurent klonowany = (Konkurent)base.Clone();
            klonowany.Zagrozenie = this.zagrozenie;
            return klonowany;
        }
        /// <summary>
        /// Metoda nadpisywana z klasy Organizacja, wypisuje czytelny opis obiektów klasy Konkurent
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + $" (stopień zagrożenia: {Zagrozenie})";
        }
    }
}
