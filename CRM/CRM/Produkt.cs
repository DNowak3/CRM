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
    public enum Jednostki { szt, kg, m, l }
    [Serializable]
    public class Produkt : IComparable<Produkt>, ICloneable, IZapisywalna
    {
        static int _aktualnyNumer;
        string _nazwa;
        double _cena;
        string _kod;
        Jednostki _jednostka;

        #region Właściwości

        public string Nazwa { get => _nazwa; set => _nazwa = value; }
        public double Cena { get => _cena; set => _cena = value; }
        public Jednostki Jednostka { get => _jednostka; set => _jednostka = value; }
        public string Kod { get => _kod; set => _kod = value; }
        #endregion

        #region Konstruktory
        static Produkt()
        {
            _aktualnyNumer = 0;
        }
        public Produkt()
        {
            Nazwa = string.Empty;
            _kod = string.Empty;
            Cena = 0;
            Jednostka = Jednostki.szt;
        }
        public Produkt(string nazwa, double cena) : this()
        {
            Nazwa = nazwa;
            Cena = cena;
            _aktualnyNumer++;
            _kod = $"{Nazwa[0]}{DateTime.Now.Year}{_aktualnyNumer}";
        }

        public Produkt(string nazwa, double cena, Jednostki jednostka) : this(nazwa, cena)
        {
            Jednostka = jednostka;
        }
        #endregion
        #region Funkcje

        public override string ToString()
        {
            return $"{Kod} {Nazwa} {Cena:C} za {Jednostka}";
        }

        public int CompareTo(Produkt other)
        {
            return Kod.CompareTo(other.Kod);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
        #region Zapis i Odczyt
        public virtual void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Produkt));
                xml.Serialize(sw, this);
            }
        }
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

        public void ZapiszJSON(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                sw.WriteLine(json.Serialize(this));
            }
        }
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
