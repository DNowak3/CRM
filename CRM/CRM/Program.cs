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
            Pracownik p1 = new Pracownik("adam", "kowalski", Plcie.M, Stanowiska.konsultant, "21-11-2020");
            Pracownicy.Dodaj(p1);
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
            //Metoda sprawdzająca czy dwa obiekty klasy Konkurent są równe
            Console.WriteLine(Konkurent.CzyToTeSameFirmy(k,k2));

            //tworzenie osoby kontaktowej do naszego klienta - firmy LG
            OsobaKontakt ok1 = new OsobaKontakt("Anna", "Wiatr", Plcie.K, Stanowiska.sekretariat, "123456789");

            Klient LG = new Klient("LG", Organizacja.Branże.Elektronika, "121-252-15-14", "Korea", "Seul", "01.01.1960", "15.12.2020", "", Status.nowy);
            LG.DodajKontakt(ok1);

            Dzialanie d1 = new Dzialanie("Umówienie na spotkanie", "08.12.2020", p1, LG.ZwrocKontakt("Anna", "Wiatr"), WynikDzialania.umowiono, " ");
            Dzialanie d2 = new Dzialanie("dzialanie2", "01.12.2020");
            Dzialanie d3 = new Dzialanie("dzialanie3", "28.12.2020");

            LG.DodajDzialanie(d1);
            LG.DodajDzialanie(d2);
            LG.DodajDzialanie(d3);

            Console.WriteLine("\nKlient LG:");
            Console.WriteLine(LG);

            Console.WriteLine("\nDziałania z LG:");
            LG.WypiszDzialania();

            Console.ReadKey();
        }
    }
}
