using CRM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyUmowa
    {
        [TestMethod]
        public void Koszt()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Produkt p2 = new Produkt("Zszywka", 0.90, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            int ilosc2 = 10;
            u1.DodajProdukt(p2, ilosc2);
            double razem = p1.Cena + p2.Cena * ilosc2;
            Assert.AreEqual(razem, u1.Koszt());
        }
        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void ZnajdzProdukt()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            Assert.IsNotNull(u1.ZnajdzProdukt("200"));
        }
        [TestMethod]
        public void JesliSztuki()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            double ilosc1 = 7.90;
            Assert.AreEqual(Math.Floor(ilosc1), u1.JesliSztuki(p1, ilosc1));
        }
        [TestMethod]
        public void UsunProdukt()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            Assert.IsTrue(u1.KupioneProdukty.ContainsKey(p1));
            u1.UsunProdukt(p1);
            Assert.IsFalse(u1.KupioneProdukty.ContainsKey(p1));
        }
        [TestMethod]
        public void UsunProduktKod()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            u1.UsunProduktKod("Z20201");
            Assert.IsFalse(u1.KupioneProdukty.ContainsKey(p1));
        }
        [TestMethod]
        public void ZmienIlosc()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            double nowaIlosc = 3;
            Assert.IsTrue(u1.ZmienIlosc(p1, nowaIlosc));
            u1.KupioneProdukty.TryGetValue(p1, out double ilosc);
            Assert.AreEqual(nowaIlosc, ilosc);
        }
        [TestMethod]
        public void ZmienIloscKod()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Umowa u1 = new Umowa();
            u1.DodajProdukt(p1);
            double nowaIlosc = 3;
            Assert.IsTrue(u1.ZmienIloscKod("Z20201", nowaIlosc));
            u1.KupioneProdukty.TryGetValue(p1, out double ilosc);
            Assert.AreEqual(nowaIlosc, ilosc);
        }
        [TestMethod]
        public void ZapisOdczytBIN()
        {
            Pracownik pr1 = new Pracownik("Adam", "Kowalski", Plcie.M, Stanowiska.sprzedawca);
            Umowa u1 = new Umowa(pr1, "2019-10-15");
            u1.ZapiszBin("Umowa1");
            Assert.AreEqual(Umowa.OdczytajBin("Umowa1").NrUmowy, u1.NrUmowy);
            Assert.AreEqual(Umowa.OdczytajBin("Umowa1").PracownikOdp.Imie, u1.PracownikOdp.Imie);
            Assert.AreEqual(Umowa.OdczytajBin("Umowa1").PracownikOdp.Nazwisko, u1.PracownikOdp.Nazwisko);
            Assert.AreEqual(Umowa.OdczytajBin("Umowa1").DataUmowy, u1.DataUmowy);
        }
        [TestMethod]
        public void Clone()
        {
            Pracownik pr1 = new Pracownik("Adam", "Kowalski", Plcie.M, Stanowiska.sprzedawca);
            Umowa u1 = new Umowa(pr1, "2019-10-15");
            Umowa u2 = (Umowa)u1.Clone();
            Assert.AreEqual(u2.PracownikOdp.Imie, u1.PracownikOdp.Imie);
            Assert.AreEqual(u2.PracownikOdp.Nazwisko, u1.PracownikOdp.Nazwisko);
            Assert.AreEqual(u2.DataUmowy, u1.DataUmowy);
        }


    }
}
