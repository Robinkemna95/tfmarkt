using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Klasse Tapetenkleister definiert was Tapetenkleister ist und was sie alles kann
    // Tapetenkleister erbt Eigenschaften von der Klasse Zusatzprodukt
    public class Tapetenkleister : Zusatzprodukt
    {
        // Klassen Member der Klasse Tapetenkleister
        public int flaeche { get; set; }

        public Tapetenkleister()
        { 
        }

        // Konstruktor der Klasse Tapetenkleister
        public Tapetenkleister(string artikelnummer, string titel, string beschreibung, decimal preis, int gewicht, int flaeche, Boolean istAusgewaehlt)
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
            this.flaeche = flaeche;
        }

        override public string produktName()
        {
            return this.GetType().Name;
        }

        override public object Clone()
        {
            Tapetenkleister kopie = new Tapetenkleister(artikelnummer, titel, beschreibung, preis, gewicht, flaeche, istAusgewaehlt);

            return kopie;
        }
    }
}
