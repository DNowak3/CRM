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

            Console.WriteLine();

            //Tworzenie obiektów klasy Konkurent
            Konkurent k = new Konkurent("IBM", Organizacja.Branże.IT, "546-432-23-88", "USA", "Nowy Jork", "10.05.2011", Konkurent.StopienZagrozenia.Wysoki);
            Konkurent k2 = new Konkurent("Nokia", Organizacja.Branże.Elektronika, "732-412-93-87", "Finlandia", "Espoo", "12.05.1865", Konkurent.StopienZagrozenia.Niski);
            Console.WriteLine(k);
            Console.WriteLine(k2);
            //Przypadek w którym nr NIP jest podany w niepoprawnym formacie
            k2.Nip = "123456789";
            Console.WriteLine(k2.Nip);

            Console.ReadKey();
        }
    }
}
