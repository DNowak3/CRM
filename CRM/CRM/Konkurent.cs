using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class Konkurent : Organizacja
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

        public override string ToString()
        {
            return base.ToString() + $" (stopień zagrożenia: {Zagrozenie})";
        }

    }
}
