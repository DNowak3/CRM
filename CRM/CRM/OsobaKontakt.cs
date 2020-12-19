﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM
{
    public class OsobaKontakt:Osoba,IEquatable<OsobaKontakt>, ICloneable
    {
        string _telefon;
        string _mail;
        string _notatki;

        public string Telefon { get => _telefon; set => _telefon = PoprawnyTelefon(value); }
        public string Mail { get => _mail; set => _mail = PoprawnyEmail(value); }
        public string Notatki { get => _notatki; set => _notatki = value; }

        #region Konstruktory
        public OsobaKontakt():base()
        {
            Telefon = Mail = Notatki = null;
        }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec) : base(imie, nazwisko,plec)
        { }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko) : base(imie, nazwisko, plec, stanowisko)
        { }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string telefon) : this(imie, nazwisko, plec, stanowisko)
        {
            Telefon = telefon;
        }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko, string telefon, string mail) : this(imie, nazwisko, plec, stanowisko, telefon)
        {
            Mail = mail;
        }
        public OsobaKontakt(string imie, string nazwisko, Plcie plec, Stanowiska stanowisko,string telefon, string mail, string notatki):this(imie, nazwisko, plec, stanowisko,telefon, mail)
        {
            Notatki = notatki;
        }
        #endregion
        #region Funkcje
        string PoprawnyTelefon(string telefon)
        {
            string maska = @"^\d{3}[ -]?\d{3}[ -]?\d{3}$";
            if (PoprawnoscRegex(telefon, maska))
            {
                return telefon;
            }
            return null;
        }
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
        bool PoprawnoscRegex(string telefon, string maska)
        {
            Regex wyrReg = new Regex(maska);
            return wyrReg.IsMatch(telefon);
        }
        public bool Equals(OsobaKontakt other)
        {
            if (other == null)
            {
                return false;
            }
            return (Imie.Equals(other.Imie) && Nazwisko.Equals(other.Nazwisko) && Telefon.Equals(other.Telefon) && Mail.Equals(other.Mail));

        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
        public override string ToString()
        {
            string napis = Telefon == null ? "" : $", nr telefonu: {Telefon}";
            napis += Mail == null ? null : $", email: {Mail}";
            napis+= Notatki==null?null:$"\n\tNotatki: {Notatki}";
            return base.ToString() + napis;
        }


    }
}
