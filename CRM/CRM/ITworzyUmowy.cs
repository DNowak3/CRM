using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    interface ITworzyUmowy
    {
        double Koszt();
        Produkt ZnajdzProdukt(string kod);
        double JesliSztuki(Produkt produkt, double ilosc);
        void DodajProdukt(Produkt produkt, double ilosc);
        bool UsunProdukt(Produkt produkt);
        bool UsunProduktKod(string kod);
        bool ZmienIlosc(Produkt produkt, double ilosc);
        bool ZmienIloscKod(string kod, double ilosc);
    }
}
