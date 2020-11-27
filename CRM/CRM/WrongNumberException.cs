using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class WrongNumberException:Exception
    {
        public WrongNumberException() : base() { }
        public WrongNumberException(string komunikat) : base(komunikat) { }
    }
}
