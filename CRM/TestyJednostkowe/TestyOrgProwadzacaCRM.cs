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
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (AlreadyInListException) { }
        }
        public void DodawaniePracownikow1()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            temp.DodajPracownika(new Pracownik("Mateusz", "Nowak", Plcie.M, Stanowiska.sprzedawca));
            Assert.AreEqual(licznik, temp.PodajIloscPracownikow());
        }
        [TestMethod]
        public void DodawanieProduktow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            temp.DodajProdukt(new Produkt());
            Assert.AreEqual(licznik, temp.PodajIloscProduktow());
        }
        [TestMethod]
        public void DodawanieKonkurentow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            temp.DodajKonkurenta(new Konkurent());
            Assert.AreEqual(licznik, temp.PodajIloscKonkurentow());
        }
        [TestMethod]
        public void DodawanieKlientow()
        {
            OrgProwadzacaCRM temp = new OrgProwadzacaCRM();
            int licznik = 1;
            temp.DodajKlienta(new Klient());
            Assert.AreEqual(licznik, temp.PodajIloscKlientow());
        }
    }
}
