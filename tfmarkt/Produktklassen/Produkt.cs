using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    // Abstrakte Klasse Produkt, definiert in allen Produkten zur Verfügung gestellte Eigenschaften
    // Produkt implementiert das Interface ICloneable um von sich selbst Kopien erstellen zu können
    public abstract class Produkt : ICloneable
    {
        // Klassen Member der abstrakten Klasse Produkt
        public int uid { get; set; }
        public string artikelnummer { get; set; }
        public decimal preis { get; set; }
        public string titel { get;  set; }
        public string beschreibung { get; set; }

        // Rückgabe des Klassennamen bzw. des Produktnamen
        abstract public string produktName();

        // Implementiertes Interface ICloneable zur Anlegen von Produktkopien bzw.
        // Kopien von vererbenden Klassen
        abstract public object Clone(); 
    }
}
