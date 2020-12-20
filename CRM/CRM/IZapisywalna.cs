using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Interfejs definiujący metodę, która umożliwia zapis danych do pliku XML
    /// </summary>
    interface IZapisywalna
    {
        /// <summary>
        /// Wcześniej wspomniana metoda do zapisu w pliku XML
        /// </summary>
        /// <param name="nazwa"> parametrem jest nazwa pliku </param>
        void ZapiszXML(string nazwa);
    }
}
