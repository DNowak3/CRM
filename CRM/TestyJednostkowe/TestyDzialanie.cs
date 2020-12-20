using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM;
using System;

namespace TestyJednostkowe
{
    [TestClass]
    public class TestyDzialanie
    {
        [TestMethod]
        public void Porownywanie()
        {
            Dzialanie d1 = new Dzialanie("Spotkanie", "01.10.2020");
            Dzialanie d2 = new Dzialanie("Rozmowa telefoniczna", "01.11.2020");
            Dzialanie d3 = new Dzialanie("Rozmowa telefoniczna", "01.09.2020");

            int wynik = d1.CompareTo(d2);
            Assert.IsTrue(wynik < 0);

            wynik = d1.CompareTo(d1);
            Assert.IsTrue(wynik == 0);

            wynik = d1.CompareTo(d3);
            Assert.IsTrue(wynik > 0);
        }

        [TestMethod]
        public void Klonowanie()
        {
            Dzialanie original = new Dzialanie("E-mail w sprawie umowy", "10-12-2020");
            Dzialanie clone = (Dzialanie)original.Clone();
            Assert.AreEqual(clone.Nazwa, original.Nazwa);
            Assert.AreEqual(clone.Data, original.Data);
            Assert.AreEqual(clone.Opis, original.Opis);
        }
    }
}
