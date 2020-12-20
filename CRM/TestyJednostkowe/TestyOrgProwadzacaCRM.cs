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
            Konkurent konk1 = new Konkurent("Biedronka", Organizacja.Branże.Inne);
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
            Klient kl1 = new Klient("Amazon", Organizacja.Branże.Inne);
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
            Konkurent k1 = new Konkurent("BudEx", Organizacja.Branże.Inne);
            Konkurent k2 = new Konkurent("AdamEx", Organizacja.Branże.Motoryzacja);
            Konkurent k3 = new Konkurent("AutoMoto", Organizacja.Branże.Motoryzacja);

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
            Klient k1 = new Klient("BudEx", Organizacja.Branże.Inne);
            Klient k2 = new Klient("AdamEx", Organizacja.Branże.Motoryzacja);
            Klient k3 = new Klient("AutoMoto", Organizacja.Branże.Motoryzacja);

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
            Konkurent k1 = new Konkurent("BudEx", Organizacja.Branże.Inne);
            Konkurent k2 = new Konkurent("AdamEx", Organizacja.Branże.Motoryzacja);
            Konkurent k3 = new Konkurent("AutoMoto", Organizacja.Branże.Motoryzacja);

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
            Klient k1 = new Klient("BudEx", Organizacja.Branże.Inne);
            Klient k2 = new Klient("AdamEx", Organizacja.Branże.Motoryzacja);
            Klient k3 = new Klient("AutoMoto", Organizacja.Branże.Motoryzacja);

            temp.DodajKlienta(k1);
            temp.DodajKlienta(k2);
            Assert.AreEqual(true, temp.JestKlientem(k1));
            Assert.AreEqual(false, temp.JestKlientem(k3));
            Assert.AreEqual(true, temp.JestKlientem("AdamEx"));
            Assert.AreEqual(false, temp.JestKlientem("AutoMoto"));
        }
    }
}
