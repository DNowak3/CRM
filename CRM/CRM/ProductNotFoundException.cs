using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Klasa tworząca wyjątek, gdy produkt nie został znaleziony w umowie
    /// </summary>
    public class ProductNotFoundException : Exception
    {
        /// <summary>
        /// Konstruktor wyjątku nieparametryczny
        /// </summary>
        public ProductNotFoundException() : base() { }
        /// <summary>
        /// Konstruktor wyjątku z komunikatem
        /// </summary>
        /// <param name="komunikat">Komunikat do wyświetlenia w razie wystąpienia wyjątku</param>
        public ProductNotFoundException(string komunikat) : base(komunikat) { }
    }
}
