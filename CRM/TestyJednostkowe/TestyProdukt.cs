using CRM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyProdukt
    {
        [TestMethod]
        public void Porownywanie()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Produkt p2 = new Produkt("Zszywka", 0.90, Jednostki.szt);
            Assert.AreEqual(1, p2.CompareTo(p1));
        }
        [TestMethod]
        public void ZapisOdczytXML()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            p1.ZapiszXML("Produkt1");
            Assert.AreEqual(Produkt.OdczytajXML("Produkt1").Nazwa, p1.Nazwa);
            Assert.AreEqual(Produkt.OdczytajXML("Produkt1").Kod, p1.Kod);
            Assert.AreEqual(Produkt.OdczytajXML("Produkt1").Cena, p1.Cena);
            Assert.AreEqual(Produkt.OdczytajXML("Produkt1").Jednostka, p1.Jednostka);
        }
        [TestMethod]
        public void ZapisOdczytJSON()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            p1.ZapiszJSON("Produkt1");
            Assert.AreEqual(Produkt.OdczytajJSON("Produkt1").Nazwa, p1.Nazwa);
            Assert.AreEqual(Produkt.OdczytajJSON("Produkt1").Kod, p1.Kod);
            Assert.AreEqual(Produkt.OdczytajJSON("Produkt1").Cena, p1.Cena);
            Assert.AreEqual(Produkt.OdczytajJSON("Produkt1").Jednostka, p1.Jednostka);
        }
        [TestMethod]
        public void Clone()
        {
            Produkt p1 = new Produkt("Zszywacz", 15.8, Jednostki.szt);
            Produkt p2 = (Produkt)p1.Clone();
            Assert.AreEqual(p1.Nazwa, p2.Nazwa);
            Assert.AreEqual(p1.Kod, p2.Kod);
            Assert.AreEqual(p1.Cena, p2.Cena);
            Assert.AreEqual(p1.Jednostka, p2.Jednostka);
        }
    }
}
