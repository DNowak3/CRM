using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Klasa definiujaca wyjatek, oznaczajacy, ze dane dzialanie nie zostalo znalezione.
    /// </summary>
    public class ActionNotFoundException:Exception
    {
        /// <summary>
        /// Konstruktor wyjatku, wywolujacy konstruktor z klasy bazowej.
        /// </summary>
        public ActionNotFoundException() : base() { }
    }
}
