using System;
using CRM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyOrgProwadzacaCRM
    {
        [TestMethod]
        public void DodawaniePracownikow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            Pracownik p1 = new Pracownik("Mateusz", "Nowak", Plcie.M, Stanowiska.sprzedawca);
            temp.DodajPracownika(p1);
            Assert.AreEqual(licznik, temp.PodajIloscPracownikow());
            try
            {
                temp.DodajPracownika(p1);
                Assert.Fail();
            }
            catch (AlreadyInListException) { }
        }

        [TestMethod]
        public void DodawanieProduktow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            Produkt pr1 = new Produkt("Walizka", 199.99, Jednostki.szt);
            temp.DodajProdukt(pr1);
            Assert.AreEqual(licznik, temp.PodajIloscProduktow());
            try
            {
                temp.DodajProdukt(pr1);
                Assert.Fail();
            }
            catch (AlreadyInListException) { }
        }
        [TestMethod]
        public void DodawanieKonkurentow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            Konkurent konk1 = new Konkurent("Biedronka", Branże.Inne);
            temp.DodajKonkurenta(konk1);
            Assert.AreEqual(licznik, temp.PodajIloscKonkurentow());
            try
            {
                temp.DodajKonkurenta(konk1);
                Assert.Fail();
            }
            catch (AlreadyInListException) { }
        }
        [TestMethod]
        public void DodawanieKlientow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            Klient kl1 = new Klient("Amazon", Branże.Inne);
            temp.DodajKlienta(kl1);
            Assert.AreEqual(licznik, temp.PodajIloscKlientow());
            try
            {
                temp.DodajKlienta(kl1);
                Assert.Fail();
            }
            catch (AlreadyInListException) { }
        }
        [TestMethod]
        public void UsuwaniePracownika()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Pracownik p1 = new Pracownik("Mateusz", "Nowak", Plcie.M, Stanowiska.sprzedawca);
            Pracownik p2 = new Pracownik("Aleksandra", "Senior", Plcie.K, Stanowiska.dyrekcja);
            Pracownik p3 = new Pracownik("Edwin", "Kolos", Plcie.K, Stanowiska.konsultant);

            temp.DodajPracownika(p1);
            temp.DodajPracownika(p2);
            Assert.AreEqual(true, temp.UsunPracownika(p1));
            Assert.AreEqual(1, temp.PodajIloscPracownikow());
            Assert.AreEqual(false, temp.UsunPracownika(p1));

            temp.DodajPracownika(p3);
            Assert.AreEqual(true, temp.UsunPracownika("Aleksandra", "Senior", Stanowiska.dyrekcja));
            Assert.AreEqual(1, temp.PodajIloscPracownikow());
            Assert.AreEqual(false, temp.UsunPracownika("Aleksandra", "Senior", Stanowiska.dyrekcja));

            temp.DodajPracownika(p1);
            temp.DodajPracownika(p2);
            temp.UsunWszystkichPracownikow();
            Assert.AreEqual(0, temp.PodajIloscPracownikow());
        }
        [TestMethod]
        public void UsuwanieProduktow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Produkt p1 = new Produkt("Myszka", 12.20, Jednostki.szt);
            Produkt p2 = new Produkt("Klawiatura", 39.99, Jednostki.szt);
            Produkt p3 = new Produkt("Komputer", 1999.99, Jednostki.szt);

            temp.DodajProdukt(p1);
            temp.DodajProdukt(p2);
            Assert.AreEqual(true, temp.UsunProdukt(p1));
            Assert.AreEqual(1, temp.PodajIloscProduktow());
            Assert.AreEqual(false, temp.UsunProdukt(p1));

            temp.DodajProdukt(p3);
            Assert.AreEqual(true, temp.UsunProdukt(p2.Kod));
            Assert.AreEqual(1, temp.PodajIloscProduktow());
            Assert.AreEqual(false, temp.UsunProdukt(p2.Kod));

            temp.DodajProdukt(p1);
            temp.DodajProdukt(p2);
            temp.UsunWszystkieProdukty();
            Assert.AreEqual(0, temp.PodajIloscProduktow());
        }
        [TestMethod]
        public void UsuwanieKonkurentow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Konkurent k1 = new Konkurent("BudEx", Branże.Inne);
            Konkurent k2 = new Konkurent("AdamEx", Branże.Motoryzacja);
            Konkurent k3 = new Konkurent("AutoMoto",Branże.Motoryzacja);

            temp.DodajKonkurenta(k1);
            temp.DodajKonkurenta(k2);
            Assert.AreEqual(true, temp.UsunKonkurenta(k1));
            Assert.AreEqual(1, temp.PodajIloscKonkurentow());
            Assert.AreEqual(false, temp.UsunKonkurenta(k1));

            temp.DodajKonkurenta(k3);
            Assert.AreEqual(true, temp.UsunKonkurenta("AdamEx"));
            Assert.AreEqual(1, temp.PodajIloscKonkurentow());
            Assert.AreEqual(false, temp.UsunKonkurenta("AdamEx"));

            temp.DodajKonkurenta(k1);
            temp.DodajKonkurenta(k2);
            temp.UsunWszystkichKonkurentow();
            Assert.AreEqual(0, temp.PodajIloscKonkurentow());
        }
        [TestMethod]
        public void UsuwanieKlientow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Klient k1 = new Klient("BudEx", Branże.Inne);
            Klient k2 = new Klient("AdamEx", Branże.Motoryzacja);
            Klient k3 = new Klient("AutoMoto",Branże.Motoryzacja);

            temp.DodajKlienta(k1);
            temp.DodajKlienta(k2);
            Assert.AreEqual(true, temp.UsunKlienta(k1));
            Assert.AreEqual(1, temp.PodajIloscKlientow());
            Assert.AreEqual(false, temp.UsunKlienta(k1));

            temp.DodajKlienta(k3);
            Assert.AreEqual(true, temp.UsunKlienta("AdamEx"));
            Assert.AreEqual(1, temp.PodajIloscKlientow());
            Assert.AreEqual(false, temp.UsunKlienta("AdamEx"));

            temp.DodajKlienta(k1);
            temp.DodajKlienta(k2);
            temp.UsunWszystkichKlientow();
            Assert.AreEqual(0, temp.PodajIloscKlientow());
        }
        [TestMethod]
        public void JestPracownikiem()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Pracownik p1 = new Pracownik("Mateusz", "Nowak", Plcie.M, Stanowiska.sprzedawca);
            Pracownik p2 = new Pracownik("Aleksandra", "Senior", Plcie.K, Stanowiska.dyrekcja);
            Pracownik p3 = new Pracownik("Edwin", "Kolos", Plcie.K, Stanowiska.konsultant);

            temp.DodajPracownika(p1);
            temp.DodajPracownika(p2);
            Assert.AreEqual(true, temp.JestPracownikiem(p1));
            Assert.AreEqual(false, temp.JestPracownikiem(p3));
            Assert.AreEqual(true, temp.JestPracownikiem("Aleksandra", "Senior", Stanowiska.dyrekcja));
            Assert.AreEqual(false, temp.JestPracownikiem("Edwin", "Kolos", Stanowiska.konsultant));
        }
        [TestMethod]
        public void JestProduktem()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Produkt p1 = new Produkt("Myszka", 12.20, Jednostki.szt);
            Produkt p2 = new Produkt("Klawiatura", 39.99, Jednostki.szt);
            Produkt p3 = new Produkt("Komputer", 1999.99, Jednostki.szt);

            temp.DodajProdukt(p1);
            temp.DodajProdukt(p2);
            Assert.AreEqual(true, temp.JestProduktem(p1));
            Assert.AreEqual(false, temp.JestProduktem(p3));
            Assert.AreEqual(true, temp.JestProduktem(p2.Kod));
            Assert.AreEqual(false, temp.JestProduktem(p3.Kod));
        }
        [TestMethod]
        public void JestKonkurentem()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Konkurent k1 = new Konkurent("BudEx", Branże.Inne);
            Konkurent k2 = new Konkurent("AdamEx",Branże.Motoryzacja);
            Konkurent k3 = new Konkurent("AutoMoto",Branże.Motoryzacja);

            temp.DodajKonkurenta(k1);
            temp.DodajKonkurenta(k2);
            Assert.AreEqual(true, temp.JestKonkurentem(k1));
            Assert.AreEqual(false, temp.JestKonkurentem(k3));
            Assert.AreEqual(true, temp.JestKonkurentem("AdamEx"));
            Assert.AreEqual(false, temp.JestKonkurentem("AutoMoto"));
        }
        [TestMethod]
        public void JestKlientem()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            Klient k1 = new Klient("BudEx", Branże.Inne);
            Klient k2 = new Klient("AdamEx",Branże.Motoryzacja);
            Klient k3 = new Klient("AutoMoto", Branże.Motoryzacja);

            temp.DodajKlienta(k1);
            temp.DodajKlienta(k2);
            Assert.AreEqual(true, temp.JestKlientem(k1));
            Assert.AreEqual(false, temp.JestKlientem(k3));
            Assert.AreEqual(true, temp.JestKlientem("AdamEx"));
            Assert.AreEqual(false, temp.JestKlientem("AutoMoto"));
        }
        [TestMethod]
        public void ZapisOdczytXML()
        {
            OrgProwadzacaCRM o = new OrgProwadzacaCRM("Monety S.A.", Branże.Finanse, "908-786-23-45", "Polska","Lublin","Aleksandra 34/5","23-456","Brak danych", "01.01.1920");
            Klient kl = new Klient("Toyota S.A.", Branże.Motoryzacja, "123-456-78-91", "Japonia", "Toyota", "01.01.1920");
            Konkurent kon = new Konkurent("Konkurencyjni z o.o.", Branże.Media);
            Pracownik prac = new Pracownik("Adam", "Kowalski", Plcie.M, Stanowiska.konsultant);
            Produkt pr = new Produkt("Telefon", 456.89);
            o.DodajKlienta(kl);
            o.DodajKonkurenta(kon);
            o.DodajPracownika(prac);
            o.DodajProdukt(pr);
            o.ZapiszXML("o.xml");
            OrgProwadzacaCRM oXML = OrgProwadzacaCRM.OdczytajXML("o.xml");

            Assert.AreEqual(oXML.Nazwa, o.Nazwa);
            Assert.AreEqual(oXML.Branza, o.Branza);
            Assert.AreEqual(oXML.Nip, o.Nip);
            Assert.AreEqual(oXML.Kraj, o.Kraj);
            Assert.AreEqual(oXML.Miasto, o.Miasto);
            Assert.AreEqual(oXML.Adres, o.Adres);
            Assert.AreEqual(oXML.KodPocztowy, o.KodPocztowy);
            Assert.AreEqual(oXML.Notatki, o.Notatki);
            Assert.AreEqual(oXML.DataZalozenia, o.DataZalozenia);
            Assert.AreEqual(true, oXML.JestProduktem(o.ListaProduktow[0].Kod));
            Assert.AreEqual(true, oXML.JestPracownikiem(o.ListaPracownikow[0].Imie, o.ListaPracownikow[0].Nazwisko, o.ListaPracownikow[0].Stanowisko));
            Assert.AreEqual(true, oXML.JestKlientem(o.ListaKlientow[0].Nazwa));
            Assert.AreEqual(true, oXML.JestKonkurentem(o.ListaKonkurentow[0].Nazwa));
        }
        [TestMethod]
        public void Clone()
        {
            OrgProwadzacaCRM o = new OrgProwadzacaCRM("Monety S.A.", Branże.Finanse, "908-786-23-45", "Polska", "Lublin", "Aleksandra 34/5", "23-456", "Brak danych", "01.01.1920");
            OrgProwadzacaCRM klon = (OrgProwadzacaCRM) o.Clone();

            o.ListaPracownikow.ForEach(p =>Assert.IsTrue(klon.JestPracownikiem(p)));
            o.ListaProduktow.ForEach(p => Assert.IsTrue(klon.JestProduktem(p)));
            o.ListaKonkurentow.ForEach(k => Assert.IsTrue(klon.JestKonkurentem(k)));
            o.ListaKlientow.ForEach(k => Assert.IsTrue(klon.JestKlientem(k)));
        }
    }
}
