using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CRM
{
    [Serializable]
    public class Umowa : ITworzyUmowy, ICloneable
    {
        string _nrUmowy;
        DateTime _dataUmowy;
        Pracownik _pracownikOdp;
        static int _aktualnyNumer;
        [NonSerialized]
        Dictionary<Produkt, double> _kupioneProdukty;
        List<Produkt> _listaKupionychProduktow;
        List<double> _ilosciKupionychProduktow;

        #region Właściwości
        public string NrUmowy { get => _nrUmowy; set => _nrUmowy = value; }
        public DateTime DataUmowy { get => _dataUmowy; set => _dataUmowy = value; }
        public Pracownik PracownikOdp { get => _pracownikOdp; set => _pracownikOdp = value; }
        
        public Dictionary<Produkt, double> KupioneProdukty { get => _kupioneProdukty; set => _kupioneProdukty = value; }
        public List<Produkt> ListaKupionychProduktow { get => _listaKupionychProduktow; set => _listaKupionychProduktow = value; }
        public List<double> IlosciKupionychProduktow { get => _ilosciKupionychProduktow; set => _ilosciKupionychProduktow = value; }
        #endregion

        #region Konstruktory
        static Umowa()
        {
            _aktualnyNumer = 200;
        }
        public Umowa()
        {
            KupioneProdukty = new Dictionary<Produkt, double>();
            ListaKupionychProduktow = new List<Produkt>();
            IlosciKupionychProduktow = new List<double>();
            _nrUmowy = string.Empty;
            DataUmowy = DateTime.Today;
            PracownikOdp = null;
        }
        public Umowa(Pracownik pracownik) : this()
        {
            _aktualnyNumer++;
            _nrUmowy = $"U/{DataUmowy.ToShortDateString()}/{_aktualnyNumer}";
            PracownikOdp = pracownik;
        }
        public Umowa(Pracownik pracownik, string data) : this(pracownik)
        {
            DateTime.TryParseExact(data, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime dd);
            DataUmowy = dd;
        }

        #endregion
        #region Funkcje
        public double Koszt()
        {
            double koszt = 0;
            foreach (Produkt p in KupioneProdukty.Keys)
            {
                KupioneProdukty.TryGetValue(p, out double ilosc);
                koszt += p.Cena * ilosc;
            }
            return koszt;
        }
        public Produkt ZnajdzProdukt(string kod)
        {
            foreach (Produkt p in KupioneProdukty.Keys)
            {
                if (p.Kod.Equals(kod))
                {
                    return p;
                }
            }
            throw new ProductNotFoundException();
        }
        public double JesliSztuki(Produkt produkt, double ilosc)
        {
            if (produkt.Jednostka == Jednostki.szt)
            {
                return Math.Floor(ilosc);
            }
            return ilosc;
        }
        #endregion
        #region Funkcje Dodawania
        public void DodajProdukt(Produkt produkt)
        {
            if (!KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty.Add(produkt, 1);
            }
            else
            {
                KupioneProdukty.TryGetValue(produkt, out double staraIlosc);
                KupioneProdukty[produkt] = staraIlosc + 1;
            }
        }

        public void DodajProdukt(Produkt produkt, double ilosc)
        {
            ilosc = JesliSztuki(produkt, ilosc);
            if (!KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty.Add(produkt, ilosc);
            }
            else
            {
                KupioneProdukty.TryGetValue(produkt, out double staraIlosc);
                KupioneProdukty[produkt] = staraIlosc + ilosc;
            }
        }
        #endregion
        #region Funkcje Usuwania
        public bool UsunProdukt(Produkt produkt)
        {
            if (KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty.Remove(produkt);
                return true;
            }
            return false;
        }
        public bool UsunProduktKod(string kod)
        {
            try
            {
                Produkt produkt = ZnajdzProdukt(kod);
                KupioneProdukty.Remove(produkt);
                return true;
            }
            catch (ProductNotFoundException)
            {
                return false;
            }
        }
        #endregion
        #region Funkcje Zmiany ilości
        public bool ZmienIlosc(Produkt produkt, double ilosc)
        {
            if (KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty[produkt] = ilosc;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ZmienIloscKod(string kod, double ilosc)
        {
            try
            {
                Produkt produkt = ZnajdzProdukt(kod);
                KupioneProdukty[produkt] = ilosc;
                return true;
            }
            catch (ProductNotFoundException)
            {
                return false;
            }
        }
        #endregion
        #region Klonowanie i Wypisywanie


        public override string ToString()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine($"Umowa nr {NrUmowy} zawarta w dniu: {DataUmowy.ToShortDateString()} przez {PracownikOdp.Imie} {PracownikOdp.Nazwisko} o wartości {Koszt():C}");
            foreach (Produkt p in KupioneProdukty.Keys)
            {
                KupioneProdukty.TryGetValue(p, out double ilosc);
                napis.AppendLine($"Produkt: {p.ToString()} Ilość: {ilosc}");
            }
            return napis.ToString();

        }

        public object Clone()
        {
            Umowa nowaumowa = new Umowa((Pracownik)PracownikOdp.Clone());
            nowaumowa.DataUmowy = DataUmowy;
            foreach (Produkt p in KupioneProdukty.Keys)
            {
                KupioneProdukty.TryGetValue(p, out double ilosc);
                nowaumowa.KupioneProdukty.Add((Produkt)p.Clone(), ilosc);
            }
            return nowaumowa;
        }
        #endregion
        #region Zapis i Odczyt

        public void ZapiszBin(string nazwa)
        {
            using (FileStream fs = new FileStream(nazwa, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }
        }
        public static Umowa OdczytajBin(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (FileStream fs = new FileStream(nazwa, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Umowa)formatter.Deserialize(fs);
            }
        }

        #endregion

    }
}
