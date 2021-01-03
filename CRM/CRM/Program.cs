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
            OrgProwadzacaCRM nasza=new OrgProwadzacaCRM("Allegro",Branże.Inne,"123","Polska","Warszawa","2000-12-12");
            OrgProwadzacaCRM klonowana1 = (OrgProwadzacaCRM)nasza.Clone();

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

            //Zapis obiektu klasy OrgProwadzacaCRM do pliku XML oraz odczyt z pliku XML
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
            //Zapis obiektu klasy OrgProwadzacaCRM do pliku JSON oraz odczyt z pliku JSON
            try
            {
                nasza.ZapiszJSON("OrganizacjaCRM.json");
                Console.WriteLine("Odczyt z pliku JSON:");
                Console.WriteLine(OrgProwadzacaCRM.OdczytajJSON("OrganizacjaCRM.json"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }

            OrgProwadzacaCRM klonowana=(OrgProwadzacaCRM)nasza.Clone();

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
            Konkurent k = new Konkurent("IBM",Branże.IT, "546-432-23-88", "USA", "Nowy Jork", "10.05.2011", Konkurent.StopienZagrozenia.Wysoki);
            Konkurent k2 = new Konkurent("Nokia", Branże.Elektronika, "732-412-93-87", "Finlandia", "Espoo", "12.05.1865", Konkurent.StopienZagrozenia.Niski);
            Console.WriteLine(k);
            Console.WriteLine(k2);

            //Przypadek w którym nr NIP jest podany w niepoprawnym formacie
            k2.Nip = "123456789";
            Console.WriteLine(k2.Nip);

            //Metoda sprawdzająca czy dwa obiekty klasy Konkurent są równe
            Console.WriteLine(Konkurent.CzyToTeSameFirmy(k, k2));

            //Zapis obiektu klasy Konkurent do pliku XML oraz odczyt z pliku XML
            k.ZapiszXML("konkurent.xml");
            Console.WriteLine("\nOdczyt z pliku XML:");
            Console.WriteLine(Konkurent.OdczytajXML("konkurent.xml"));

            //Zapis obiektu klasy Konkurent do pliku JSON oraz odczyt z pliku JSON
            k.ZapiszJSON("konkurent.json");
            Console.WriteLine("Odczyt z pliku JSON:");
            Console.WriteLine(Konkurent.OdczytajJSON("konkurent.json"));
            Console.WriteLine();

            //Dodawanie konkurentow
            nasza.DodajKonkurenta(k);
            nasza.DodajKonkurenta(k2);
            nasza.DodajKonkurenta(new Konkurent("Motorola",Branże.Telekomunikacja));
            nasza.DodajKonkurenta(new Konkurent("Motorola", Branże.Telekomunikacja));

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
            //Tworzenie osoby kontaktowej do naszego klienta
            OsobaKontakt ok1 = new OsobaKontakt("Anna", "Wiatr", Plcie.K, Stanowiska.sekretariat, "123456789","anna.wiatr@onet.com");

            //Klonowanie osoby kontaktowej
            OsobaKontakt ok2 = (OsobaKontakt)ok1.Clone();
            ok2.Nazwisko = "Lis";
            ok1.Mail = "awiatr@gmail.com";
            ok2.Mail = "alis@onet.pl";

            //Stworzenie klientow
            Klient LG = new Klient("LG",Branże.Elektronika, "121-252-15-14", "Korea", "Seul", "01.01.1960", "15.12.2020", "", Status.nowy);
            Klient MoneyPL = new Klient("MoneyMoney", Branże.Finanse, "124-242-14-11", "Polska", "Katowice", "01.01.1999", "01.12.2020", "", Status.stały);

            //Dodanie osób kontaktowych do klientów
            LG.DodajKontakt(ok1);
            LG.DodajKontakt(ok2);

            //Posortowanie listy osób kontaktowych
            LG.SortujKontakty();

            //Dodanie klientów do naszej organizacji
            nasza.DodajKlienta(new Klient("Facebook", Branże.Media, "456-456-56-56", "USA", "New York", "20.05.2003", "12.01.2021", "Wazny klient", Status.stały));
            nasza.DodajKlienta(new Klient("Castorama", Branże.Inne, "456-457-56-56", "Francja", "Paryż", "20.05.1996", "10.01.2021", "", Status.były));


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
            MoneyPL.DodajDzialanie(d3);
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
            nasza.DodajKlienta(new Klient("Olek-Moto",Branże.Motoryzacja));
            nasza.DodajKlienta(new Klient("Olek-Moto", Branże.Motoryzacja));

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

            //Ilosc klientow
            Console.WriteLine(nasza.PodajIloscKlientow());

            //Czyszczenie listy klientow
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
            nasza.ProduktySortujPoKodzie();
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



            //Stworzenie przykladowego pliku organizacji NaszaOrganizacja.xml ze wszystkimi informacjami do wykorzystania w GUI
            OrgProwadzacaCRM NaszaFirma = new OrgProwadzacaCRM("Allegro", Branże.Inne, "454-454-56-56", "Polska", "Poznań", "ul. Szeroka 1", "30-250", "Najpopularniejszy serwis aukcyjny w Polsce", "01.01.1999");
            NaszaFirma.DodajPracownika(new Pracownik("Adam", "Nowak", Plcie.M, Stanowiska.dyrekcja, "21-11-2020", "123-456-789", "a.nowak@gmail.com"));
            NaszaFirma.DodajPracownika(new Pracownik("Witold", "Kowalski", Plcie.M, Stanowiska.konsultant, "21-10-2020", "123-400-789", "w.kowalski@gmail.com", "Spóźnia się do pracy"));
            NaszaFirma.DodajPracownika(new Pracownik("Julia", "Kot", Plcie.K, Stanowiska.sekretariat, "01.01-2020", "100-400-789", "j.kot@gmail.com"));
            NaszaFirma.DodajPracownika(new Pracownik("Anna", "Lis", Plcie.K, Stanowiska.sprzedawca, "01.01-2019", "100-400-300", "a.lis@gmail.com", "Sumienny pracownik"));

            nasza.DodajKonkurenta(new Konkurent("AliExpress", Branże.Inne));
            nasza.DodajKonkurenta(new Konkurent("Wish", Branże.Inne));
            nasza.DodajKonkurenta(new Konkurent("Motorola", Branże.Telekomunikacja));
            nasza.DodajKonkurenta(new Konkurent("Media Expert", Branże.Elektronika));

            Klient fb = new Klient("Facebook", Branże.Media, "456-456-56-56", "USA", "New York", "20.05.2003", "12.01.2021", "Ważny klient", Status.stały);
            Klient castorama = new Klient("Castorama", Branże.Inne, "456-457-56-56", "Francja", "Paryż", "20.05.1996", "10.01.2021", "", Status.były);
            Klient olek = new Klient("Olek-Moto", Branże.Motoryzacja, "789-789-89-89", "Polska", "Warszawa", "12.04.2002", "25.01.2021", "", Status.stały);
            Klient OLX = new Klient("OLX", Branże.Inne, "123-123-78-78", "Polska", "Kraków", "02.04.2001", "26.01.2021", "", Status.nowy);
            Klient tax = new Klient("Biuro podatkowe TAX", Branże.Finanse, "123-456-96-96", "Polska", "Lublin", "02.04.2001", "13.01.2021", "", Status.były);


            Dzialanie d_1 = new Dzialanie("Pożyczka", "28.12.2020", WynikDzialania.ukonczono, "Zawarcie pożcyzki na kwotę 10 000 zł");
            Dzialanie d_2 = new Dzialanie("Spotkanie", "18.12.2020", WynikDzialania.anulowano, "Klient anulował spotkanie");
            Dzialanie d_3 = new Dzialanie("Konferencja", "11.10.2020", WynikDzialania.ukonczono, "Konferencja w ważnej sprawie");
            Dzialanie d_4 = new Dzialanie("Podpisanie umowy", "27.02.2021", WynikDzialania.zaplanowano, "Zaplanowano podpisanie umowy najmu nowego lokalu");
            Dzialanie d_5 = new Dzialanie("Zapłata za zamówienie", "06.11.2019");
            Dzialanie d_6 = new Dzialanie("Zapłata za usługi", "18.03.2019");
            Dzialanie d_7 = new Dzialanie("Rozmowa telefoniczna", "18.12.2020");
            Dzialanie d_8 = new Dzialanie("Spłata raty pożyczki", "10.02.2021", WynikDzialania.zaplanowano, "");

            OsobaKontakt o_1 = new OsobaKontakt("Patrycja", "Sosna", Plcie.K, Stanowiska.dyrekcja, "123-456-789", "p.sosna@gmail.com");
            OsobaKontakt o_2 = new OsobaKontakt("Piotr", "Drwal", Plcie.M, Stanowiska.konsultant, "123-456-789", "p.drwal@gmail.com");
            OsobaKontakt o_3 = new OsobaKontakt("Karol", "Lasowski", Plcie.M, Stanowiska.sekretariat, "123-456-789", "k.lasowski@gmail.com");
            OsobaKontakt o_4 = new OsobaKontakt("Mateusz", "Piotrowski", Plcie.M, Stanowiska.sprzedawca, "123-456-789", "m.piotrowski@gmail.com");
            OsobaKontakt o_5 = new OsobaKontakt("Kinga", "Brzoza", Plcie.K, Stanowiska.sekretariat, "123-456-789", "k.brzoza@gmail.com");

            fb.DodajDzialanie(d_1);
            fb.DodajDzialanie(d_2);
            fb.DodajDzialanie(d_8);
            castorama.DodajDzialanie(d_4);
            castorama.DodajDzialanie(d_3);
            olek.DodajDzialanie(d_5);
            OLX.DodajDzialanie(d_6);
            tax.DodajDzialanie(d_7);
            MoneyPL.DodajDzialanie(d_3);

            MoneyPL.DodajKontakt(o_5);
            fb.DodajKontakt(o_1);
            castorama.DodajKontakt(o_2);
            castorama.DodajKontakt(o_3);
            olek.DodajKontakt(o_4);
            OLX.DodajKontakt(o_5);
            tax.DodajKontakt(o_2);

            NaszaFirma.DodajKlienta(fb);
            NaszaFirma.DodajKlienta(castorama);
            NaszaFirma.DodajKlienta(tax);
            NaszaFirma.DodajKlienta(olek);
            NaszaFirma.DodajKlienta(OLX);
            NaszaFirma.DodajKlienta(MoneyPL);

            NaszaFirma.DodajProdukt(pr1);
            NaszaFirma.DodajProdukt(pr2);
            NaszaFirma.DodajProdukt(pr3);
            NaszaFirma.ZapiszXML("NaszaFirma.xml");

            try
            {
                NaszaFirma.ZapiszXML("NaszaFirma.xml");
                Console.WriteLine("Odczyt z pliku XML:");
                Console.WriteLine(OrgProwadzacaCRM.OdczytajXML("NaszaFirma.xml"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }

            Console.ReadKey();
        }
    }
}
