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
            Pracownicy.Dodaj(new Pracownik("adam", "kowalski",Plcie.M,Stanowiska.konsultant,"21-11-2020","0000"));
            Pracownicy.Dodaj(new Pracownik("adam", "nowak", Plcie.M, Stanowiska.dyrekcja, "21-11-2020"));
            Pracownicy.Dodaj(new Pracownik("adam", "jan", Plcie.M, Stanowiska.konsultant, "21-11-2020"));
            Pracownicy.Dodaj(new Pracownik("lewy", "kowalski",Plcie.M,Stanowiska.konsultant,"21 - 11 - 2020"));

            Console.WriteLine(Pracownicy.ToString());
            Pracownicy.Sortuj();
            Console.WriteLine(Pracownicy.ToString());
            Pracownicy.Usun("Adam", "Kowalski", Stanowiska.konsultant);
            Console.WriteLine(Pracownicy.ToString());
            Pracownicy.ZnajdzWszystkichNaStanowisku(Stanowiska.dyrekcja).ForEach(p => Console.WriteLine(p));


            Console.ReadKey();
        }
    }
}
