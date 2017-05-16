using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Produktklassen;

namespace tfmarkt.Kalkulation
{
    public class Boden
    {
        public int laenge { get; set; }
        public int breite { get; set; }
        public Fliesenpaket fliesen { get; set; }

        public Boden(int breite, int laenge)
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
