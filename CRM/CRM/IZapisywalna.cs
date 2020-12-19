using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    interface IZapisywalna
    {
        void ZapiszXML(string nazwa);
        //Organizacja OdczytajXML(string nazwa); jeśli metoda do odczytu będzie w interfejsie to nie może być statyczna więc chyba się nie da (?)
    }
}
