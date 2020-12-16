using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class Umowa :ITworzyUmowy
    {
        string _nrUmowy;
        DateTime _dataUmowy;
        Pracownik _pracownikOdp;
        static int _aktualnyNumer;
        Dictionary<Produkt, double> _kupioneProdukty;
        

        public string NrUmowy { get => _nrUmowy; }
        public DateTime DataUmowy { get => _dataUmowy; set => _dataUmowy = value; }
        public Pracownik PracownikOdp { get => _pracownikOdp; set => _pracownikOdp = value; }
        internal Dictionary<Produkt, double> KupioneProdukty { get => _kupioneProdukty; set => _kupioneProdukty = value; }

        static Umowa()
        {
            _aktualnyNumer = 200;
        }
        public Umowa()
        {
            KupioneProdukty = new Dictionary<Produkt, double>();
            _nrUmowy = string.Empty;
            DataUmowy = DateTime.Today;
            PracownikOdp = null;
        }
        public Umowa(Pracownik pracownik) :this()
        {
            _aktualnyNumer++;
            _nrUmowy = $"U/{DataUmowy.ToShortDateString()}/{_aktualnyNumer}";
            PracownikOdp = pracownik;
        }
        public Umowa(Pracownik pracownik, string data) :this(pracownik)
        {
            DateTime.TryParseExact(data, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime dd);
            DataUmowy = dd;
        }

        public double Koszt()
        {
            double koszt = 0;
            foreach (Produkt p in KupioneProdukty.Keys)
            {
                KupioneProdukty.TryGetValue(p, out double ilosc);
                koszt += p.Cena*ilosc;
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
            if(produkt.Jednostka==Jednostki.szt)
            {
                return Math.Floor(ilosc);
            }
            return ilosc;
        }
                
        public void DodajProdukt(Produkt produkt, double ilosc)
        {
            ilosc = JesliSztuki(produkt, ilosc);
            if(!KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty.Add(produkt, ilosc);
            }
            else
            {
                KupioneProdukty.TryGetValue(produkt, out double staraIlosc);
                KupioneProdukty[produkt] = staraIlosc + ilosc;
            }
        }
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
            catch(ProductNotFoundException)
            {
                return false;
            }
        }
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
    }
}
