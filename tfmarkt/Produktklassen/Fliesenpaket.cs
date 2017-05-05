using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Klasse Fliesenpaket definiert was ein Fliesenpaket ist und was sie alles kann
    // Fliesenpaket erbt Eigenschaften von der abstrakten Klasse Produkt
    public class Fliesenpaket : Produkt
    {
        // Klassen Member der Klasse Fliesenpaket
        public int laenge { get; set; }
        public int breite { get; set; }
        public int anzahl { get; set; }

        public Fliesenpaket()
        { 
        }

        //Konstruktor der Klasse Fliesenpaket
        public Fliesenpaket(string artikelnummer, string titel, string beschreibung, decimal preis, int laenge, int breite, int anzahl)
        {
            //Member der abstrakten Klasse Produkt
            base.artikelnummer = artikelnummer;
            base.titel = titel;
            base.beschreibung = beschreibung;
            base.preis = preis;

            //Member der Klasse Fliesenpaket
            this.laenge = laenge;
            this.breite = breite;
            this.anzahl = anzahl;
        }

        public double getFlaeche()
        {
            return this.laenge * this.breite;
        }

        override public string produktName()
        {
            return this.GetType().Name;
        }

        override public object Clone()
        {
            Fliesenpaket kopie = new Fliesenpaket(artikelnummer, titel, beschreibung, preis, laenge, breite, anzahl);

            return kopie;
        }
    }
}
