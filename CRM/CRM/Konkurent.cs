using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CRM
{
    public class Konkurent : Organizacja
    {
        public enum StopienZagrozenia { Niski, BardzoNiski, Średni, Wysoki, BardzoWysoki }
        StopienZagrozenia zagrozenie;

        internal StopienZagrozenia Zagrozenie { get => zagrozenie; set => zagrozenie = value; }

        public Konkurent() : base() { }
        public Konkurent(string nazwa, Branże branza) : base(nazwa, branza) { }
        public Konkurent(string nazwa, Branże branza, string nip, string kraj, string miasto, string dataZalozenia, StopienZagrozenia zagrozenie) : base(nazwa, branza, nip, kraj, miasto, dataZalozenia)
        {
            Zagrozenie = zagrozenie;
        }

        public static bool CzyToTeSameFirmy(Konkurent k1, Konkurent k2)
        {
            if (k1.Equals(k2))
                return true;
            return false;
        }

        public override void ZapiszXML(string nazwa)
        {
            using (StreamWriter sw = new StreamWriter(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Konkurent));
                xml.Serialize(sw, this);
            }
        }

        public static Konkurent OdczytajXML(string nazwa) 
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using (StreamReader sr = new StreamReader(nazwa))
            {
                XmlSerializer xml = new XmlSerializer(typeof(Konkurent));
                return (Konkurent)xml.Deserialize(sr);
            }
        }


        public override string ToString()
        {
            return base.ToString() + $" (stopień zagrożenia: {Zagrozenie})";
        }

    }
}
