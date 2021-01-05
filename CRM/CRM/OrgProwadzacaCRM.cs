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
    [Serializable]
    /// <summary>
    /// Klasa definiująca organizację, która prowadzi dla siebie system CRM.
    /// </summary>
    public class OrgProwadzacaCRM:Organizacja,IZapisywalna,ICloneable
    {
        /// <summary>
        /// Lista zawierająca wszystkich pracowników organizacji.
        /// </summary>
        List<Pracownik> _listaPracownikow;
        /// <summary>
        /// Lista zawierająca wszystkie produkty, które sprzedaje organizacja.
        /// </summary>
        List<Produkt> _listaProduktow;
        /// <summary>
        /// Lista zawierająca wszystkich konkurentów organizacji.
        /// </summary>
        List<Konkurent> _listaKonkurentow;
        /// <summary>
        /// Lista zawierająca wszystkich klientów organizacji.
        /// </summary>
        List<Klient> _listaKlientow;

        public List<Pracownik> ListaPracownikow { get => _listaPracownikow;}
        public List<Produkt> ListaProduktow { get => _listaProduktow;  }
        public List<Konkurent> ListaKonkurentow { get => _listaKonkurentow; }
        public List<Klient> ListaKlientow { get => _listaKlientow;}

        #region Konstruktory
        public OrgProwadzacaCRM()
        {
            _listaPracownikow = new List<Pracownik>();
            _listaProduktow = new List<Produkt>();
            _listaKlientow = new List<Klient>();
            _listaKonkurentow = new List<Konkurent>();

        }
        public OrgProwadzacaCRM(string nazwa, Branże branza) : base(nazwa,branza)
        {
            _listaPracownikow = new List<Pracownik>();
            _listaProduktow = new List<Produkt>();
            _listaKlientow = new List<Klient>();
            _listaKonkurentow = new List<Konkurent>();
        }

        public OrgProwadzacaCRM(string nazwa, Branże branza, string nip, string kraj, string miasto,string dataZalozenia): base(nazwa, branza, nip,  kraj,  miasto, dataZalozenia)
        {
            _listaPracownikow = new List<Pracownik>();
            _listaProduktow = new List<Produkt>();
            _listaKlientow = new List<Klient>();
            _listaKonkurentow = new List<Konkurent>();
        }
        public OrgProwadzacaCRM(string nazwa, Branże branza, string nip, string kraj, string miasto, string adres, string kodpocztowy, string notatki, string dataZalozenia) : base(nazwa, branza, nip, kraj, miasto, adres,kodpocztowy,notatki, dataZalozenia)
        {
            _listaPracownikow = new List<Pracownik>();
            _listaProduktow = new List<Produkt>();
            _listaKlientow = new List<Klient>();
            _listaKonkurentow = new List<Konkurent>();
        }

        public override object Clone()
        {
            OrgProwadzacaCRM klonowana = new OrgProwadzacaCRM(Nazwa, Branza,Nip,  Kraj, Miasto, Adres, KodPocztowy,Notatki, DataZalozenia.ToString());

            _listaPracownikow.ForEach(p => klonowana.ListaPracownikow.Add((Pracownik)p.Clone()));
            _listaProduktow.ForEach(p => klonowana.ListaProduktow.Add((Produkt)p.Clone()));
            _listaKonkurentow.ForEach(k => klonowana.ListaKonkurentow.Add((Konkurent)k.Clone()));
            _listaKlientow.ForEach(k => klonowana.ListaKlientow.Add((Klient)k.Clone()));
            return klonowana;
        }

        public bool WprowadzonoZmiany(OrgProwadzacaCRM other)
        {
            if (_listaKlientow.FindAll(k=>other.JestKlientem(k)).Count()==this.PodajIloscKlientow() && 
                _listaKonkurentow.FindAll(k => other.JestKonkurentem(k)).Count() == this.PodajIloscKonkurentow() &&
                _listaPracownikow.FindAll(p => other.JestPracownikiem(p)).Count() == this.PodajIloscPracownikow() &&
                _listaProduktow.FindAll(p => other.JestProduktem(p)).Count() == this.PodajIloscProduktow() &&
                Nazwa==other.Nazwa && Branza==other.Branza && Nip==other.Nip && DataZalozenia==other.DataZalozenia &&
                Kraj==other.Kraj && Miasto ==other.Miasto && Adres==other.Adres && KodPocztowy==other.KodPocztowy && Notatki==other.Notatki)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Funkcje Pracownicy
        /// <summary>
        /// Funkcja dodająca obiekt Pracownik do listy pracowników organizacji. Powoduje wyrzucenie wyjątku AlreadyInListException(), jeżeli taki obiekt już znajduje się na liście.
        /// </summary>
        /// <param name="p">Obiekt Pracownik</param>
        public void DodajPracownika(Pracownik p)
        {
            if (JestPracownikiem(p))
            {
                throw new AlreadyInListException("Pracownik znajduje się już na liście pracowników");
            }
            _listaPracownikow.Add(p);
        }
        /// <summary>
        /// Funkcja usuwając pracownika z listy pracowników organizacji.
        /// </summary>
        /// <param name="imie">Imie pracownika do usunięcia.</param>
        /// <param name="nazwisko">Nazwisko pracownika do usunięcia.</param>
        /// <param name="stanowisko">Stanowisko pracownika do usunięcia</param>
        /// <returns>Prawdę, jeśli taki pracownik był na liście i go usunięto,
        /// Fałsz, jeśli takiego pracownika nie było.</returns>
        public bool UsunPracownika(string imie, string nazwisko, Stanowiska stanowisko)
        {
            if (_listaPracownikow.Count() != 0 && JestPracownikiem(imie, nazwisko, stanowisko))
            {
                _listaPracownikow.RemoveAt(_listaPracownikow.FindIndex(p => (p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko)));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwając pracownika z listy pracowników organizacji.
        /// </summary>
        /// <param name="p">Obiekt Pracownik przeznaczony do usunięcia.</param>
        /// <returns>Prawdę, jeśli taki pracownik był na liście i go usunięto,
        /// Fałsz, jeśli takiego pracownika nie było.</returns>
        public bool UsunPracownika(Pracownik p)
        {
            if (_listaPracownikow.Count() != 0 && JestPracownikiem(p))
            {
                _listaPracownikow.Remove(p);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwa wszystkich pracowników z listy.
        /// </summary>
        public void UsunWszystkichPracownikow()
        {
            _listaPracownikow.Clear();
        }
        /// <summary>
        /// Podaje liczbę pracowników pracujących w organizacji.
        /// </summary>
        /// <returns>Zwraca liczbę pracowników.</returns>
        public int PodajIloscPracownikow()
        {
            if(_listaPracownikow is null)
            {
                return -1;
            }
            return _listaPracownikow.Count();
        }
        /// <summary>
        /// Funkcja sprawdza, czy podany obiekt Pracownik znajduje się na liście pracowników.
        /// </summary>
        /// <param name="p">Obiekt pracownik, którego szukamy na liście pracowników.</param>
        /// <returns>Prawdę, jeśli obiekt Pracownik jest pracownikiem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestPracownikiem(Pracownik p)
        {
            return _listaPracownikow.Contains(p);
        }
        /// <summary>
        /// Funkcja sprawdza, czy pracownik o podanym imeiniu, nazwisku i stanowisku znajduje się na liście pracowników.
        /// </summary>
        /// <param name="imie">Imie pracownika, który jest szukany.</param>
        /// <param name="nazwisko">Nazwisko pracownika, który jest szukany.</param>
        /// <param name="stanowisko">Stanowisko pracownika, który jest szukany.</param>
        /// <returns>Prawdę, jeśli obiekt o takich parametrach jest pracownikiem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestPracownikiem(string imie, string nazwisko, Stanowiska stanowisko)
        {
            return _listaPracownikow.Exists(p => p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich pracowników organizacji prowadzącej CRM znajdujących się na podanym stanowisku.
        /// </summary>
        /// <param name="stanowisko">Stanowisko, na którym szukamy pracowników.</param>
        /// <returns>Listę pracowników, znajdujących się na podanym stanowisku.</returns>
        public List<Pracownik> ZnajdzWszystkichPracownikowStanowisko(Stanowiska stanowisko)
        {
            return _listaPracownikow.FindAll(p => p.Stanowisko == stanowisko);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich pracowników organizacji prowadzącej CRM o podane płci.
        /// </summary>
        /// <param name="plec">Płeć, jaką mają szukani pracownicy.</param>
        /// <returns>Listę pracowników, o podanej płci.</returns>
        public List<Pracownik> ZnajdzWszystkichPracownikowPlec(Plcie plec)
        {
            return _listaPracownikow.FindAll(p => p.Plec == plec);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich pracowników organizacji prowadzącej CRM, którzy zaczeli w niej pracować przed podaną datą.
        /// </summary>
        /// <param name="dataRozpoczecia">Data, przed którą zostali zatrudnieni szukani pracownicy.</param>
        /// <returns>Listę pracowników, którzy zostali zatrudnieni przed podaną datą.</returns>
        public List<Pracownik> ZnajdzWszystkichPracownikowPracaPrzed(string dataRozpoczecia)
        {
            DateTime DataRozpoczecia;
            DateTime.TryParseExact(dataRozpoczecia, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DataRozpoczecia);

            return _listaPracownikow.FindAll(p => p.DataRozpoczeciaPracy < DataRozpoczecia);
        }
        /// <summary>
        /// Funkcja sortuje wszystkich pracowników alfabetycznie, najpierw po nazwisku, potem po imieniu.
        /// </summary>
        public void PracownicySortujAlfabetycznie()
        {
            _listaPracownikow.Sort();
        }
        /// <summary>
        /// Funkcja sortuje wszystkich pracowników po dacie zatrudnienia. 
        /// </summary>
        /// <param name="rosnaco">True oznacza sortowanie od najwcześniejszego zatrudnienia do najpóźniejszego,
        /// False oznacza sortowanie od najpóźniejszego zatrudnienia do najwcześniejszego.</param>
        public void PracownicySortujPoDacieZatrudnienia(bool rosnaco=true)
        {
            if (rosnaco)
            {
                _listaPracownikow.Sort((x, y) => x.DataRozpoczeciaPracy.CompareTo(y.DataRozpoczeciaPracy));
            }
            else
            {
                _listaPracownikow.Sort((x, y) => y.DataRozpoczeciaPracy.CompareTo(x.DataRozpoczeciaPracy));

            }
        }
        #endregion

        #region Funkcje Produkty
        /// <summary>
        /// Funkcja dodająca obiekt Produktów do listy produktów jakie oferuje organizacja. Powoduje wyrzucenie wyjątku AlreadyInListException(), jeżeli taki obiekt już znajduje się na liście.
        /// </summary>
        /// <param name="p">Obiekt Produkt</param>
        public void DodajProdukt(Produkt p)
        {
            if (JestProduktem(p))
            {
                throw new AlreadyInListException("Produkt znajduje się już na liście produktów");
            }
            _listaProduktow.Add(p);
        }
        /// <summary>
        /// Funkcja usuwając produkt z listy produktów, jakie oferuje organizacja.
        /// </summary>
        /// <param name="kod">Kod produktu do usunięcia.</param>
        /// <returns>Prawdę, jeśli taki produkt był na liście i go usunięto,
        /// Fałsz, jeśli takiego produktu nie było.</returns>
        public bool UsunProdukt(string kod)
        {
            if (_listaProduktow.Count() != 0 && JestProduktem(kod))
            {
                _listaProduktow.RemoveAt(_listaProduktow.FindIndex(p => (p.Kod == kod)));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwając produkt z listy produktów, jakie oferuje organizacja.
        /// </summary>
        /// <param name="p">Obiekt Produkt do usunięcia.</param>
        /// <returns>Prawdę, jeśli taki produkt był na liście i go usunięto,
        /// Fałsz, jeśli takiego produktu nie było.</returns>
        public bool UsunProdukt(Produkt p)
        {
            if (_listaProduktow.Count() != 0 && JestProduktem(p))
            {
                _listaProduktow.Remove(p);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwa wszystkie produkty z listy produktów.
        /// </summary>
        public void UsunWszystkieProdukty()
        {
            _listaProduktow.Clear();
        }
        /// <summary>
        /// Funkcja oblicza liczbę produktów oferowanych przez organizację.
        /// </summary>
        /// <returns>Zwraca liczbę produktów.</returns>
        public int PodajIloscProduktow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaProduktow.Count();
        }
        /// <summary>
        /// Funkcja sprawdza, czy podany obiekt Produkt znajduje się na liście produktów organizacji.
        /// </summary>
        /// <param name="p">Obiekt produkt, którego szukamy na liście produktów.</param>
        /// <returns>Prawdę, jeśli obiekt Produkt jest produktem oferowanym przez organizację prowadzącą CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestProduktem(Produkt p)
        {
            return _listaProduktow.Contains(p);
        }
        /// <summary>
        /// Funkcja sprawdza, czy produkt o podanym kodzie znajduje się na liście produktó organizacji.
        /// </summary>
        /// <param name="kod">Kod produktu, który jest szukany.</param>
        /// <returns>Prawdę, jeśli obiekt o takim kodzie jest produktem oferowanym przez oraganizację prowadzącą CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestProduktem(string kod)
        {
            return _listaProduktow.Exists(p => p.Kod == kod);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkie produkty oferowane przez organizację prowadzącą CRM poniżej lub powyżej podanej ceny.
        /// </summary>
        /// <param name="porownywalnaCena">Cena wobec której porównywane są produkty.</param>
        /// <param name="tansze">Parametr służący do opisania, czy chcemy sobaczyć produkty tańsze czy droższe. True oznacza, że chcemy uzyskać produkty tańsze (co też jest domyślną wartością), a false, że produkty droższe od podanej ceny.</param>
        /// <returns>Listę produktów tańszych/droższych od podanej ceny.</returns>
        public List<Produkt> ZnajdzWszystkieProduktyCena(double porownywalnaCena, bool tansze = true)
        {
            if (tansze)
            {
                return _listaProduktow.FindAll(p => p.Cena < porownywalnaCena);
            }
            else
            {
                return _listaProduktow.FindAll(p => p.Cena > porownywalnaCena);
            }
        }
        

        /// <summary>
        /// Znajduje listę produktów o podanym kodzie.
        /// </summary>
        /// <param name="kod">Szukany kod produktu</param>
        /// <returns>Lista produktów o podanym kodzie</returns>
        public List<Produkt> ZnajdzProduktKod(string kod)
        {
            return _listaProduktow.FindAll(p => p.Kod == kod);
        }

        /// <summary>
        /// Funkcja wyszukuje wszystkie pprodukty oferowane przez organizację prowadzącą CRM, które sprzedawane są w podanej jednostce.
        /// </summary>
        /// <param name="jednostka">Jednostka, w jakiej sprzedawane są produkty, których szukamy.</param>
        /// <returns>Listę produktów o danej jednostce.</returns>
        public List<Produkt> ZnajdzWszystkieProduktyJednostka(Jednostki jednostka)
        {
            return _listaProduktow.FindAll(p => p.Jednostka == jednostka);
        }
        /// <summary>
        /// Funkcja sortująca produkty oferowane przez organizację prowadzącą CRM według ceny. 
        /// </summary>
        /// <param name="rosnaco">True, produkty sortowane są rosnąco wg ceny, False, produkty sortowane są malejąco wg ceny.</param>
        public void ProduktySortujPoCenie(bool rosnaco=true)
        {
            if (rosnaco)
            {
                _listaProduktow.Sort((x, y) => x.Cena.CompareTo(y.Cena));
            }
            else
            {
                _listaProduktow.Sort((x, y) => y.Cena.CompareTo(x.Cena));

            }
        }
        /// <summary>
        /// Funkcja sortuje wszystkich produkty alfabetycznie po ich nazwie.
        /// </summary>
        public void ProduktySortujPoKodzie()
        {
            _listaProduktow.Sort();
        }
        #endregion

        #region Funkcje Konkurenci
        /// <summary>
        /// Funkcja dodająca obiekt Konkurent do listy konkurentów organizacji. Powoduje wyrzucenie wyjątku AlreadyInListException(), jeżeli taki obiekt już znajduje się na liście.
        /// </summary>
        /// <param name="k">Obiekt Konkurent</param>
        public void DodajKonkurenta(Konkurent k)
        {
            if (JestKonkurentem(k))
            {
                throw new AlreadyInListException("Konkurent znajduje się już na liście konkurentów");
            }
            _listaKonkurentow.Add(k);
        }
        /// <summary>
        /// Funkcja usuwająca konkurenta z listy konkurentów organizacji.
        /// </summary>
        /// <param name="nazwa">Nazwa organizacji przeznaczonej do usunięcia z listy konkurentów.</param>
        /// <returns>Prawdę, jeśli taki konkurent był na liście i go usunięto,
        /// Fałsz, jeśli takiego konkurenta nie było.</returns>
        public bool UsunKonkurenta(string nazwa)
        {
            if (_listaKonkurentow.Count() != 0 && JestKonkurentem(nazwa))
            {
                _listaKonkurentow.RemoveAt(_listaKonkurentow.FindIndex(k => (k.Nazwa == nazwa)));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwając konkurenta z listy konkurentów organizacji.
        /// </summary>
        /// <param name="k">Obiekt Konkurent przeznaczony do usunięcia.</param>
        /// <returns>Prawdę, jeśli taki konkurent był na liście i go usunięto,
        /// Fałsz, jeśli takiego pracownika nie było.</returns>
        public bool UsunKonkurenta(Konkurent k)
        {
            if (_listaKonkurentow.Count() != 0 && JestKonkurentem(k))
            {
                _listaKonkurentow.Remove(k);
                return true;
            }
            return false; 
        }
        /// <summary>
        /// Funkcja usuwa wszystkich konkurentoów z listy konkurentów organizacji.
        /// </summary>
        public void UsunWszystkichKonkurentow()
        {
            _listaKonkurentow.Clear();
        }
        /// <summary>
        /// Podaje liczbę konkurentów organizacji prowadzącej CRM.
        /// </summary>
        /// <returns>Zwraca liczbę konkurentów.</returns>
        public int PodajIloscKonkurentow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaKonkurentow.Count();
        }
        /// <summary>
        /// Funkcja sprawdza, czy podany obiekt Konkurent znajduje się na liście konkurentów.
        /// </summary>
        /// <param name="k">Obiekt Konkurent, którego szukamy na liście konkurentów.</param>
        /// <returns>Prawdę, jeśli obiekt Konkurent jest konkurentem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestKonkurentem(Konkurent k)
        {
            return _listaKonkurentow.Contains(k);
        }
        /// <summary>
        /// Funkcja sprawdza, czy konkurent o podanej nazwie znajduje się na liście konkurentów.
        /// </summary>
        /// <param name="nazwa">Nazwa szukanej konkurencyjnej organizacji.</param>
        /// <returns>Prawdę, jeśli obiekt o takich parametrach jest konkurentem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestKonkurentem(string nazwa)
        {
            return _listaKonkurentow.Exists(k => k.Nazwa == nazwa);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich konkurentów organizacji prowadzącej CRM o podanym stopiu zagrożenia.
        /// </summary>
        /// <param name="zagrozenie">Stopień zagrożenia szukanych konkurentów.</param>
        /// <returns>Listę konkurentów, o podanym stopniu zagrożenia.</returns>
        public List<Konkurent> ZnajdzWszystkichKonkurentowZagrozenie(Konkurent.StopienZagrozenia zagrozenie)
        {
            return _listaKonkurentow.FindAll(k => k.Zagrozenie == zagrozenie);
        }
        /// <summary>
        /// Funkcja sortuje wszystkich konkurentów alfabetycznie po nazwie.
        /// </summary>
        public void KonkurenciSortujAlfabetycznie()
        {
            _listaKonkurentow.Sort();
        }
        /// <summary>
        /// Funkcja sortuje wszystkich konkurentów według daty założenia organizacji.
        /// </summary>
        public void KonkurenciSortujDataZalozenia()
        {
            _listaKonkurentow.Sort((x, y) => y.DataZalozenia.CompareTo(x.DataZalozenia));
        }
        #endregion

        #region Funkcje Klienci
        /// <summary>
        /// Funkcja dodająca obiekt Klient do listy klientów organizacji. Powoduje wyrzucenie wyjątku AlreadyInListException(), jeżeli taki obiekt już znajduje się na liście.
        /// </summary>
        /// <param name="k">Obiekt Klient</param>
        public void DodajKlienta(Klient k)
        {
            if (JestKlientem(k))
            {
                throw new AlreadyInListException("Klient znajduje się już na liście klientow");
            }
            _listaKlientow.Add(k);
        }
        /// <summary>
        /// Funkcja usuwająca klient z listy klientów organizacji.
        /// </summary>
        /// <param name="nazwa">Nazwa organizacji klienta przeznaczonej do usunięcia z listy klientoów.</param>
        /// <returns>Prawdę, jeśli taki klient był na liście i go usunięto,
        /// Fałsz, jeśli takiego konkurenta nie było.</returns>
        public bool UsunKlienta(string nazwa)
        {
            if (_listaKlientow.Count() != 0 && JestKlientem(nazwa))
            {
                _listaKlientow.RemoveAt(_listaKlientow.FindIndex(k => (k.Nazwa == nazwa)));
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwająca klienta z listy klientów organizacji prowadzącej CRM.
        /// </summary>
        /// <param name="k">Obiekt Klient przeznaczony do usunięcia.</param>
        /// <returns>Prawdę, jeśli taki klient był na liście i go usunięto,
        /// Fałsz, jeśli takiego pracownika nie było.</returns>
        public bool UsunKlienta(Klient k)
        {
            if (_listaKlientow.Count() != 0 && JestKlientem(k))
            {
                _listaKlientow.Remove(k);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Funkcja usuwa wszystkich klientów z listy klientów organizacji.
        /// </summary>
        public void UsunWszystkichKlientow()
        {
            _listaKlientow.Clear();
        }
        /// <summary>
        /// Podaje liczbę klientów organizacji prowadzącej CRM.
        /// </summary>
        /// <returns>Zwraca liczbę klientów.</returns>
        public int PodajIloscKlientow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaKlientow.Count();
        }
        /// <summary>
        /// Funkcja sprawdza, czy podany obiekt Klient znajduje się na liście klientów organizcji prowadzącej CRM.
        /// </summary>
        /// <param name="k">Obiekt Klient, którego szukamy na liście klientów.</param>
        /// <returns>Prawdę, jeśli obiekt Klient jest klientem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestKlientem(Klient k)
        {
            return _listaKlientow.Contains(k);
        }
        /// <summary>
        /// Funkcja sprawdza, czy klient o podanej nazwie organizacji znajduje się na liście klientów organizacji prowadzącej CRM.
        /// </summary>
        /// <param name="nazwa">Nazwa szukanego klienta.</param>
        /// <returns>Prawdę, jeśli obiekt o takiej nazwie jest klientem oraganizacji prowadzącej CRM,
        /// Fałsz, jeśli nie jest.</returns>
        public bool JestKlientem(string nazwa)
        {
            return _listaKlientow.Exists(k => k.Nazwa == nazwa);
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich klientów organizacji prowadzącej CRM, z którymi planowany kontakt ma odbyć się za podaną liczbę dni. Domyślnie wyszukuje klientów, z którymi planowany kontakt jest ustawiony na dzisiaj.
        /// </summary>
        /// <param name="ZaIleDni">Liczba dni, za którą ma się odbyć planowany kontakt z klientem.</param>
        /// <returns>Listę klientów, z którymi planowany kontakt jest ustawiony za podaną liczbę dni.</returns>
        public List<Klient> ZnajdzWszystkichKlientowPlanowanyKontakt(int ZaIleDni = 0)
        {
            return _listaKlientow.FindAll(k => k.DataPlanowanegoKontaktu == DateTime.Today.AddDays(ZaIleDni));
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich klientów organizacji prowadzącej CRM, z którymi ostatnio kontaktowano się podaną liczbę dni temu. Domyślnie wyszukuje klientów, z którymi osatnio kontaktowano się 2 tygodnie temu.
        /// </summary>
        /// <param name="IleDniTemu">Liczba dni jaka minęła odkąd kontaktowano się z klientem.</param>
        /// <returns>Listę klientów, z którymi kontaktowano się ustaloną liczbę dni temu.</returns>
        public List<Klient> ZnajdzWszystkichKlientowOstatniKontakt(int IleDniTemu = 14)
        {
            return _listaKlientow.FindAll(k => k.OstatniKontakt() <= DateTime.Today.AddDays(-IleDniTemu));
        }
        /// <summary>
        /// Funkcja wyszukuje wszystkich klientów organizacji prowadzącej CRM o podanym statusie.
        /// </summary>
        /// <param name="status">Status wyszukiwanych klientów.</param>
        /// <returns>Listę klientów, o podanym statusie.</returns>
        public List<Klient> ZnajdzWszystkichKlientowStatus(Status status)
        {
            return _listaKlientow.FindAll(k => k.Status == status);
        }
        /// <summary>
        /// Funkcja sortuje wszystkich klientów alfabetycznie po nazwie organizacji.
        /// </summary>
        public void KlienciSortujAlfabetycznie()
        {
            _listaKlientow.Sort();
        }
        /// <summary>
        /// Funkcja sortuje wszystkich klientów według daty planowanego kontaktu.
        /// </summary>
        public void KlienciSortujDataPlanowanegoKontaktu()
        {
            _listaKlientow.Sort((x, y) => y.DataPlanowanegoKontaktu.CompareTo(x.DataPlanowanegoKontaktu));
        }
        /// <summary>
        /// Funkcja sortuje wszystkich klientów według daty ostatniego kontaktu.
        /// </summary>
        public void KlienciSortujDataOstatniegoKontaktu()
        {
            _listaKlientow.Sort((x, y) => y.OstatniKontakt().CompareTo(x.OstatniKontakt()));
        }

        /// <summary>
        /// Metoda aktualizuje statusy wszystkich klientów organizacji
        /// </summary>
        public void AktualizujStatusKlientow()
        {
            foreach (Klient k in ListaKlientow)
            {
                k.AktualizujStatus();
            }
        }

        /// <summary>
        /// Metoda aktualizuje daty planowanych kontaktow ze wszystkimi klientami organizacji
        /// </summary>
        public void AktualizujDatyPlanowanychKontaktow()
        {
            foreach (Klient k in ListaKlientow)
            {
                k.AktualizujDatePlanKontaktu();
            }
        }
        #endregion
        #region Zapis/Odczyt
        /// <summary>
        /// Funkcja zapisuje dane dane organizacji, która prowadzi CRM do pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku do którego zapisujemy dane, musi się kończyć na ".xml"</param>
        public override void ZapiszXML(string nazwa)
        {
            foreach (Klient k in _listaKlientow)
            {
                k.StosDoListy();
                foreach (Umowa u in k.TransakcjeList)
                {
                    u.ListaKupionychProduktow.Clear();
                    u.IlosciKupionychProduktow.Clear();
                    foreach (Produkt key in u.KupioneProdukty.Keys)
                    {
                        u.ListaKupionychProduktow.Add(key);
                        u.IlosciKupionychProduktow.Add(u.KupioneProdukty[key]);
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(OrgProwadzacaCRM));
                xml.Serialize(sw, this);
            }
        }
        /// <summary>
        /// Funkcja odczytująca dane organizacji, która prowadzi CRM z pliku XML.
        /// </summary>
        /// <param name="nazwa">Nazwa pliku z którego odczytujemy dane, musi się kończyć na ".xml"</param>
        /// <returns>Odczytany plik jako obiekt klasy OrgProwadzacaCRM</returns>
        public static OrgProwadzacaCRM OdczytajXML(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(OrgProwadzacaCRM));
                OrgProwadzacaCRM o = (OrgProwadzacaCRM)xml.Deserialize(sr);
                foreach (Klient k in o.ListaKlientow)
                {
                    k.ListaDoStosu();
                    foreach (Umowa u in k.TransakcjeList)
                    {
                        for(int i=0;i< u.IlosciKupionychProduktow.Count();++i)
                        {
                            u.KupioneProdukty.Add(u.ListaKupionychProduktow[i], u.IlosciKupionychProduktow[i]);
                            Console.WriteLine(u.KupioneProdukty);
                        }
                    }
                }
                return o;
            }
        }
        /// <summary>
        /// Funkcja zapisuje dane organizacji do pliku JSON
        /// </summary>
        /// <param name="nazwa"></param>
        public override void ZapiszJSON(string nazwa)
        {
            //foreach (Klient k in _listaKlientow)
            //{
            //    k.StosDoListy();
            //    foreach (Umowa u in k.TransakcjeList)
            //    {
            //        foreach (Produkt key in u.KupioneProdukty.Keys)
            //        {
            //            u.ListaKupionychProduktow.Add(key);
            //            u.IlosciKupionychProduktow.Add(u.KupioneProdukty[key]);
            //        }
            //    }
            //}
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                sw.WriteLine(json.Serialize(this));
            }
        }
        /// <summary>
        /// Funkcja odczytująca dane organizacji z pliku JSON
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns></returns>
        public static OrgProwadzacaCRM OdczytajJSON(string nazwa)
        {
            if (!File.Exists(nazwa))
                return null;
            using (StreamReader sw = new StreamReader(nazwa))
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                string z = sw.ReadToEnd(); 
                OrgProwadzacaCRM o = (OrgProwadzacaCRM)json.Deserialize(z, typeof(OrgProwadzacaCRM));
                //foreach (Klient k in o.ListaKlientow)
                //{
                //    k.ListaDoStosu();
                //    foreach (Umowa u in k.TransakcjeList)
                //    {
                //        for (int i = 0; i < u.IlosciKupionychProduktow.Count(); ++i)
                //        {
                //            u.KupioneProdukty.Add(u.ListaKupionychProduktow[i], u.IlosciKupionychProduktow[i]);
                //            Console.WriteLine(u.KupioneProdukty);
                //        }
                //    }
                //}
                return o;
            }
        }
        #endregion
        #region Funkcje wypisujace
        /// <summary>
        /// Funkcja wypisująca na konsolę wszystkie produkty, jakie oferuje organizacja prowadząca CRM.
        /// </summary>
        /// <returns>Napis zawierający informacje o wszystkich produktach z listy produktów organizacji.</returns>
        public string WypiszProdukty()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Produkty:");
            if (_listaProduktow.Count() == 0)
            {
                napis.Append($"Firma {Nazwa} nie posiada żadnych produktów");
            }
            else
            {
                _listaProduktow.ToList().ForEach(p => napis.AppendLine(p.ToString()));
            }
            return napis.ToString();
        }
        /// <summary>
        /// Funkcja wypisująca na konsolę wszystkich pracowników organizacji prowadzącej CRM.
        /// </summary>
        /// <returns>Napis zawierający informacje o wszystkich pracownikach organizacji.</returns>
        public string WypiszPracownikow()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Pracownicy:");
            if (_listaPracownikow.Count() == 0)
            {
                napis.Append($"Firma {Nazwa} nie posiada żadnych pracowników");
            }
            else
            {
                _listaPracownikow.ForEach(p => napis.AppendLine(p.ToString()));
            }
            return napis.ToString();
        }
        /// <summary>
        /// Funkcja wypisująca na konsolę wszystkich konkurentów organizacji prowadzącej CRM.
        /// </summary>
        /// <returns>Napis zawierający informacje o wszystkich konkurentach organizacji.</returns>
        public string WypiszKonkurentow()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Konkurenci:");
            if (_listaKonkurentow.Count() == 0)
            {
                napis.Append($"Firma {Nazwa} nie posiada żadnych konkurentów");
            }
            else
            {
                _listaKonkurentow.ForEach(p => napis.AppendLine(p.ToString()));
            }
            return napis.ToString();
        }
        /// <summary>
        /// Funkcja wypisująca na konsolę wszystkich klientów organizacji prowadzącej CRM.
        /// </summary>
        /// <returns>Napis zawierający informacje o wszystkich klientach organizacji.</returns>
        public string WypiszKlientow()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("Klienci:");
            if (_listaKlientow.Count() == 0)
            {
                napis.Append($"Firma {Nazwa} nie posiada żadnych klientów");
            }
            else
            {
                _listaKlientow.ForEach(p => napis.AppendLine(p.ToString()));
            }
            return napis.ToString();
        }
        /// <summary>
        /// Funkcja wypisująca na konsolę wszystkie informacje o organizacji prowadzącej CRM oraz jej pracowników, konkurentów, klientów oraz produkty, które oferuje.
        /// </summary>
        /// <returns>Napis zawierający informacje o całej organizacji prowadzącej CRM.</returns>
        public override string ToString()
        {
            StringBuilder napis = new StringBuilder();
            napis.AppendLine("\tOrganizacja: " + base.ToString());
            napis.AppendLine("Pracownicy:");
            _listaPracownikow.ForEach(p => napis.AppendLine(p.ToString()));
            napis.AppendLine("Produkty:");
            _listaProduktow.ToList().ForEach(p => napis.AppendLine(p.ToString()));
            napis.AppendLine("Konkurenci:");
            _listaKonkurentow.ForEach(p => napis.AppendLine(p.ToString()));
            napis.AppendLine("Klienci:");
            _listaKlientow.ForEach(p => napis.AppendLine(p.ToString()));

            return napis.ToString();
        }
        #endregion
    }
}
