using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    /// <summary>
    /// Klasa tworzaca wyjatek, oznaczajacy ze dana osoba nie jest czlonkiem organizacji.
    /// </summary>
    public class NonOrganizationMemberException:Exception
    {
        /// <summary>
        /// Konstruktor, wysolujacy konstruktor klasy bazowej.
        /// </summary>
        public NonOrganizationMemberException() : base() { }
    }
}
