using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("TestyJednostkowe")]
namespace CRM
{
    
    [Serializable]
    ///<summary>
    ///Klasa definiująca umowę.
    ///</summary>
    public class Umowa : ITworzyUmowy, ICloneable
    {
        /// <summary>
        /// Numer umowy
        /// </summary>
        string _nrUmowy;
        /// <summary>
        /// Data zawarcia umowy
        /// </summary>
        DateTime _dataUmowy;
        /// <summary>
        /// Pracownik organizacji prowadzącej CRM, który zawarł umowę.
        /// </summary>
        Pracownik _pracownikOdp;
        /// <summary>
        /// Aktualny numer umowy
        /// </summary>
        static int _aktualnyNumer;
        /// <summary>
        /// Słownik zawierający informacje o kupionych produktach i zakupionej ilości każdego z nich.
        /// </summary>
        Dictionary<Produkt, double> _kupioneProdukty;
        /// <summary>
        /// Lista kupionych produktów generowana na podstawie słownika KupioneProdukty
        /// </summary>
        List<Produkt> _listaKupionychProduktow;
        /// <summary>
        /// Lista ilości zakupionych produktów generowana na podstawie słownika KupioneProdukty
        /// </summary>
        List<double> _ilosciKupionychProduktow;

        #region Właściwości
        /// <summary>
        /// Właściwości
        /// </summary>
        public string NrUmowy { get => _nrUmowy; set => _nrUmowy = value; }
        public DateTime DataUmowy { get => _dataUmowy; set => _dataUmowy = value; }
        public Pracownik PracownikOdp { get => _pracownikOdp; set => _pracownikOdp = value; }               
        internal Dictionary<Produkt, double> KupioneProdukty { get => _kupioneProdukty; set => _kupioneProdukty = value; }
        public List<Produkt> ListaKupionychProduktow { get => _listaKupionychProduktow; set => _listaKupionychProduktow = value; }
        public List<double> IlosciKupionychProduktow { get => _ilosciKupionychProduktow; set => _ilosciKupionychProduktow = value; }
        public static int AktualnyNumer { get => _aktualnyNumer; }
        #endregion

        #region Konstruktory
        /// <summary>
        /// Konstruktor statyczny ustawiający początkową wartość aktulnego numeru umowy.
        /// </summary>
        static Umowa()
        {
            _aktualnyNumer = 200;
        }
        /// <summary>
        /// Podstawowy konstruktor umowy. Ustawia numer umowy i PracownkaOdp na puste wartości, a datę zawarcia umowy na dziś.
        /// </summary>
        public Umowa()
        {
            _aktualnyNumer++;
            KupioneProdukty = new Dictionary<Produkt, double>();
            ListaKupionychProduktow = new List<Produkt>();
            IlosciKupionychProduktow = new List<double>();
            _nrUmowy = string.Empty;
            DataUmowy = DateTime.Today;
            PracownikOdp = null;
        }
        /// <summary>
        /// Konstruktor parametryczny Umowy. Ustawia numer umowy oraz PracownikaOdp
        /// </summary>
        /// <param name="pracownik">Pracownik z organizacji prowadzącej CRM odpowiedzialny za podpisanie umowy</param>
        public Umowa(Pracownik pracownik) : this()
        {
            _nrUmowy = $"U/{DataUmowy.ToShortDateString()}/{AktualnyNumer}";
            PracownikOdp = pracownik;
        }
        /// <summary>
        /// Konstruktor parametryczny Umowy. Ustawia datę zawarcia umowy oraz PracownikOdp
        /// </summary>
        /// <param name="pracownik">Pracownik z organizacji prowadzącej CRM odpowiedzialny za podpisanie umowy</param>
        /// <param name="data">Data zawarcia umowy</param>
        public Umowa(Pracownik pracownik, string data) : this(pracownik)
        {
            DateTime.TryParseExact(data, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime dd);
            DataUmowy = dd;
            _nrUmowy = $"U/{DataUmowy.ToShortDateString()}/{AktualnyNumer}";
        }

        #endregion
        #region Funkcje
        /// <summary>
        /// Funkcja zliczająca całkowity koszt wszystkich zakupionych produktów w umowie.
        /// </summary>
        /// <returns>Koszt wszystkich zakupionych produktów w umowie</returns>
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
        /// <summary>
        /// Funkcja wyszukująca produkt w umowie po kodzie. Jeśli nie ma produktu o takim kodzie w umowie, funkcja zwraca ProductNotFoundException.
        /// </summary>
        /// <param name="kod">Kod szukanego produktu</param>
        /// <returns>Produkt o podanym kodzie lub wyjątek ProductNotFoundException</returns>
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
        /// <summary>
        /// Funkcja, która sprawdza czy jednostka miary produktu to szuka i jeśli tak - ilość zostaje zaokrąglona w dół do wartości całkowitych.
        /// </summary>
        /// <param name="produkt">Wybrany produkt dla którego sprawdzana jest jednostka</param>
        /// <param name="ilosc">Ilość wybranego produktu wprowadzona przez użytkownika</param>
        /// <returns>Wartość ilości produktu. Jeśli produkt liczony jest w sztukach, zwracana jest wartość ilości zaokrąlona w dół do najbliższej liczby całkowitej</returns>
        public double JesliSztuki(Produkt produkt, double ilosc)
        {
            if (produkt.Jednostka == Jednostki.szt)
            {
                return Math.Floor(ilosc);
            }
            return ilosc;
        }
        /// <summary>
        /// Funkcja zapisująca obiekty ze slownika KupioneProdukty do odpowiednich list
        /// </summary>
        public void SlownikDoListy()
        {
            ListaKupionychProduktow.Clear();
            IlosciKupionychProduktow.Clear();
            foreach (Produkt key in KupioneProdukty.Keys)
            {
                if(!ListaKupionychProduktow.Contains(key))
                {
                    ListaKupionychProduktow.Add(key);
                    IlosciKupionychProduktow.Add(KupioneProdukty[key]);
                }
            }
        }
        #endregion
        #region Funkcje Dodawania
        /// <summary>
        /// Dodawanie 1 produktu do umowy. Jeśli produkt już jest w umowie, dodaje się do niego jedną jednostkę.
        /// </summary>
        /// <param name="produkt">Dodawany produkt</param>
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
        /// <summary>
        /// Dodawanie produktu do umowy wraz z ilością. Jeśli produkt już jest w umowie, dodaje się do istniejącej ilości dodatkowe jednostki nowej ilości.
        /// </summary>
        /// <param name="produkt">Dodawany produkt</param>
        /// <param name="ilosc">Ilość dodawanego produktu</param>

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
        /// <summary>
        /// Usuwanie produktu z umowy.
        /// </summary>
        /// <param name="produkt">Usuwany produkt.</param>
        /// <returns>True jeśli produkt był w umowie i został usunięty. W przeciwnym wypadku - false</returns>
        public bool UsunProdukt(Produkt produkt)
        {
            if (KupioneProdukty.ContainsKey(produkt))
            {
                KupioneProdukty.Remove(produkt);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Usuwanie produktu z umowy wg kodu produktu.
        /// </summary>
        /// <param name="kod">Kod usuwanego produktu</param>
        /// <returns>True jeśli produkt był w umowie i został usunięty. W przeciwnym wypadku - false</returns>
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
        /// <summary>
        /// Zmiana ilości produktu będącego w umowie.
        /// </summary>
        /// <param name="produkt">Wybrany produkt</param>
        /// <param name="ilosc">Nowa ilość produktu</param>
        /// <returns>True jeśli produkt był w umowie i zmieniono jego ilość na nową, w przeciwnym wypadku - false</returns>
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
        /// <summary>
        /// Zmiana ilości produktu będącego w umowie wg kodu.
        /// </summary>
        /// <param name="kod">Kod wybranego produktu</param>
        /// <param name="ilosc">Nowa ilość produktu</param>
        /// <returns>True jeśli produkt był w umowie i zmieniono jego ilość na nową, w przeciwnym wypadku - false</returns>
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

        /// <summary>
        /// Funkcja wypisuje informacje na temat umowy wraz z wszystkimi zakupionymi produktami i ilościami
        /// </summary>
        /// <returns>Zwraca napis z informacją o umowie</returns>
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
        /// <summary>
        /// Funkcja tworzy kopię bieżącej umowy.
        /// </summary>
        /// <returns>Nowy obiekt klasy Umowa, będący kopią bieżącego</returns>
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
        /// <summary>
        /// Zapis do pliku binarnego umowy
        /// </summary>
        /// <param name="nazwa">Nazwa pliku BIN</param>
        public void ZapiszBin(string nazwa)
        {
            using (FileStream fs = new FileStream(nazwa, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
            }
        }
        /// <summary>
        /// Odczyt z pliku binarnego umowy
        /// </summary>
        /// <param name="nazwa">Nazwa pliku BIN</param>
        /// <returns>Zwraca odczytany plik jako obiekt klasy Umowa</returns>
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
