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
            //Stworzenie głównej organizacji
            Console.WriteLine("\t\t\tGŁÓWNA ORGANIZACJA");
            OrgProwadzacaCRM nasza=new OrgProwadzacaCRM("Allegro",Organizacja.Branże.Inne,"123","Polska","miasto","2000-12-12");
            //Zapis obiektu klasy Pracownik do pliku XML oraz odczyt z pliku XML
            try
            {
                nasza.ZapiszXML("OrganizacjaCRM.xml");
                Console.WriteLine("Odczyt z pliku XML:");
                Console.WriteLine(OrgProwadzacaCRM.OdczytajXML("OrganizacjaCRM.xml"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            //Stworzenie pracownikow
            #region Pracownicy
            Console.WriteLine("\t\t\tPRACOWNICY");
            Pracownik p1 = new Pracownik("adam", "kowalski", Plcie.M, Stanowiska.konsultant, "21-11-2020", "999-999-999","kowalski@student.agh.edu.pl.com","Leniwy, nie lubi Agaty");
            Console.WriteLine("Pierwszy pracownik: "+p1);

            //Klonowanie pracownika p1
            Pracownik p2 = (Pracownik)p1.Clone();
            p2.Imie = "Patryk";
            p2.DataRozpoczeciaPracy = new DateTime(2000, 12, 2);

            //Zapis obiektu klasy Pracownik do pliku XML oraz odczyt z pliku XML
            try
            {
                p2.ZapiszXML("pracownik.xml");
                Console.WriteLine("Odczyt z pliku XML:");
                Console.WriteLine(Pracownik.OdczytajXML("pracownik.xml"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            

            //Dodawanie pracownikow
            nasza.DodajPracownika(p1);
            nasza.DodajPracownika(p2);
            nasza.DodajPracownika(new Pracownik("adam", "nowak", Plcie.M, Stanowiska.dyrekcja, "21-11-2020"));
            nasza.DodajPracownika(new Pracownik("adam", "jan", Plcie.M, Stanowiska.konsultant, "21-11-2020"));
            nasza.DodajPracownika(new Pracownik("lewa", "kowalski", Plcie.K, Stanowiska.konsultant, "21-11-2020"));

            //Sortowanie pracownikow
            nasza.PracownicySortujAlfabetycznie();
            Console.WriteLine(nasza.WypiszPracownikow());



            //Sprawdzanie czy jest pracownikiem
            Console.WriteLine(nasza.JestPracownikiem(p1));
            Console.WriteLine(nasza.JestPracownikiem("Adam", "Jan", Stanowiska.konsultant));

            //Szukanie pracowników
            Console.WriteLine("Pracownicy mężczyźni: ");
            nasza.ZnajdzWszystkichPracownikowPlec(Plcie.M).ForEach(p => Console.WriteLine(p));
            Console.WriteLine("\nPracownicy na stanowisku konsultanta: ");
            nasza.ZnajdzWszystkichPracownikowStanowisko(Stanowiska.konsultant).ForEach(p => Console.WriteLine(p));
            Console.WriteLine("\nPracownicy, którzy zaczęli pracę przed 12 grudnia 2000 roku: ");
            nasza.ZnajdzWszystkichPracownikowPracaPrzed("2000-12-12").ForEach(p => Console.WriteLine(p));

            //Usuwanie pracownikow
            nasza.UsunPracownika(p1);
            nasza.UsunPracownika("Adam", "Jan", Stanowiska.konsultant);

            //Ilosc pracownikow
            Console.WriteLine(nasza.PodajIloscPracownikow());

            //Czyszczenie listy pracowników
            nasza.UsunWszystkichPracownikow();
            Console.WriteLine(nasza.WypiszPracownikow());
            #endregion

            //Stworzenie konkurentow
            #region Konkurenci    
            Console.WriteLine("\t\t\tKONKURENCI");
            Konkurent k = new Konkurent("IBM", Organizacja.Branże.IT, "546-432-23-88", "USA", "Nowy Jork", "10.05.2011", Konkurent.StopienZagrozenia.Wysoki);
            Konkurent k2 = new Konkurent("Nokia", Organizacja.Branże.Elektronika, "732-412-93-87", "Finlandia", "Espoo", "12.05.1865", Konkurent.StopienZagrozenia.Niski);
            Console.WriteLine(k);
            Console.WriteLine(k2);

            //Przypadek w którym nr NIP jest podany w niepoprawnym formacie
            k2.Nip = "123456789";
            Console.WriteLine(k2.Nip);

            //Metoda sprawdzająca czy dwa obiekty klasy Konkurent są równe
            Console.WriteLine(Konkurent.CzyToTeSameFirmy(k, k2));

            //Zapis obiektu klasy Konkurent do pliku XML oraz odczyt z pliku XML
            k.ZapiszXML("konkurent.xml");
            Console.WriteLine("Odczyt z pliku XML:");
            Console.WriteLine(Konkurent.OdczytajXML("konkurent.xml"));

            //Dodawanie konkurentow
            nasza.DodajKonkurenta(k);
            nasza.DodajKonkurenta(k2);
            nasza.DodajKonkurenta(new Konkurent("Motorola", Organizacja.Branże.Telekomunikacja));

            //Sortowanie konkurentow
            nasza.KonkurenciSortujAlfabetycznie();
            Console.WriteLine(nasza.WypiszKonkurentow());
            Console.WriteLine(Konkurent.CzyToTeSameFirmy(k,k2));
            


            //Sprawdzanie czy jest konkurentem
            Console.WriteLine(nasza.JestKonkurentem(k));
            Console.WriteLine(nasza.JestKonkurentem(k2));
            Console.WriteLine(nasza.JestKonkurentem("Motorola"));
            Console.WriteLine(nasza.JestKonkurentem("Nokia"));

            //Szukanie konkurentów
            Console.WriteLine("Konkurenci o stopniu zagrożenia niskim: ");
            nasza.ZnajdzWszystkichKonkurentowZagrozenie(Konkurent.StopienZagrozenia.Niski).ForEach(kon => Console.WriteLine(kon));

            //Usuwanie konkurentow
            nasza.UsunKonkurenta(k);
            nasza.UsunKonkurenta("Motorola");

            //Ilosc konkurentow
            Console.WriteLine(nasza.PodajIloscKonkurentow());

            //Czyszczenie listy konkurentów
            nasza.UsunWszystkichKonkurentow();
            Console.WriteLine(nasza.WypiszKonkurentow());
            #endregion

            //Stworzenie klientow
            #region Klienci
            Console.WriteLine("\t\t\tKLIENCI");
            //Tworzenie osoby kontaktowej do naszego klienta - firmy LG
            OsobaKontakt ok1 = new OsobaKontakt("Anna", "Wiatr", Plcie.K, Stanowiska.sekretariat, "123456789","anna.wiatr@onet.com");

            //Klonowanie osoby kontaktowej
            OsobaKontakt ok2 = (OsobaKontakt)ok1.Clone();
            ok2.Nazwisko = "Lis";
            ok1.Mail = "awiatr@gmail.com";
            ok2.Mail = "alis@onet.pl";

            //Stworzenie klientow
            Klient LG = new Klient("LG", Organizacja.Branże.Elektronika, "121-252-15-14", "Korea", "Seul", "01.01.1960", "15.12.2020", "", Status.nowy);
            Klient MoneyPL = new Klient("MoneyMoney", Organizacja.Branże.Finanse, "124-242-14-11", "Polska", "Katowice", "01.01.1999", "01.12.2020", "", Status.stały);

            //Dodanie osób kontaktowych do klientów
            LG.DodajKontakt(ok1);
            LG.DodajKontakt(ok2);

            //Posortowanie listy osób kontaktowych
            LG.SortujKontakty();

            //Stworzenie działań
            Dzialanie d1 = new Dzialanie("Umówienie na spotkanie", "08.12.2020", p1, LG.ZwrocKontakt("Anna", "Wiatr"), WynikDzialania.umowiono, " ");
            Dzialanie d2 = new Dzialanie("Negocjacje", "01.12.2020");
            Dzialanie d3 = new Dzialanie("Wpłata za zamówienie", "28.12.2020");

            //Klonowanie działań
            Dzialanie d4 = (Dzialanie)d1.Clone();
            d4.Nazwa = "Sprzedaż towarów";
            d4.Wynik = WynikDzialania.ukonczono;

            //Dodanie działań do klienta
            LG.DodajDzialanie(d1);
            LG.DodajDzialanie(d2);
            LG.DodajDzialanie(d3);
            LG.DodajDzialanie(d4);

            //Posortowanie działań od najnowszych do najstarszych
            LG.SortujDzialania();

            //Wypisanie klienta
            Console.WriteLine("\nKlient LG:");
            Console.WriteLine(LG);

            //Wypisanie działań klienta LG
            Console.WriteLine("\nDziałania z LG (posortowane):");
            LG.WypiszDzialania();

            //Dodanie klientow 
            nasza.DodajKlienta(LG);
            nasza.DodajKlienta(MoneyPL);
            nasza.DodajKlienta(new Klient("Olek-Moto", Organizacja.Branże.Motoryzacja));

            //Sortowanie klientow
            Console.WriteLine("Klienci alfabetycznie:");
            nasza.KlienciSortujAlfabetycznie();
            Console.WriteLine(nasza.WypiszKlientow());
            Console.WriteLine("Klienci po dacie ostatniego kontaktu");
            nasza.KlienciSortujDataOstatniegoKontaktu();
            Console.WriteLine(nasza.WypiszKlientow());
            Console.WriteLine("Klienci po dacie planowanego kontaktu");
            nasza.KlienciSortujDataPlanowanegoKontaktu();
            Console.WriteLine(nasza.WypiszKlientow());

            //Sprawdzanie czy jest klientem
            Console.WriteLine(nasza.JestKlientem(LG));
            Console.WriteLine(nasza.JestKlientem("Olek-Moto"));
            Console.WriteLine(nasza.JestKlientem("AdamEX"));

            //Szukanie klientów
            Console.WriteLine("Klienci, z którymi nie kontaktowano się trzy tygodnie:");
            nasza.ZnajdzWszystkichKlientowOstatniKontakt(21).ForEach(kl => Console.WriteLine(kl));
            Console.WriteLine("Klienci, do których planowany kontakt jest oznaczony jako dzisiaj:");
            nasza.ZnajdzWszystkichKlientowPlanowanyKontakt().ForEach(kl => Console.WriteLine(kl));
            Console.WriteLine("Klienci o statusie \"były\":");
            nasza.ZnajdzWszystkichKlientowStatus(Status.były).ForEach(kl => Console.WriteLine(kl));


            //Usuwanie klientów
            nasza.UsunKlienta(LG);
            nasza.UsunKlienta("Olek-Moto");

            //Ilosc konkurentow
            Console.WriteLine(nasza.PodajIloscKlientow());

            //Czyszczenie listy konkurentów
            nasza.UsunWszystkichKlientow();
            Console.WriteLine(nasza.WypiszKlientow());
            #endregion

            //Stworzenie produktow
            #region Produkty
            Console.WriteLine("\t\t\tKLIENCI");
            //Tworzenie obiektow klasy Produkt
            Produkt pr1 = new Produkt("Papier kancelaryjny", 10.50, Jednostki.kg);
            Produkt pr2 = new Produkt("Materiały biurowe", 5.60);
            Produkt pr3 = new Produkt("Komputer", 1000);

            //Wypisanie produktow
            Console.WriteLine("\nProdukty:");
            Console.WriteLine(pr1);
            Console.WriteLine(pr2);
            Console.WriteLine(pr3);

            //Stworzenie umowy
            Umowa u1 = new Umowa(p1);

            //Dodanie produktow do umowy
            u1.DodajProdukt(pr1, 5);
            u1.DodajProdukt(pr2, 2);
            u1.DodajProdukt(pr3, 1);

            //Wypisanie umowy
            Console.WriteLine("\nUmowa:");
            Console.WriteLine(u1);

            //Wprowadzanie zmian w umowie
            u1.DodajProdukt(pr2, 4.6);
            u1.UsunProdukt(pr1);
            u1.ZmienIloscKod("K20203", 2);

            //Wypisanie umowy po modyfikacji
            Console.WriteLine("\nUmowa po modyfikacji:");
            Console.WriteLine(u1);

            u1.UsunProduktKod("M20202");
            u1.ZmienIlosc(pr3, 4);
            Console.WriteLine("\nUmowa po modyfikacji:");
            Console.WriteLine(u1);

            //Dodanie produktów 
            nasza.DodajProdukt(pr1);
            nasza.DodajProdukt(pr2);
            nasza.DodajProdukt(pr3);

            //Sortowanie produktów
            Console.WriteLine("Produkty po cenie rosnąco:");
            nasza.ProduktySortujPoCenie();
            Console.WriteLine(nasza.WypiszProdukty());
            Console.WriteLine("Produkty po cenie malejąco:");
            nasza.ProduktySortujPoCenie(false);
            Console.WriteLine(nasza.WypiszProdukty());
            Console.WriteLine("Produkty po nazwie:");
            nasza.ProduktySortujPoNazwie();
            Console.WriteLine(nasza.WypiszProdukty());

            //Sprawdzanie czy jest produktem
            Console.WriteLine(nasza.JestProduktem(pr1));
            Console.WriteLine(nasza.JestProduktem("M20202"));

            //Szukanie produktów
            Console.WriteLine("Wsztstkie produkty tańsze niż 20:");
            nasza.ZnajdzWszystkieProduktyCena(20,true).ForEach(pr => Console.WriteLine(pr));
            Console.WriteLine("Wsztstkie produkty droższe niż 10:");
            nasza.ZnajdzWszystkieProduktyCena(10).ForEach(pr => Console.WriteLine(pr));
            Console.WriteLine("Wszystkie produkty o jednostce szt:");
            nasza.ZnajdzWszystkieProduktyJednostka(Jednostki.szt).ForEach(pr => Console.WriteLine(pr));

            //Usuwanie produktów
            nasza.UsunProdukt(pr1);
            nasza.UsunProdukt("Komputer");

            //Ilosc produktow
            Console.WriteLine(nasza.PodajIloscProduktow());

            //Czyszczenie listy konkurentów
            nasza.UsunWszystkieProdukty();
            Console.WriteLine(nasza.WypiszProdukty());
            #endregion

            Console.ReadKey();
        }
    }
}
