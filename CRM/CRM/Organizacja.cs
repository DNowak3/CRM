using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM
{
    abstract class Organizacja : IComparable<Organizacja>, IEquatable<Organizacja>, ICloneable
    {
        Regex wzorNip = new Regex(@"^\d{3}-\d{3}-\d{2}-\d{2}$");
        public enum Branże { IT, Finanse, Elektronika, Telekomunikacja, Motoryzacja, Media, Energetyka, Inne }

        string nazwa;
        Branże branza;
        string nip;
        DateTime dataZalozenia;
        string kraj;
        string miasto;
        string adres;
        string kodPocztowy;
        string notatki;

        public string Nip
        {
            get
            {
                return nip;
            }
            set
            {
                if (wzorNip.IsMatch(value))
                    nip = value;
                else
                    nip = "000-000-00-00";
            }
        }     
        public string Nazwa { get => nazwa; set => nazwa = value; }
        private Branże Branza { get => branza; set => branza = value; }
        public string Kraj { get => kraj; set => kraj = value; }
        public string Miasto { get => miasto; set => miasto = value; }
        public string Adres { get => adres; set => adres = value; }
        public string KodPocztowy { get => kodPocztowy; set => kodPocztowy = value; }
        public string Notatki { get => notatki; set => notatki = value; }
        public DateTime DataZalozenia { get => dataZalozenia; set => dataZalozenia = value; }

        public Organizacja()
        {
            Nazwa = string.Empty;
            Branza = Branże.Inne;
            Adres = string.Empty;
            KodPocztowy = string.Empty;
            Notatki = string.Empty;
        }

        public Organizacja(string nazwa, Branże branza) : this()
        {
            Nazwa = nazwa;
            Branza = branza;
        }

        public Organizacja(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia) : this(nazwa, branza)
        {
            Nip = nip;
            Kraj = kraj;
            Miasto = miasto;
            string[] formatDaty = { "dd.MM.yyyy", "dd.MMM.yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy", "dd-MMM-yyyy" };
            DataZalozenia = DateTime.ParseExact(dataZalozenia, formatDaty, null, System.Globalization.DateTimeStyles.None);
        }

        public int CompareTo(Organizacja other)     //porównywanie nazwy
        {
            return Nazwa.CompareTo(other.Nazwa);
        }

        public bool Equals(Organizacja other)       //organizacje są równe gdy posiadają takie same nazwy, branże i adresy nip
        {
            if (Nazwa.Equals(other.Nazwa) && Branza.Equals(other.Branza) && Nip.Equals(other.Nazwa))
                return true;
            return false;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Nazwa.ToUpper()} ({Branza}) {Nip} {Kraj}: {Miasto}, data założenia: {DataZalozenia.ToShortDateString()}";
        }
    }
}
