using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Interfejs definiujący metodę, która umożliwia zapis danych do pliku XML oraz do pliku JSON
    /// </summary>
    interface IZapisywalna
    {
        /// <summary>
        /// Metoda do zapisu w pliku XML
        /// </summary>
        /// <param name="nazwa"> parametrem jest nazwa pliku </param>
        void ZapiszXML(string nazwa);
        /// <summary>
        /// Metoda do zapisu w pliku JSON
        /// </summary>
        /// <param name="nazwa"></param>
        void ZapiszJSON(string nazwa);
    }
}
