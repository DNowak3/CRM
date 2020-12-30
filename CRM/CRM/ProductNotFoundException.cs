using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public class ProductNotFoundException :Exception
    {
        public ProductNotFoundException() : base() { }
        public ProductNotFoundException(string komunikat) : base(komunikat) { }
    }
}
