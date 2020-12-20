using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public class AlreadyInListException:Exception
    {
        public AlreadyInListException() : base() { }
        public AlreadyInListException( string komunikat) : base(komunikat) { }
    }
}
