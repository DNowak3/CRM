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
    ///<summary>
    ///Typ wyliczeniowy. Zawiera podstawowe jednostki miary produktów.
    ///</summary>
    public enum Jednostki { szt, kg, m, l }
    [Serializable]
    ///<summary>
    ///Klasa definiująca produkt.
    ///</summary>
    public class Produkt : IComparable<Produkt>, ICloneable, IZapisywalna
    {
        /// <summary>
        /// Zmienna statyczna przechowująca informację o ostatnim aktualnym numerze produktu.
        /// </summary>
        static int _aktualnyNumer;
        /// <summary>
        /// Nazwa produktu.
        /// </summary>
        string _nazwa;
        /// <summary>
        /// Cena produktu.
        /// </summary>
        double _cena;
        /// <summary>
        /// Kod produktu.
        /// </summary>
        string _kod;
        /// <summary>
        /// Jednostka miary produktu.
        /// </summary>
        Jednostki _jednostka;

        #region Właściwości
        /// <summary>
        /// Właściwości
        /// </summary>
        public string Nazwa { get => _nazwa; set => _nazwa = value; }
        public double Cena { get => _cena; set => _cena = value; }
        public Jednostki Jednostka { get => _jednostka; set => _jednostka = value; }
        public string Kod { get => _kod; set => _kod = value; }
        public static int AktualnyNumer { get => _aktualnyNumer; }
        #endregion

        #region Konstruktory
        /// <summary>
        /// Konstruktor statyczny wywołujący początkową wartość aktualnego numeru.
        /// </summary>
        static Produkt()
        {
            _aktualnyNumer = 0;
        }
        /// <summary>
        /// Podstawowy konstruktor produktu. Ustawia nazwę i kod na puste wartości, cenę na zero, a jednostkę domyślnie na sztuki.
        /// </summary>
        public Produkt()
        {
            _aktualnyNumer++;
            Nazwa = string.Empty;
            _kod = string.Empty;
            Cena = 0;
            Jednostka = Jednostki.szt;
        }
        /// <summary>
        /// Konstruktor parametryczny produktu. Ustawia cenę i nazwę na podstawie wartości podanych przez użytkownika.
        /// </summary>
        /// <param name="nazwa">Nazwa podana przez użytkownika</param>
        /// <param name="cena">Cena podana przez użytkownika</param>
        public Produkt(string nazwa, double cena) : this()
        {
            Nazwa = nazwa;
            Cena = Math.Abs(cena);
            _kod = $"{Nazwa[0]}{DateTime.Now.Year}{AktualnyNumer}";
        }
        /// <summary>
        /// Konstruktor parametryczny produktu. Ustawia cenę, nazwę i jednostkę miary na podstawie wartości podanych przez użytkownika.
        /// </summary>
        /// <param name="nazwa">Nazwa podana przez użytkownika</param>
        /// <param name="cena">Cena podana przez użytkownika</param>
        /// <param name="jednostka">Jednostka podana przez użytkownika</param>

        public Produkt(string nazwa, double cena, Jednostki jednostka) : this(nazwa, cena)
        {
            Jednostka = jednostka;
        }
        #endregion
        #region Funkcje
        /// <summary>
        /// Tworzy i zwraca napis zawierający informacje o produkcie.
        /// </summary>
        /// <returns>Napis z informacjami o produkcie</returns>
        public override string ToString()
        {
            return $"{Kod} {Nazwa} {Cena:C} za {Jednostka}";
        }
        /// <summary>
        /// Porównuje dwa produkty na podstawie kodu.
        /// </summary>
        /// <param name="other">Produkt, z którym porównujemy.</param>
        /// <returns>Zwraca wartość porównania kodów produktów:
        /// 0 jeżeli kody są równe
        /// mniej niż 0 jeżeli kod bieżącego produktu jest mniejszy niż kod produktu, z którym prównujemy
        /// więcej niż 0 jeżeli kod bieżącego produktu jest większy niż kod produktu, z którym prównujemy
        ///  </returns>
        public int CompareTo(Produkt other)
        {
            return Kod.CompareTo(other.Kod);
        }
        /// <summary>
        /// Metoda tworzy obiekt klasy Produkt bedacy kopia biezacego produktu.
        /// </summary>
        /// <returns>Zwraca nowy produkt będący kopią bieżącego produktu</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
        #region Zapis i Odczyt
        /// <summary>
        /// Zapisuje informacje o produkcie do pliku XML
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        public virtual void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Produkt));
                xml.Serialize(sw, this);
            }
        }
        /// <summary>
        /// Odczytuje informacje o produkcie z pliku XML
        /// </summary>
        /// <param name="nazwa">Nazwa pliku XML</param>
        /// <returns>Zwraca odczytany plik jako obiekt klasy Produkt</returns>
        public static Produkt OdczytajXML(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Produkt));
                return (Produkt)xml.Deserialize(sr);
            }
        }
        /// <summary>
        /// Zapisuje informacje o produkcie do pliku JSON
        /// </summary>
        /// <param name="nazwa">Nazwa pliku JSON</param>
        public void ZapiszJSON(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                sw.WriteLine(json.Serialize(this));
            }
        }
        /// <summary>
        /// Odczytuje informacje o produkcie z pliku JSON
        /// </summary>
        /// <param name="nazwa">Nazwa pliku JSON</param>
        /// <returns>Zwraca odczytany plik jako obiekt klasy Produkt</returns>
        public static Produkt OdczytajJSON(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sw = new StreamReader(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                string z = sw.ReadToEnd();
                return (Produkt)json.Deserialize(z, typeof(Produkt));
            }
        }
        #endregion
    }
}
