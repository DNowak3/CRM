using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CRM
{
    [Serializable]
    public class OrgProwadzacaCRM:Organizacja,IZapisywalna
    {
        List<Pracownik> _listaPracownikow;
        List<Produkt> _listaProduktow;
        List<Konkurent> _listaKonkurentow;
        List<Klient> _listaKlientow;

        public List<Pracownik> ListaPracownikow { get => _listaPracownikow;}
        public List<Produkt> ListaProduktow { get => _listaProduktow;  }
        public List<Konkurent> ListaKonkurentow { get => _listaKonkurentow; }
        public List<Klient> ListaKlientow { get => _listaKlientow;}

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

        public OrgProwadzacaCRM(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia): base(nazwa, branza, nip,  kraj,  miasto, dataZalozenia)
        {
            _listaPracownikow = new List<Pracownik>();
            _listaProduktow = new List<Produkt>();
            _listaKlientow = new List<Klient>();
            _listaKonkurentow = new List<Konkurent>();
        }
        #region Funkcje Pracownicy
        public void DodajPracownika(Pracownik p)
        {
            if (JestPracownikiem(p))
            {
                throw new AlreadyInListException("Pracownik znajduje się już na liście pracowników");
            }
            _listaPracownikow.Add(p);
        }
        public bool UsunPracownika(string imie, string nazwisko, Stanowiska stanowisko)
        {
            if (_listaPracownikow.Count() != 0 && JestPracownikiem(imie, nazwisko, stanowisko))
            {
                _listaPracownikow.RemoveAt(_listaPracownikow.FindIndex(p => (p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko)));
                return true;
            }
            return false;
        }
        public bool UsunPracownika(Pracownik p)
        {
            if (_listaPracownikow.Count() != 0 && JestPracownikiem(p))
            {
                _listaPracownikow.Remove(p);
                return true;
            }
            return false;
        }
        public void UsunWszystkichPracownikow()
        {
            _listaPracownikow.Clear();
        }
        public int PodajIloscPracownikow()
        {
            if(_listaPracownikow is null)
            {
                return -1;
            }
            return _listaPracownikow.Count();
        }
        public bool JestPracownikiem(Pracownik p)
        {
            return _listaPracownikow.Contains(p);
        }
        public bool JestPracownikiem(string imie, string nazwisko, Stanowiska stanowisko)
        {
            return _listaPracownikow.Exists(p => p.Imie == imie && p.Nazwisko == nazwisko && p.Stanowisko == stanowisko);
        }
        public List<Pracownik> ZnajdzWszystkichPracownikowStanowisko(Stanowiska stanowisko)
        {
            return _listaPracownikow.FindAll(p => p.Stanowisko == stanowisko);
        }
        public List<Pracownik> ZnajdzWszystkichPracownikowPlec(Plcie plec)
        {
            return _listaPracownikow.FindAll(p => p.Plec == plec);
        }
        public List<Pracownik> ZnajdzWszystkichPracownikowPracaPrzed(string dataRozpoczecia)
        {
            DateTime DataRozpoczecia;
            DateTime.TryParseExact(dataRozpoczecia, new[] { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" }, null, System.Globalization.DateTimeStyles.None, out DataRozpoczecia);

            return _listaPracownikow.FindAll(p => p.DataRozpoczeciaPracy < DataRozpoczecia);
        }
        public void PracownicySortujAlfabetycznie()
        {
            _listaPracownikow.Sort();
        }
        #endregion

        #region Funkcje Produkty
        public void DodajProdukt(Produkt p)
        {
            if (JestProduktem(p))
            {
                throw new AlreadyInListException("Produkt znajduje się już na liście produktów");
            }
            _listaProduktow.Add(p);
        }
        public bool UsunProdukt(string kod)
        {
            if (_listaProduktow.Count() != 0 && JestProduktem(kod))
            {
                _listaProduktow.RemoveAt(_listaProduktow.FindIndex(p => (p.Kod == kod)));
                return true;
            }
            return false;
        }
        public bool UsunProdukt(Produkt p)
        {
            if (_listaProduktow.Count() != 0 && JestProduktem(p))
            {
                _listaProduktow.Remove(p);
                return true;
            }
            return false;
        }
        public void UsunWszystkieProdukty()
        {
            _listaProduktow.Clear();
        }
        public int PodajIloscProduktow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaProduktow.Count();
        }
        public bool JestProduktem(Produkt p)
        {
            return _listaProduktow.Contains(p);
        }
        public bool JestProduktem(string kod)
        {
            return _listaProduktow.Exists(p => p.Kod == kod);
        }
        public List<Produkt> ZnajdzWszystkieProduktyCena(float porownywalnaCena, bool tansze = false)
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
        public List<Produkt> ZnajdzWszystkieProduktyJednostka(Jednostki jednostka)
        {
            return _listaProduktow.FindAll(p => p.Jednostka == jednostka);
        }
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
        public void ProduktySortujPoNazwie()
        {
            _listaProduktow.Sort();
        }
        #endregion

        #region Funkcje Konkurenci
        public void DodajKonkurenta(Konkurent k)
        {
            if (JestKonkurentem(k))
            {
                throw new AlreadyInListException("Konkurent znajduje się już na liście konkurentów");
            }
            _listaKonkurentow.Add(k);
        }
        public bool UsunKonkurenta(string nazwa)
        {
            if (_listaKonkurentow.Count() != 0 && JestKonkurentem(nazwa))
            {
                _listaKonkurentow.RemoveAt(_listaKonkurentow.FindIndex(k => (k.Nazwa == nazwa)));
                return true;
            }
            return false;
        }
        public bool UsunKonkurenta(Konkurent k)
        {
            if (_listaKonkurentow.Count() != 0 && JestKonkurentem(k))
            {
                _listaKonkurentow.Remove(k);
                return true;
            }
            return false; 
        }
        public void UsunWszystkichKonkurentow()
        {
            _listaKonkurentow.Clear();
        }
        public int PodajIloscKonkurentow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaKonkurentow.Count();
        }
        public bool JestKonkurentem(Konkurent k)
        {
            return _listaKonkurentow.Contains(k);
        }
        public bool JestKonkurentem(string nazwa)
        {
            return _listaKonkurentow.Exists(k => k.Nazwa == nazwa);
        }
        public List<Konkurent> ZnajdzWszystkichKonkurentowZagrozenie(Konkurent.StopienZagrozenia zagrozenie)
        {
            return _listaKonkurentow.FindAll(k => k.Zagrozenie == zagrozenie);
        }
        public void KonkurenciSortujAlfabetycznie()
        {
            _listaKonkurentow.Sort();
        }
        #endregion

        #region Funkcje Klienci
        public void DodajKlienta(Klient k)
        {
            if (JestKlientem(k))
            {
                throw new AlreadyInListException("Klient znajduje się już na liście klientow");
            }
            _listaKlientow.Add(k);
        }
        public bool UsunKlienta(string nazwa)
        {
            if (_listaKlientow.Count() != 0 && JestKlientem(nazwa))
            {
                _listaKlientow.RemoveAt(_listaKlientow.FindIndex(k => (k.Nazwa == nazwa)));
                return true;
            }
            return false;
        }
        public bool UsunKlienta(Klient k)
        {
            if (_listaKlientow.Count() != 0 && JestKlientem(k))
            {
                _listaKlientow.Remove(k);
                return true;
            }
            return false;
        }
        public void UsunWszystkichKlientow()
        {
            _listaKlientow.Clear();
        }
        public int PodajIloscKlientow()
        {
            if (_listaPracownikow is null)
            {
                return -1;
            }
            return _listaKlientow.Count();
        }
        public bool JestKlientem(Klient k)
        {
            return _listaKlientow.Contains(k);
        }
        public bool JestKlientem(string nazwa)
        {
            return _listaKlientow.Exists(k => k.Nazwa == nazwa);
        }
        public List<Klient> ZnajdzWszystkichKlientowPlanowanyKontakt(int ZaIleDni=0)
        { 
            return _listaKlientow.FindAll(k => k.DataPlanowanegoKontaktu == DateTime.Today.AddDays(ZaIleDni));
        }
        public List<Klient> ZnajdzWszystkichKlientowOstatniKontakt(int IleDniTemu=14)
        {
            return _listaKlientow.FindAll(k => k.OstatniKontakt()<=DateTime.Today.AddDays(-IleDniTemu));
        }
        public List<Klient> ZnajdzWszystkichKlientowStatus(Status status)
        {
            return _listaKlientow.FindAll(k => k.Status == status);
        }
        public void KlienciSortujAlfabetycznie()
        {
            _listaKlientow.Sort();
        }
        public void KlienciSortujDataPlanowanegoKontaktu()
        {
            _listaKlientow.Sort((x,y)=>y.DataPlanowanegoKontaktu.CompareTo(x.DataPlanowanegoKontaktu));
        }
        public void KlienciSortujDataOstatniegoKontaktu()
        {
            _listaKlientow.Sort((x, y) => y.OstatniKontakt().CompareTo(x.OstatniKontakt()));
        }
        #endregion
        #region Zapis/Odczyt
        public override void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(OrgProwadzacaCRM));
                xml.Serialize(sw, this);
            }
        }

        public static OrgProwadzacaCRM OdczytajXML(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(OrgProwadzacaCRM));
                return (OrgProwadzacaCRM)xml.Deserialize(sr);
            }
        }


        #endregion
        #region Funkcje wypisujace
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
