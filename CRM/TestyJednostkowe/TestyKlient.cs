using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CRM;
using System.Collections.Generic;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyKlient
    {
        [TestMethod]
        public void DodawanieDzialania()
        {
            Klient Toyota = new Klient("Toyota S.A.",Branże.Motoryzacja);
            Dzialanie d = new Dzialanie("Podpisanie umowy", "12.10.2020");
            Toyota.DodajDzialanie(d);
            Assert.AreEqual(Toyota.IleDzialan(), 1);
        }

        [TestMethod]
        public void UsuwanieDzialania()
        {
            Klient Toyota = new Klient("Toyota S.A.",Branże.Motoryzacja);
            Dzialanie d = new Dzialanie("Podpisanie umowy", "12.10.2020");
            Toyota.DodajDzialanie(d);
            Toyota.UsunDzialanie(d.Nazwa);
            Assert.AreEqual(Toyota.IleDzialan(), 0);
        }

        [TestMethod]
        public void ZnajdzDzialaniePoNazwie()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Dzialanie d1 = new Dzialanie("Podpisanie umowy", "12.10.2020");
            Dzialanie d2 = new Dzialanie("Podpisanie umowy", "14.11.2020");
            Toyota.DodajDzialanie(d1);
            Toyota.DodajDzialanie(d2);
            Assert.AreEqual(Toyota.ZnajdzDzialanie("podpisanie umowy").Nazwa, d2.Nazwa);
            Assert.AreEqual(Toyota.ZnajdzDzialanie("podpisanie umowy").Data, d2.Data);
        }

        [TestMethod]
        public void ZnajdzOstatniKontakt()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Dzialanie d1 = new Dzialanie("Podpisanie umowy", "12.10.2020");
            Dzialanie d2 = new Dzialanie("Podpisanie umowy", "14.11.2020");
            Dzialanie d3 = new Dzialanie("Podpisanie umowy", "01.09.2020");
            Toyota.DodajDzialanie(d1);
            Toyota.DodajDzialanie(d2);
            Toyota.DodajDzialanie(d3);
            Assert.AreEqual(Toyota.OstatniKontakt(), d2.Data);
        }

        [TestMethod]
        public void ZnajdzDzialaniePoDacie()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Dzialanie d1 = new Dzialanie("Podpisanie umowy", "12.10.2020");
            Dzialanie d2 = new Dzialanie("Podpisanie umowy", "14.11.2020");
            Dzialanie d3 = new Dzialanie("Podpisanie umowy", "01.09.2020");
            Toyota.DodajDzialanie(d1);
            Toyota.DodajDzialanie(d2);
            Toyota.DodajDzialanie(d3);

            List<Dzialanie> dzialania = Toyota.ZnajdzDzialania("12.10.2020");
            Assert.IsTrue(dzialania.Contains(d1));
            Assert.IsTrue(dzialania.Contains(d2));
            Assert.IsFalse(dzialania.Contains(d3));
        }

        [TestMethod]
        public void AktualizacjaStatusu()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Toyota.AktualizujStatus();
            Assert.IsTrue(Toyota.Status == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NonOrganizationMemberException))]
        public void ZwrocKontakt()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Toyota.ZwrocKontakt("Brad", "Pitt");
            Assert.Fail();
        }


        [TestMethod]
        public void DodawanieUsuwanieKontaktu()
        {
            Klient Toyota = new Klient("Toyota S.A.",Branże.Motoryzacja);
            OsobaKontakt o = new OsobaKontakt("Brad", "Pitt", Plcie.M);
            Toyota.DodajKontakt(o);

            Assert.IsTrue(Toyota.PosiadaKontakt("Brad", "Pitt"));
            Assert.AreEqual(Toyota.ZwrocKontakt("Brad", "Pitt"), o);

            Toyota.UsunKontakt(o);
            Assert.IsFalse(Toyota.PosiadaKontakt("Brad", "Pitt"));
        }

        [TestMethod]
        public void DodawanieUsuwanieTransakcji()
        {
            Klient Toyota = new Klient("Toyota S.A.", Branże.Motoryzacja);
            Pracownik p = new Pracownik();
            Umowa u1 = new Umowa(p, "02-06-2020");
            Umowa u2 = new Umowa(p, "12-08-2020");
            Toyota.DodajTransakcje(u1);
            Toyota.DodajTransakcje(u2);
            Toyota.UsunTransakcje(u2.NrUmowy);
            Assert.IsTrue(Toyota.ZwrocTransakcje(u2.NrUmowy).Count == 0);
        }

        [TestMethod]
        public void ZapisOdczytXML()
        {
            Klient Toyota = new Klient("Toyota S.A.",Branże.Motoryzacja, "123-456-78-91", "Japonia", "Toyota", "01.01.1920");
            Toyota.ZapiszXML("Toyota.xml");
            Assert.AreEqual(Klient.OdczytajXML("Toyota.xml").Nazwa, Toyota.Nazwa);
            Assert.AreEqual(Klient.OdczytajXML("Toyota.xml").Nip, Toyota.Nip);
            Assert.AreEqual(Klient.OdczytajXML("Toyota.xml").Kraj, Toyota.Kraj);
            Assert.AreEqual(Klient.OdczytajXML("Toyota.xml").Miasto, Toyota.Miasto);
        }

    }
}
