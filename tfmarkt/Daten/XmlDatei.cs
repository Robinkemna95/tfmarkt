using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using tfmarkt.Katalog;
using tfmarkt.Produktklassen;

namespace tfmarkt.Daten
{
    class XmlDatei : IDatenhandler
    {
        // Klassen Member der Klasse XmlDatei
        public string dateiTapeten { get; set; }
        public string dateiFliesen { get; set; }
        public string dateiZusatzprodukte { get; set; }

        public string xmlVerzeichnis;

        private XmlSerializer xmlSerializer;

        // Konstruktor der Klasse XmlDatei
        public XmlDatei()
        {
            // Member der Klasse XmlDatei
            this.xmlVerzeichnis = System.Environment.CurrentDirectory;

            this.dateiTapeten = "Tapeten.xml";
            this.dateiFliesen = "Fliesen.xml";
            this.dateiZusatzprodukte = "Zusatzprodukte.xml";
        }

        public bool sichereProduktkatalog(Produktkatalog pdKatalog)
        {
            Boolean erfolgreich = false;

            Type tyTapeten = pdKatalog.tapeten.GetType();
            Type tyFliesen = pdKatalog.fliesen.GetType();
            Type tyZusatzprodukte = pdKatalog.zusatzprodukte.GetType();

            this.dateiSchreiben(this.dateiTapeten, tyTapeten, pdKatalog.tapeten);
            this.dateiSchreiben(this.dateiFliesen, tyFliesen, pdKatalog.fliesen);
            this.dateiSchreiben(this.dateiZusatzprodukte, tyZusatzprodukte, pdKatalog.zusatzprodukte);
            

            // Noch nicht implementiert

            return erfolgreich;
        }

        public bool fuelleProduktkatalog(Katalog.Produktkatalog katalog)
        {
            Boolean erfolgreich = false;

            // Noch nicht implementiert

            return erfolgreich;
        }

        // Methode zum Schreiben der einzelnen Listen in die entsprechende XML-Datei
        private void dateiSchreiben<T>(string dateiname, Type t, List<T> liste)
        {
            this.xmlSerializer = new XmlSerializer(t);
            TextWriter WriteFileStream = new StreamWriter(this.xmlVerzeichnis + "/" + dateiname);
            this.xmlSerializer.Serialize(WriteFileStream, liste);

            // Cleanup
            WriteFileStream.Close(); 
        }
    }
}
