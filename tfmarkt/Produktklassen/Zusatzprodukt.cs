using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Abstrakte Klasse der Zusatzprodukte, definiert in allen Zusatzprodukten zur Verfügung gestellte Eigenschaften
    // Zusatzprodukt erbt Eigenschaften von der abstrakten Klasse Produkt
    public abstract class Zusatzprodukt : Produkt
    {
        // Klassen Member der abstrakten Klasse Zusatzprodukt
        public Boolean istAusgewaehlt { get; set; }
        public int gewicht { get; set; }
    }
}
