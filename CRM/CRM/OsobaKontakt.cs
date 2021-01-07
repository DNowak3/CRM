using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CRM
{
    /// <summary>
    /// Klasa definiująca osobę, z którą się kontaktujemy. Dziedziczy po klasie Osoba.
    /// </summary>
    [Serializable]
    public class OsobaKontakt :Osoba,IEquatable<OsobaKontakt>, ICloneable
    {
        /// <summary>
        /// Telefon do osoby, z którą się kontaktujemy.
        /// </summary>
        string _telefon;
        /// <summary>
        /// Mail do osoby, z którą się kontaktujemy.
        /// </summary>
        string _mail;
        /// <summary>
        /// Notatki o osobie, z którą się kontaktujemy.
        /// </summary>
        string _notatki;

        #region Wlasciwosci
        public string Telefon { get => _telefon; set => _telefon = PoprawnyTelefon(value); }
        public string Mail { get => _mail; set => _mail = PoprawnyEmail(value); }
        public string Notatki { get => _notatki; set => _notatki = value; }
        #endregion
        #region Konstruktory
        public OsobaKontakt():base()
        {
            Telefon = Mail = Notatki = null;
        }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec) : base(imie, nazwisko,plec)
        { }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko) : base(imie, nazwisko, plec, stanowisko)
        { }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string telefon, string mail) : this(imie, nazwisko, plec, stanowisko)
        {
            Telefon = telefon;

            Mail = mail;
        }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko,string telefon, string mail, string notatki):this(imie, nazwisko, plec, stanowisko,telefon, mail)
        {
            Notatki = notatki;
        }
        #endregion
        #region Funkcje
        /// <summary>
        /// Funkacja sprawdzająca, czy podany numer telefonu jest poprawny.
        /// </summary>
        /// <param name="telefon">Telefon osoby, z którą się kontaktujemy.</param>
        /// <returns>Null, jeżeli numer jest niepoprawny
        /// telefon (czyli wpisany napis), jeżeli telefon został podany poprawnie.</returns>
        string PoprawnyTelefon(string telefon)
        {
            string maska = @"^\d{3}[ -]?\d{3}[ -]?\d{3}$";
            if (PoprawnoscRegex(telefon, maska))
            {
                return telefon;
            }
            return null;
        }
        /// <summary>
        /// Funkacja sprawdzająca, czy podany email jest poprawny.
        /// </summary>
        /// <param name="email">Email osoby, z którą się kontaktujemy.</param>
        /// <returns>Null, jeżeli email jest niepoprawny,
        /// email (czyli wpisany napis), jeżeli email został podany poprawnie.</returns>
        string PoprawnyEmail(string email)
        {
            //\w - oznacza to samo co to: [a-zA-Z0-9_]
            //+ - dopasowuje tyle razy ile trzeba
            //[\w.%-_+]+ - dopasowuje ktorys z tych znakow tyle razy ile trzeba
            //@ - dopasowuje malpke w srodku maila
            //\. - dopasowuje kropke
            //https://stackoverflow.com/questions/5342375/regex-email-validation

            string maska = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (PoprawnoscRegex(email, maska))
                {
                    return email;
                }
            return null;
        }
        /// <summary>
        /// Funkcja sprawdzająca, czy podany napis jest zgodny z podaną do niego maską.
        /// </summary>
        /// <param name="napis">Napis, który chcemy zwalidować.</param>
        /// <param name="maska">Sposób, w jaki napis powinien być zapisany.</param>
        /// <returns>Prawdę, jeśli jest zgodny,
        /// Nieprawdę, jeśli nie jest zgodny.</returns>
        bool PoprawnoscRegex(string napis, string maska)
        {
            if (napis == null)
            {
                return false;
            }
            Regex wyrReg = new Regex(maska);
            return wyrReg.IsMatch(napis);
        }
        /// <summary>
        /// Funkcja sprawdza, czy dwie osoby kontaktowe są tą samą osobą kontaktową.
        /// Porównuje ich Imiei Nazwisko oraz Telefon i Mail, jeśli te nie są nullami.
        /// </summary>
        /// <param name="other">Osoba, z którą porównujemy.</param>
        /// <returns>Prawda, jeśli są,
        /// Fałsz, jeśli nie są.</returns>
        public bool Equals(OsobaKontakt other)
        {
            if (other == null)
            {
                return false;
            }
            if (Telefon != null && Mail != null)
            {
                return (Imie.Equals(other.Imie) && Nazwisko.Equals(other.Nazwisko) && Telefon.Equals(other.Telefon) && Mail.Equals(other.Mail));
            }
            return (Imie.Equals(other.Imie) && Nazwisko.Equals(other.Nazwisko));
        }
        /// <summary>
        /// Funkcja tworzy kopię obiektu osoby, z którą się kontaktujemy.
        /// </summary>
        /// <returns>Kopia obiektu OsobaKontakt.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
        /// <summary>
        /// Funkcja tworzy i zwraca napis z danymi osoby do kontaktu.
        /// </summary>
        /// <returns>Napis zawierający dane osoby do kontaktu.</returns>
        public override string ToString()
        {
            string napis = Telefon == null ? "" : $", nr telefonu: {Telefon}";
            napis += Mail == null ? null : $", email: {Mail}";
            napis+= $"\n\tNotatki: {Notatki}";
            return base.ToString() + napis;
        }
    }
}
