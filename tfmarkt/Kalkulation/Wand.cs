using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Produktklassen;

namespace tfmarkt.Kalkulation
{
    public class Wand
    {
        public int laenge { get; set; }
        public int breite { get; set; }
        public Tapetenrolle tapete { get; set; }

        public Wand(int breite, int laenge)
        {
            this.breite = breite;
            this.laenge = laenge;
        }

        public int getFlaeche()
        {
            return this.laenge * this.breite;
        }

    }
}
