using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Klasse Fliesenkleber definiert was Fliesenkleber ist und was sie alles kann
    // Fliesenkleber erbt Eigenschaften von der Klasse Zusatzprodukt
    public class Fliesenkleber : Zusatzprodukt
    {
        // Klassen Member der Klasse Fliesenkleber
        public int minFlaeche { get; set; }
        public int maxFlaeche { get; set; }

        public Fliesenkleber()
        { 
        }

        // Konstruktor der Klasse Fliesenkleber
        public Fliesenkleber(string artikelnummer, string titel, string beschreibung, decimal preis, int gewicht, int minFlaeche, int maxFlaeche, Boolean istAusgewaehlt)
        {
            // Member der abstrakten Klasse Produkt
            base.artikelnummer = artikelnummer.StartsWith("Z") ? artikelnummer : "Z" + artikelnummer;
            base.titel = titel;
            base.beschreibung = beschreibung;
            base.preis = preis;

            // Member der abstrakten Klasse Zusatzprodukt
            base.gewicht = gewicht;
            base.istAusgewaehlt = istAusgewaehlt;

            // Member der Klasse Tapetenkleister
            this.minFlaeche = minFlaeche;
            this.maxFlaeche = maxFlaeche;
        }

        // Gibt den Mittelwert aus minFlaeche und maxFlaeche zurück
        public int getFlaeche()
        {
            return (this.minFlaeche + this.maxFlaeche) / 2;
        }

        override public string produktName()
        {
            return this.GetType().Name;
        }

        override public object Clone()
        {
            Fliesenkleber kopie = new Fliesenkleber(artikelnummer, titel, beschreibung, preis, gewicht, minFlaeche, maxFlaeche, istAusgewaehlt);

            return kopie;
        }
    }
}
