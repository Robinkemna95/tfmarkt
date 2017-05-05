using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Klasse Tapetenrolle definiert was eine Tapetenrolle ist und was sie alles kann
    // Tapetenrolle erbt Eigenschaften von der abstrakten Klasse Produkt
    public class Tapetenrolle : Produkt
    {
        // Klassen Member der Klasse Tapetenrolle
        public int laenge { get; set; }
        public int breite { get; set; }
        public int rapport { get; set; }

        public Tapetenrolle()
        {
        }

        // Konstruktor der Klasse Tapetenrolle
        public Tapetenrolle(string artikelnummer, string titel, string beschreibung, decimal preis, int laenge, int breite, int rapport)
        {
            // Member der abstrakten Klasse Produkt
            base.artikelnummer = artikelnummer; 
            base.titel = titel;
            base.beschreibung = beschreibung;
            base.preis = preis;

            // Member der Klasse Tapetenrolle
            this.laenge = laenge;
            this.breite = breite;
            this.rapport = rapport;
        }

        // Rückgabe der Fläche die mit dieser Tapete beklebt werden kan
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
            Tapetenrolle kopie = new Tapetenrolle(artikelnummer, titel, beschreibung, preis, laenge, breite, rapport);

            return kopie;
        }
    }
}
