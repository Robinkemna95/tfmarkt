using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Produktklassen;

namespace tfmarkt.Kalkulation
{
    class Ergebnis
    {
        public int flaecheGesamt { get; set; }
        public int anzahlProdukt { get; set; }
        public decimal preis { get; set; }
        public Produkt produkt { get; set; }

        public Ergebnis(int flaecheGesamt = 0, int anzahlProdukt = 0, decimal preis = 0.00m, Produkt produkt = null)
        {
            this.flaecheGesamt = flaecheGesamt;
            this.anzahlProdukt = anzahlProdukt;
            this.preis = preis;
            this.produkt = produkt;
        }
    }
}
