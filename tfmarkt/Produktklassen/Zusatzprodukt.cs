using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    abstract class Zusatzprodukt : Produkt
    {
        // Klassen Member der abstrakten Klasse Zusatzprodukt
        public Boolean istAusgewaehlt { get; protected set; }
        public double gewicht { get; protected set; }
    }
}
