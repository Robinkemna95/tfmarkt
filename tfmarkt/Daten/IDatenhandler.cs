using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Katalog;

namespace tfmarkt.Daten
{
    // Interface das die Kommunikation zwischen Produktkatalog und Ladeprozess verwaltet
    // Dabei ist es egal über welches Medium, Datenbank, Datei etc. die Daten zur 
    // Verfügung gestellt werden, das Interface stellt dazu eine gemeinsam nutzbare Schnittstelle
    //
    // Jede Klasse die Daten für den Produktkatalog bereitstellen möchte, muss dieses Interface implementieren 
    interface IDatenhandler
    {
        // Sichert den Produktkatalog
        Boolean sichereProduktkatalog(Produktkatalog katalog);

        // lädt den Produktkatalog
        Boolean fuelleProduktkatalog(Produktkatalog katalog);
    }
}
