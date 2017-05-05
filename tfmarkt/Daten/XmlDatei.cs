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
        // Statische Klassen Member der Klasse XmlDatei
        public const string ENDUNG = ".xml";

        // Klassen Member der Klasse XmlDatei
        public string dateiTapeten { get; set; }
        public string dateiFliesen { get; set; }
        public string dateiZusatzprodukte { get; set; }

        // Das zur Laufzeit ermittelte Arbeitsverzeichnis 
        // ist der Speicherort für die XML Dateien
        public string xmlVerzeichnis;

        private XmlSerializer xmlSerializer;

        // Konstruktor der Klasse XmlDatei
        public XmlDatei()
        {
            // Member der Klasse XmlDatei
            this.xmlVerzeichnis = System.Environment.CurrentDirectory;

            this.dateiTapeten = "Tapeten" + ENDUNG;
            this.dateiFliesen = "Fliesen" + ENDUNG;
            this.dateiZusatzprodukte = "Zusatzprodukt";
        }

        // Sicherung des Produktkataloges in XML Dateien
        // Es werden separate XML Dateien für Fliesen, Tapeten, 
        // Fliesenkleber, Fugenmoertel und Tapetenkleister angelegt 
        public bool sichereProduktkatalog(Produktkatalog pdKatalog)
        {
            Boolean erfolgreich = false;

            Type tyTapeten = pdKatalog.tapeten.GetType();
            Type tyFliesen = pdKatalog.fliesen.GetType();
            Type tyZusatzprodukte;

            this.dateiSchreiben(this.dateiTapeten, tyTapeten, pdKatalog.tapeten);
            this.dateiSchreiben(this.dateiFliesen, tyFliesen, pdKatalog.fliesen);

            foreach (Zusatzprodukt z in pdKatalog.zusatzprodukte)
            {
                String tmpDateiname;
                tyZusatzprodukte = z.GetType();

                tmpDateiname = this.dateiZusatzprodukte + "_" + tyZusatzprodukte.Name + ENDUNG;

                this.dateiSchreiben(tmpDateiname, tyZusatzprodukte, z);
            }

            erfolgreich = true;

            return erfolgreich;
        }

        // Laden des Produktkataloges aus XML Dateien
        // Daten werden aus separaten XML Dateien für Fliesen, Tapeten, 
        // Fliesenkleber, Fugenmoertel und Tapetenkleister herausgelesen
        public bool fuelleProduktkatalog(Katalog.Produktkatalog pdKatalog)
        {
            Boolean erfolgreich = false;
            String tmpDateiname;
            List<Zusatzprodukt> tmpZusatzprodukte = new List<Zusatzprodukt>();

            Type tyTapeten = pdKatalog.tapeten.GetType();
            Type tyFliesen = pdKatalog.fliesen.GetType();
            Type tyZusatzprodukte;

            // Einlesen der Tapeten XML Datei
            if (File.Exists(this.xmlVerzeichnis + "/" + dateiTapeten))
            {
                List<Tapetenrolle> tmp;
                tmp = this.dateiLesen<List<Tapetenrolle>>(this.dateiTapeten, tyTapeten);

                if (tmp.Count > 0)
                {
                    pdKatalog.tapeten.Clear();
                    foreach(Tapetenrolle t in tmp)
                    {
                        pdKatalog.tapeten.Add(t);
                    }
                }
            }

            // Einlesen der Fliesen XML Datei
            if (File.Exists(this.xmlVerzeichnis + "/" + dateiFliesen))
            {
                List<Fliesenpaket> tmp;
                tmp = this.dateiLesen<List<Fliesenpaket>>(this.dateiFliesen, tyFliesen);

                if (tmp.Count > 0)
                {
                    pdKatalog.fliesen.Clear();
                    foreach (Fliesenpaket t in tmp)
                    {
                        pdKatalog.fliesen.Add(t);
                    }
                }
            }

            
            // Dateinamen festlegen und einlesen Tapetenkleister XML Datei
            tmpDateiname = this.dateiZusatzprodukte + "_" + new Tapetenkleister().produktName() + ENDUNG;
            if (File.Exists(this.xmlVerzeichnis + "/" + tmpDateiname))
            {
                Zusatzprodukt tmp = null;
                tmp = this.dateiLesen<Zusatzprodukt>(tmpDateiname, new Tapetenkleister().GetType());

                if (tmp != null)
                {
                    tmpZusatzprodukte.Add(tmp);
                }
            }

            // Dateinamen festlegen und einlesen Fliesenkleber XML Datei
            tmpDateiname = this.dateiZusatzprodukte + "_" + new Fliesenkleber().produktName() + ENDUNG;
            if (File.Exists(this.xmlVerzeichnis + "/" + tmpDateiname))
            {
                Zusatzprodukt tmp = null;
                tmp = this.dateiLesen<Zusatzprodukt>(tmpDateiname, new Fliesenkleber().GetType());

                if (tmp != null)
                {
                    tmpZusatzprodukte.Add(tmp);
                }
            }

            // Dateinamen festlegen und einlesen Fugenmoertel XML Datei
            tmpDateiname = this.dateiZusatzprodukte + "_" + new Fugenmoertel().produktName() + ENDUNG;
            if (File.Exists(this.xmlVerzeichnis + "/" + tmpDateiname))
            {
                Zusatzprodukt tmp = null;
                tmp = this.dateiLesen<Zusatzprodukt>(tmpDateiname, new Fugenmoertel().GetType());

                if (tmp != null)
                {
                    tmpZusatzprodukte.Add(tmp);
                }
            }

            if (tmpZusatzprodukte.Count > 0)
            {
                pdKatalog.zusatzprodukte.Clear();
                foreach (Zusatzprodukt t in tmpZusatzprodukte)
                {
                    pdKatalog.zusatzprodukte.Add(t);
                }
                
            }

            erfolgreich = true;

            return erfolgreich;
        }

        // Methode zum Schreiben der Listen für Tapeten und Fliesen und der
        // einzelnen Zusatzprodukte in die angegebene XML-Datei
        private bool dateiSchreiben(string dateiname, Type t, object produkt)
        {
            Boolean erfolgreich = false;

            try
            {
                using (TextWriter WriteFileStream = new StreamWriter(this.xmlVerzeichnis + "/" + dateiname))
                {
                    this.xmlSerializer = new XmlSerializer(t);

                    this.xmlSerializer.Serialize(WriteFileStream, produkt); 

                    // Schließen nicht vergessen
                    WriteFileStream.Close();

                    erfolgreich = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }                            

            return erfolgreich;
        }

        // Methode zum Schreiben der Listen für Tapeten und Fliesen und der
        // einzelnen Zusatzprodukte in die angegebene XML-Datei
        private T dateiLesen<T>(string dateiname, Type t)
        {
            T rueckgabe = default(T);

            try
            {
                using (TextReader ReadFileStream = new StreamReader(this.xmlVerzeichnis + "/" + dateiname))
                {
                    this.xmlSerializer = new XmlSerializer(t);

                   rueckgabe = (T)this.xmlSerializer.Deserialize(ReadFileStream);

                    // Schließen nicht vergessen
                    ReadFileStream.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            return rueckgabe;
        }
    }
}
