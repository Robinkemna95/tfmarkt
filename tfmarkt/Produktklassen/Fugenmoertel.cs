using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    class Fugenmoertel : Zusatzprodukt
    {
        // Klassen Member der Klasse Fuegenmoertel
        public int minFlaeche { get; set; }
        public int maxFlaeche { get; set; }

         // Konstruktor der Klasse Tapetenkleister
        public Fugenmoertel(string artikelnummer, string titel, string beschreibung, decimal preis, double gewicht, int minFlaeche, int maxFlaeche, Boolean istAusgewaehlt)
        {
            // Member der abstrakten Klasse Produkt
            base.artikelnummer = artikelnummer;
            base.titel = titel;
            base.beschreibung = beschreibung;
            base.preis = preis;
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
            Fugenmoertel kopie = new Fugenmoertel(artikelnummer, titel, beschreibung, preis, gewicht, minFlaeche, maxFlaeche, istAusgewaehlt);

            return kopie;
        }
    }
}
