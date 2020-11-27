using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{

    
    class Program
    {
        static void Main()
        {
            Pracownicy.Dodaj(new Pracownik("adam", "kowalski",Plcie.M,Stanowiska.konsultant,"21-11-2020"));
            Pracownicy.Dodaj(new Pracownik("adam", "nowak", Plcie.M, Stanowiska.konsultant, "21-11-2020"));
            Pracownicy.Dodaj(new Pracownik("adam", "jan", Plcie.M, Stanowiska.konsultant, "21-11-2020"));
            Pracownicy.Dodaj(new Pracownik("lewy", "kowalski",Plcie.M,Stanowiska.konsultant,"21 - 11 - 2020"));

            Console.WriteLine(Pracownicy.ToString());
            Console.ReadKey();
        }
    }
}
