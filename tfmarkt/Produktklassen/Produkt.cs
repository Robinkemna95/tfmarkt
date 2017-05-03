using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Produktklassen
{
    abstract class Produkt : ICloneable
    {
        // Klassen Member der abstrakten Klasse Produkt
        public int uid { get; protected set; }
        public string artikelnummer { get; protected set; }
        public decimal preis { get; protected set; }
        public string titel { get; protected set; }
        public string beschreibung { get; protected set; }

        // Rückgabe des Klassennamen bzw. des Produktnamen
        abstract public string produktName();

        // Implementiertes Interface ICloneable zur Anlegen von Produktkopien bzw.
        // Kopien von vererbenden Klassen
        abstract public object Clone(); 
    }
}
