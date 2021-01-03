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
    /// Enumeracja, zawiera mozliwe wartosci statusu klienta.
    /// </summary>
    public enum Status { potencjalny, nowy, stały, były }


    /// <summary>
    /// Klasa definiujaca klientow firmy.
    /// </summary>
    [Serializable]
    public class Klient : Organizacja, IZapisywalna, ICloneable
    {
        /// <summary>
        /// Lista osob w firmie, z ktorymi mozemy sie kontaktowac.
        /// </summary>
        List<OsobaKontakt> _listaKontaktow;
        /// <summary>
        /// Data planowanego kontaktu.
        /// </summary>
        DateTime _dataPlanowanegoKontaktu;
        /// <summary>
        /// Aktualny status klienta.
        /// </summary>
        Status _status;
        /// <summary>
        /// Historia kontaktow i dzialan podjetych wobec klienta.
        /// </summary>
        Stack<Dzialanie> _dzialania;
        /// <summary>
        /// Transakcje zawarte z klientem.
        /// </summary>
        Stack<Umowa> _transakcje;
        /// <summary>
        /// Uwagi dotyczace klienta.
        /// </summary>
        string _uwagi;
        /// <summary>
        /// Zmienna pomocnicza, do serializacji transakcji.
        /// </summary>
        List<Umowa> _transakcjeList;
        /// <summary>
        /// Zmienna pomocnicza do serializacji dzialan.
        /// </summary>
        List<Dzialanie> _dzialaniaList;

        #region konstruktory i właściwości
        /// <summary>
        /// Podstawowy konstruktor nieparametryczny.
        /// </summary>
        public Klient()
        {
            _listaKontaktow = new List<OsobaKontakt>();
            _dzialania = new Stack<Dzialanie>();
            _transakcje = new Stack<Umowa>();
            _transakcjeList = new List<Umowa>();
            _dzialaniaList = new List<Dzialanie>();
        }

        /// <summary>
        /// Ogolny konsruktor parametryczny odwolujacy sie do konstruktora klasy bazowej.
        /// </summary>
        /// <param name="nazwa">Nazwa firmy, bedacej naszym klientem</param>
        /// <param name="branza">Branza w jakiej dziala firma</param>
        public Klient(string nazwa, Branże branza) : base(nazwa, branza)
        {
            _listaKontaktow = new List<OsobaKontakt>();
            _dzialania = new Stack<Dzialanie>();
            _transakcje = new Stack<Umowa>();
            _transakcjeList = new List<Umowa>();
            _dzialaniaList = new List<Dzialanie>();
        }

        /// <summary>
        /// Bardziej szczegolowy konsruktor parametryczny odwolujacy sie do konstruktora klasy bazowej.
        /// </summary>
        /// <param name="nazwa">Nazwa firmy, bedacej naszym klientem</param>
        /// <param name="branza">Branza w jakiej dziala firma</param>
        /// <param name="nip">NIP firmy</param>
        /// <param name="kraj">Kraj, w ktorym dzia firma</param>
        /// <param name="miasto">Miasto, w ktorym dzia firma</param>
        /// <param name="dataZalozenia">Data zalozenia firmy</param>
        public Klient(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia)
            : base(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            _listaKontaktow = new List<OsobaKontakt>();
            _dzialania = new Stack<Dzialanie>();
            _transakcje = new Stack<Umowa>();
            _transakcjeList = new List<Umowa>();
            _dzialaniaList = new List<Dzialanie>();
        }


        /// <summary>
        /// Konsruktor ustawiajacy wartosci wszystkim polom, odwolujacy sie do poprzedniego konstruktora.
        /// </summary>
        /// <param name="nazwa">Nazwa firmy, bedacej naszym klientem</param>
        /// <param name="branza">Branza w jakiej dziala firma</param>
        /// <param name="nip">NIP firmy</param>
        /// <param name="kraj">Kraj, w ktorym dzia firma</param>
        /// <param name="miasto">Miasto, w ktorym dzia firma</param>
        /// <param name="dataZalozenia">Data zalozenia firmy</param>
        /// <param name="dataPlanowKontaktu">Planowany kontakt z klientem</param>
        /// <param name="uwagi">Uwagi dotyczace firmy</param>
        /// <param name="status">Aktualny status klienta</param>
        public Klient(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia,
            string dataPlanowKontaktu, string uwagi, Status status = Status.potencjalny)
            : this(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(dataPlanowKontaktu, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            _uwagi = uwagi;
            _dataPlanowanegoKontaktu = d;
            _status = status;
        }

        /// <summary>
        /// Wlasciwosci.
        /// </summary>
        public DateTime DataPlanowanegoKontaktu { get => _dataPlanowanegoKontaktu; set => _dataPlanowanegoKontaktu = value; }
        public string Uwagi { get => _uwagi; set => _uwagi = value; }
        public Status Status { get => _status; set => _status = value; }
        public List<OsobaKontakt> ListaKontaktow { get => _listaKontaktow; }
        public List<Umowa> TransakcjeList { get => _transakcjeList; set => _transakcjeList = value; }
        public List<Dzialanie> DzialaniaList { get => _dzialaniaList; set => _dzialaniaList = value; }
        #endregion

        #region Lista działań
        /// <summary>
        /// Dodaje dzialanie do listy dzialan.
        /// </summary>
        /// <param name="dzialanie">Dzialanie, ktore ma sie znalezc na liscie dzialan</param>
        public void DodajDzialanie(Dzialanie dzialanie)
        {
            _dzialania.Push(dzialanie);
        }

        /// <summary>
        /// Metoda szukajaca ostatniego wystapienia dzialania o podanej nazwie
        /// </summary>
        /// <param name="nazwa">Nazwa szukanego dzialania</param>
        /// <returns>
        /// Zwraca pierwsze wystapienie dzialania o podanej nazwie.
        /// Jesli dzialania o podanej nazwie nie ma na liscie, wyrzuca wyjatek.
        /// </returns>
        public Dzialanie ZnajdzDzialanie(string nazwa)
        {
            foreach (Dzialanie d in _dzialania)
            {
                if (d.Nazwa.ToLower() == nazwa.ToLower())
                {
                    return d;
                }
            }
            throw new ActionNotFoundException();
        }

        /// <summary>
        /// Oblicza ile dzialan ogolem podjeto wobec klienta.
        /// </summary>
        /// <returns>Zwraca liczbe dzialan podjetych wobec klienta</returns>
        public int IleDzialan()
        {
            return _dzialania.Count();
        }
        /// <summary>
        /// Wyszukuje date ostatniego kontaktu z klientem
        /// </summary>
        /// <returns>Zwraca date ostatniego kontaktu</returns>
        public DateTime OstatniKontakt()
        {
            SortujDzialania();
            return IleDzialan() == 0 ? DateTime.MinValue : _dzialania.Peek().Data;
        }
        /// <summary>
        /// Wyszukuje wszystkie dzialania od podanej daty do dzisiaj
        /// </summary>
        /// <param name="dataOd">Data, od ktorej zaczynaja byc szukane dzialania</param>
        /// <returns>Lista dzialan od podanej daty do dziś</returns>
        public List<Dzialanie> ZnajdzDzialania(string dataOd)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(dataOd, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.Data.CompareTo(d) >= 0));
            return dzialania.Count() == 0 ? null : dzialania;
        }
        /// <summary>
        /// Wyszukuje wszytskie przyszłe działania.
        /// </summary>
        /// <returns>Zwraca listę przyszłych działań</returns>
        public List<Dzialanie> ZnajdzPlanowaneDzialania()
        {
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.Data.CompareTo(DateTime.Today) >= 0));
            return dzialania.Count() == 0 ? null : dzialania;
        }

        /// <summary>
        /// Wyszukuje wszystkie kontakty danego pracownika z klientem.
        /// </summary>
        /// <param name="pracownik">Pracownik, ktorego kontaktow z klientem szukamy</param>
        /// <returns>Lista kontaktow danego pracownika z klientem</returns>
        public List<Dzialanie> ZnajdzDzialania(Pracownik pracownik)
        {
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.Pracownik.Equals(pracownik)));
            return dzialania.Count() == 0 ? null : dzialania;
        }

        /// <summary>
        /// Usuwa podane dzialanie z listy dzialan.
        /// </summary>
        /// <param name="d">Dzialanie do usuniecia</param>
        public void UsunDzialanie(Dzialanie d)
        {
                _dzialania = new Stack<Dzialanie>(_dzialania.Where(x => x.Nazwa != d.Nazwa || !x.Data.Equals(d.Data)));
                _dzialania.Reverse();
        }

        /// <summary>
        /// Usuwa pierwsze wystapienie dzialania o podanej nazwie z listy dzialan.
        /// </summary>
        /// <param name="nazwa">Nazwa dzialania, ktore ma zostac usuniete</param>
        /// <returns>Zwraca prawde, jesli usuniecie powiodlo sie, w przeciwnym wypadku falsz</returns>
        public bool UsunDzialanie(string nazwa)
        {
            foreach (Dzialanie d in _dzialania)
            {
                if (d.Nazwa == nazwa)
                {
                    _dzialania = new Stack<Dzialanie>(_dzialania.Where(x => x.Nazwa != nazwa));
                    _dzialania.Reverse();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Usuwa wszystkie dzialania z listy dzialan.
        /// </summary>
        public void UsunWszystkieDzialania()
        {
            _dzialania.Clear();
        }

        /// <summary>
        /// Zwraca wszystkie dzialania w formie listy.
        /// </summary>
        /// <returns></returns>
        public List<Dzialanie> ZwrocDzialania()
        {
            return _dzialania.ToList();
        }
        #endregion

        /// <summary>
        /// Oblicza liczbe osob do kontaktu z klientem;
        /// </summary>
        /// <returns>Liczba osob na liscie kontaktow</returns>
        public int IleKontaktow()
        {
            return _listaKontaktow.Count();
        }

        #region Lista kontaktow
        /// <summary>
        /// Dodaje osobe do listy kontaktow z firma.
        /// </summary>
        /// <param name="o">Osoba do kontaktu</param>
        /// <returns>Zwraca prawde, jesli dodanie powiodlo sie, w przeciwnym wypadku falsz</returns>
        public void DodajKontakt(OsobaKontakt o)
        {
            _listaKontaktow.Add(o);
        }

        /// <summary>
        /// Usuwa dana osobe z listy kontaktow.
        /// </summary>
        /// <param name="o">Osoba do usuniecia z listy kontaktow</param>
        /// <returns>Zwraca prawde, jesli usuniecie powiodlo sie, w przeciwnym wypadku falsz</returns>
        public bool UsunKontakt(OsobaKontakt o)
        {
            if (_listaKontaktow.Contains(o))
            {
                _listaKontaktow.Remove(o);
                return true;
            }
            Console.WriteLine($"Osoby nie ma na liście kontaktów");
            return false;
        }

        /// <summary>
        /// Metoda usuwa wszystkie kontakty z listy kontaktow
        /// </summary>
        public void UsunWszystkieKontakty()
        {
            _listaKontaktow.Clear();
        }

        /// <summary>
        /// Metoda sparwdza czy dana osoba jest juz w liscie kontaktow.
        /// </summary>
        /// <param name="imie">Imie sprawdzanej osoby</param>
        /// <param name="nazwisko">Nazwisko sprawdzanej osoby</param>
        /// <returns>Zwraca prawde jesli osoba jest na liscie kontaktow, w przeciwnym wypadku falsz</returns>
        public bool PosiadaKontakt(string imie, string nazwisko)
        {
            foreach (OsobaKontakt o in _listaKontaktow)
            {
                if (o.Imie == imie && o.Nazwisko == nazwisko)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metoda wyszukuje osobe kotaktowa o podanym imieniu i nazwisku.
        /// </summary>
        /// <param name="imie">Imie szukanej osoby</param>
        /// <param name="nazwisko">Nazwisko szukanej osoby</param>
        /// <returns>
        /// Zwraca osobe kontaktowa o podanym imieniu i nazwisku.
        /// Jesli osoba nie zostala znaleziona zostaje wyrzucony wyjatek NonOrganizationMemberException.
        /// </returns>
        public OsobaKontakt ZwrocKontakt(string imie, string nazwisko)
        {
            foreach (OsobaKontakt o in _listaKontaktow)
            {
                if (o.Imie == imie && o.Nazwisko == nazwisko)
                {
                    return o;
                }
            }
            throw new NonOrganizationMemberException();
        }
        #endregion

        #region Lista umów
        /// <summary>
        /// Metoda dodaje umowe do listy transakcji z klientem.
        /// </summary>
        /// <param name="umowa">Umowa, ktora ma zostac dodana do listy transakcji</param>
        public void DodajTransakcje(Umowa umowa)
        {
            _transakcje.Push(umowa);
        }

        /// <summary>
        /// Metoda, ktora usuwa dana umowe z listy transakcji.
        /// </summary>
        /// <param name="numer">Numer umowy, ktora ma zostac usunieta</param>
        /// <returns>Zwraca prawde, jesli usuniecie umowy powiodlo sie, w przeciwnym wypadku falsz</returns>
        public bool UsunTransakcje(string numer) //Napisz jak będziesz znać kod do umowy
        {
            foreach (Umowa u in _transakcje)
            {
                if (u.NrUmowy == numer)
                {
                    _transakcje = new Stack<Umowa>(_transakcje.Where(x => x.NrUmowy != numer));
                    _transakcje.Reverse();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Metoda, ktora usuwa dana umowe z listy transakcji.
        /// </summary>
        /// <param name="umowa">Umowa która ma zostać usunięta</param>
        /// <returns>Zwraca prawde, jesli usuniecie umowy powiodlo sie, w przeciwnym wypadku falsz</returns>
        public bool UsunTransakcje(Umowa umowa)
        {
            foreach (Umowa u in _transakcje)
            {
                if (u.Equals(umowa))
                {
                    _transakcje = new Stack<Umowa>(_transakcje.Where(x => x != umowa));
                    _transakcje.Reverse();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metoda wyszukuje wszystkie transakcje podpisane z klientem od podanej daty do dzis
        /// </summary>
        /// <param name="data">Data, od ktorej maja zaczac byc szukane transakcje</param>
        /// <returns>Zwraca liste znalezionych traksakcji lub null jesli zadna nie zostala znaleziona</returns>
        public List<Umowa> ZnajdzTransakcje(string data)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(data, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            List<Umowa> transakcje = new List<Umowa>(_transakcje.Where(x => x.DataUmowy == d));
            return transakcje.Count() == 0 ? null : transakcje;
        }

        /// <summary>
        /// Metoda wyszukuje stransakcje o podanym numerze
        /// </summary>
        /// <param name="numer">Numer szukanej transakcji</param>
        /// <returns>Zwraca transakcje o podanym numerze</returns>
        public List<Umowa> ZwrocTransakcje(string numer)
        {
            List<Umowa> transakcje = new List<Umowa>();
            foreach (Umowa u in _transakcje)
            {
                if (u.NrUmowy == numer)
                {
                    transakcje.Add(u);
                    
                }
            }
            return transakcje;
        }
        /// <summary>
        /// Metoda wyszukująca transakcje zawarte przez podanego pracownika.
        /// </summary>
        /// <param name="p">Szukany pracownik odpowiedzialny za zawarcie umowy</param>
        /// <returns>Lista transakcji zawartych przez podanego pracownika </returns>
        public List<Umowa> ZwrocTransakcjePracownik(Pracownik p)
        {
            List<Umowa> transakcje = new List<Umowa>();
            foreach (Umowa u in _transakcje)
            {
                if (u.PracownikOdp.Equals(p))
                {
                    transakcje.Add(u);

                }
            }
            return transakcje;
        }
        #endregion

        #region Aktualizacje statusu i daty planowanego kontaktu
        /// <summary>
        /// Metoda ktualizujaca status klienta
        /// </summary>
        public void AktualizujStatus()
        {
            if (_transakcje.Count() == 0)
            {
                Status = Status.potencjalny;
            }
            else if (_transakcje.Peek().DataUmowy.AddYears(1).CompareTo(DateTime.Today) > 0)
            {
                if (_transakcje.Count() < 3)
                {
                    Status = Status.nowy;
                }
                else
                {
                    Status = Status.stały;
                }
            }
            else
            {
                Status = Status.były;
            }
        }

        /// <summary>
        /// Metoda aktualizuje date planowanego kontaktu:
        /// Jesli daty nie ma to ustawia ja na dzis.
        /// Jesli data jest w przyszlosci, to pozostaje bez zmian.
        /// Jesli data juz minela, to sa dwie mozliwosci:
        /// Jesli ostatni kontakt z klientem byl mniej niz 2 tygodnie temu, to ustalana jest na dwa tygodnie po ostatnim kontakcie.
        /// Jesli ostatni kontakt z klientem byl wiecej niz 2 tygodnie temu, to ustalana jest na dzien dzisiejszy.
        /// </summary>
        public void AktualizujDatePlanKontaktu()
        {
            if (DataPlanowanegoKontaktu == null)
            {
                DataPlanowanegoKontaktu = DateTime.Today;
            }
            else if (DateTime.Compare(DataPlanowanegoKontaktu, DateTime.Today) < 0)
            {
                DataPlanowanegoKontaktu = DateTime.Compare(OstatniKontakt().AddDays(14), DateTime.Today) < 0 ? DateTime.Today : OstatniKontakt() == DateTime.MinValue ? DateTime.Today : OstatniKontakt().AddDays(14);
            }
        }
        #endregion

        #region Wypisywanie i ToString

        /// <summary>
        /// Tworzy czytelny dla czlowieka opis klienta.
        /// </summary>
        /// <returns>Zwraca ciag znakow bedacy opisem danego klienta</returns>
        public override string ToString()
        {
            return base.ToString() + $"\nStatus: {Status}\nData planowanego kontaktu : {DataPlanowanegoKontaktu.ToString("dd.MM.yyyy")}\nLiczba dzialan: {_dzialania.Count()}\nLiczba transakcji: {_transakcje.Count()}\nLiczba kontaktów: {ListaKontaktow.Count()}\n";
        }

        /// <summary>
        /// Meotda tworzy czytelna, tekstowa reprezentacje wszystkich transakcji z klientem i wypisuje je na konsoli.
        /// </summary>
        public void WypiszTransakcje()
        {
            Console.WriteLine($"Transakcje z firma {Nazwa}:");
            List<Umowa> umowy = _transakcje.ToList();
            umowy.Reverse();
            foreach (Umowa u in umowy)
            {
                Console.WriteLine(u);
            }
        }

        /// <summary>
        /// Metoda wypisuje na konsoli w czytelny sposob wszystkie kontakty i dzialania podjete wobec klienta.
        /// </summary>
        public void WypiszDzialania()
        {
            Console.WriteLine($"Działania wobec firmy {Nazwa}:");
            List<Dzialanie> dzialaniaLista = _dzialania.ToList();
            dzialaniaLista.Reverse();
            foreach (Dzialanie d in dzialaniaLista)
            {
                Console.WriteLine(d);
            }
        }
        
        /// <summary>
        /// Metoda wypisuje na konsoli liste osob kontaktowych z klientem:
        /// </summary>
        public void WypiszKontakty()
        {
            Console.WriteLine($"Lista kontaktów:");
            foreach (OsobaKontakt o in _listaKontaktow)
            {
                Console.WriteLine(o);
            }
        }
        #endregion

        #region Sortowanie i klonowanie
        /// <summary>
        /// Metoda sortujaca malejaco lub rosnaco liste dzialan wg dat.
        /// </summary>
        /// <param name="odNajblizszego">
        /// Jesli parametr przyjmie wartosc prawda, to dzialania sa sortowane od najnowszych do najstarszych.
        /// Jesli otrzyma wartosc falsz, to dzialania sa sortowane od najstarszych do najnowszych.
        /// Domyslnie prawda.
        /// </param>
        public void SortujDzialania(bool odNajblizszego = true)
        {
            List<Dzialanie> temp = _dzialania.ToList();
            if (odNajblizszego)
            {
                temp.Sort((x, y) => x.Data.CompareTo(y.Data));
            }
            else
            {
                temp.Sort((x, y) => y.Data.CompareTo(x.Data));
            }

            _dzialania.Clear();
            foreach (Dzialanie d in temp)
            {
                _dzialania.Push(d);
            }
        }

        /// <summary>
        /// Metoda sortujaca malejaco lub rosnaco liste kontaktow wg nazwisk.
        /// </summary>
        /// <param name="malejaco">
        /// Jesli parametr przyjmie wartosc falsz, to kontakty sa sortowane nazwiskami od A do Z.
        /// Jesli otrzyma wartosc prawda, to kontakty sa sortowane od Z do A.
        /// Domyslnie falsz.
        /// </param>
        public void SortujKontakty(bool malejaco = false)
        {
            if (malejaco)
            {
                _listaKontaktow.Sort((x, y) => y.Nazwisko.CompareTo(x.Nazwisko));
            }
            else
            {
                _listaKontaktow.Sort((x, y) => x.Nazwisko.CompareTo(y.Nazwisko));
            }
        }

        /// <summary>
        /// Metoda tworzy obiekt klasy klient bedacy kopia biezacego klienta.
        /// </summary>
        /// <returns>Zwraca nowego klienta bedacego kopia biezacego</returns>
        public override object Clone()
        {
            Klient klon = new Klient(Nazwa, Branza, Nip, Kraj, Miasto, DataZalozenia.ToString(), DataPlanowanegoKontaktu.ToString(), Uwagi, Status);
            _listaKontaktow.ForEach(o => klon.DodajKontakt((OsobaKontakt)o.Clone()));

            foreach(Dzialanie d in _dzialania)
            {
                klon.DodajDzialanie((Dzialanie)d.Clone());
            }

            foreach (Umowa u in _transakcje)
            {
                klon.DodajTransakcje((Umowa)u.Clone());
            }
            return klon;
        }
        #endregion

        #region Zapis i odczyt

        /// <summary>
        /// Metoda pomocnicza zapisujaca dane ze stosow do list po serializacji.
        /// </summary>
        public void StosDoListy()
        {
            TransakcjeList = _transakcje.ToList();
            DzialaniaList = _dzialania.ToList();
        }
        /// <summary>
        /// Metoda pomocnicza zapisujaca dane z list do stosow po deserializacji.
        /// </summary>
        public void ListaDoStosu()
        {
            _dzialania = new Stack<Dzialanie>(DzialaniaList);
            _transakcje = new Stack<Umowa>(TransakcjeList);
        }

        /// <summary>
        /// Metoda zapisuje dane o kliencie do pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku do którego zapisujemy dane, zakonczona na ".xml"</param>
        public override void ZapiszXML(string nazwa)
        {
            StosDoListy();
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Klient));
                xml.Serialize(sw, this);
            }
        }
        /// <summary>
        /// Metoda odczytująca dane klienta z pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku z którego odczytujemy dane, musi się kończyć na ".xml"</param>
        /// <returns>Zwraca odczytany plik jako obiekt klasy Klient</returns>
        public static Klient OdczytajXML(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Klient));
                Klient k = (Klient)xml.Deserialize(sr);
                k.ListaDoStosu();
                return (Klient)k;
            }
        }
        /// <summary>
        /// Metoda zapisująca dane klienta w pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        public override void ZapiszJSON(string nazwa)
        {
            base.ZapiszJSON(nazwa);
        }
        /// <summary>
        /// Metoda odczytująca dane o kliencie z pliku JSON
        /// </summary>
        /// <param name="nazwa"> nazwa pliku </param>
        /// <returns></returns>
        public static Klient OdczytajJSON(string nazwa)
        {
            if (!File.Exists(nazwa))
                return null;
            using (StreamReader sw = new StreamReader(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                string z = sw.ReadToEnd();
                return (Klient)json.Deserialize(z, typeof(Klient));
            }
        }
        #endregion
    }
}
