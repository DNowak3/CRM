using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    enum Status { potencjalny, nowy, stały, były }
    class Klient:Organizacja
    {
        List<OsobaKontakt> _listaKontaktow;
        DateTime _dataPlanowanegoKontaktu;
        Status _status;
        Stack<Dzialanie> _dzialania;
        //Stack<Umowa> _transakcje;
        string _uwagi;

        public Klient(string nazwa, Branże branza) : base(nazwa, branza)
        {
            _listaKontaktow = new List<OsobaKontakt>();
            _dzialania = new Stack<Dzialanie>();
        }

        public Klient(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia) : base(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            _listaKontaktow = new List<OsobaKontakt>();
            _dzialania = new Stack<Dzialanie>();
            //_transakcje = new Stack<Umowa>();
        }

        public Klient(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia, string dataPlanowKontaktu, string uwagi, Status status = Status.potencjalny) : this(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(dataPlanowKontaktu, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            _uwagi = uwagi;
            _dataPlanowanegoKontaktu = d;
            _status = status;
        }

        public DateTime DataPlanowanegoKontaktu { get => _dataPlanowanegoKontaktu; set => _dataPlanowanegoKontaktu = value; }
        public string Uwagi { get => _uwagi; set => _uwagi = value; }
        internal Status Status { get => _status; set => _status = value; }

        public void DodajDzialanie(Dzialanie dzialanie)
        {
            _dzialania.Push(dzialanie);
        }

        public Dzialanie ZnajdzDzialanie(string nazwa)
        {
            foreach (Dzialanie d in _dzialania)
            {
                if (d.Nazwa == nazwa)
                {
                    return d;
                }
            }
            throw new ActionNotFoundException();
        }

        public List<Dzialanie> ZnajdzDzialania(string dataOd)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(dataOd, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.Data.CompareTo(d) >= 0));
            return dzialania.Count() == 0 ? null : dzialania;
        }

        public List<Dzialanie> ZnajdzDzialania(Pracownik pracownik)
        {
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.Pracownik.Equals(pracownik))); //wymaga Equal!!!
            return dzialania.Count() == 0 ? null : dzialania;
        }

        public List<Dzialanie> ZnajdzDzialania(OsobaKontakt osobaKontaktowa) //odwroc
        {
            List<Dzialanie> dzialania = new List<Dzialanie>(_dzialania.ToList().Where(x => x.OsobaKontaktowa.Equals(osobaKontaktowa))); //wymaga Equal!!!
            return dzialania.Count() == 0 ? null : dzialania;
        }

        public bool UsunDzialanie(string nazwa)
        {
            foreach (Dzialanie d in _dzialania)
            {
                if (d.Nazwa == nazwa)
                {
                    _dzialania = new Stack<Dzialanie>(_dzialania.Where(x => x.Nazwa != nazwa));
                    return true;
                }
            }
            return false;
        }

        public bool DodajKontakt(OsobaKontakt o)
        {
            if (_listaKontaktow.Contains(o))
            {
                Console.WriteLine($"Osoba jest już na liście kontaktów");
                return false;
            }
            _listaKontaktow.Add(o);
            return true;
        }

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
        public void UsunWszystkieKontakty()
        {
            _listaKontaktow.Clear();
        }

        public OsobaKontakt ZwrocKontakt(string imie, string nazwisko)
        {
            foreach(OsobaKontakt o in _listaKontaktow)
            {
                if(o.Imie == imie && o.Nazwisko == nazwisko)
                {
                    return o;
                }
            }
            throw new NonOrganizationMemberException();
        }

        /*public void DodajTransakcje(Umowa umowa) //sprawdzic czy umowa juz jest
        {
            _transakcje.Push(umowa);
        }

        public void UsunTransakcje(string numer) //Napisz jak będziesz znać kod do umowy
        {
            //_transakcje = new Stack<Umowa>(_transakcje.Where(x = > x.NrUmowy == numer);
        }

        public List<Umowa> ZnajdzTransakcje(string data)
        {
            string[] formatyDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DateTime.TryParseExact(data, formatyDaty, null, System.Globalization.DateTimeStyles.None, out DateTime d);
            List<Umowa> transakcje = new List<Umowa>(_transakcje.Where(x => x.Data == d));
            return transakcje.Count() == 0 ? null : transakcje;
        }

        public void AktualizujStatus() //nwm czy się przyda
        {
            if (_transakcje.Count() == 0)
            {
                Status = Status.potencjalny;
            }
            else if (_transakcje.Peek().Data.AddYear().CompareTo(DateTime.Today) > 0)
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
        }*/

        public void WypiszTransakcje() { }

        public void WypiszDzialania()
        {
            Console.WriteLine($"Działania wobec firmy {Nazwa}:");
            List < Dzialanie > dzialaniaLista = _dzialania.ToList();
            dzialaniaLista.Reverse();
            foreach (Dzialanie d in dzialaniaLista)
            {
                Console.WriteLine(d);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\n\nStatus: {Status}");
            sb.AppendLine($"Data planowanego kontaktu : {DataPlanowanegoKontaktu.ToString("dd.MM.yyyy")}");
            sb.AppendLine("ListaKontaktow:");

            int i = 0;
            foreach (OsobaKontakt o in _listaKontaktow)
            {
                i++;
                sb.AppendLine($"{i}. {o.ToString()}");
            }
            return base.ToString() + sb.ToString();
        }

    }
}
