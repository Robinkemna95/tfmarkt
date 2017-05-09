using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Produktklassen;
using tfmarkt.Daten;

namespace tfmarkt.Katalog
{
    // Der Produktkatalog enthält alle Tapeten, Fliesen und Zusatzprodukte die der tfmarkt verkauft
    // Über das Interface Datenhandler werden die Produktinformationen unabhängig von der Quelle in
    // den Produktkatalog hineingeladen oder gespeichert
    public class Produktkatalog
    {
        // Klassen Member der Klasse Produktkatalog
        public List<string> artikelnummern { get; private set; }
        public List<Tapetenrolle> tapeten { get; private set; }
        public List<Fliesenpaket> fliesen { get; private set; }
        public List<Zusatzprodukt> zusatzprodukte { get; private set; }
        
        public IDatenhandler datenhandler { get; private set; }

        // Konstruktor der Klasse Produktkatalog
        public Produktkatalog(IDatenhandler daten)
        {
            // Member der Klasse Produktkatalog
            this.artikelnummern = new List<string>(103);
            this.tapeten = new List<Tapetenrolle>(50);
            this.fliesen = new List<Fliesenpaket>(50);
            this.zusatzprodukte = new List<Zusatzprodukt>(3);

            // Laden der Daten aus dem Datenhandler, hier wird nur das Interface benötigt
            datenhandler = daten;
            datenhandler.fuelleProduktkatalog(this);
        }

        //Holt die Tapete mit den Index aus der Liste der Tapeten
        public Tapetenrolle getTapetenrolle(int index)
        {
            return this.tapeten[index];
        }

        //Holt das Fliesenpaket mit den Index aus der Liste der Fliesenpakete
        public Fliesenpaket getFliesenpaket(int index)
        {
            return this.fliesen[index];
        }

        //Holt das Zusatzprodukt mit den Index aus der Liste der Zusatzprodukte
        public Zusatzprodukt getZusatzprodukt(int index)
        {
            return this.zusatzprodukte[index];
        }

        // Holt ein Produkt heraus zu dem die Artikelnummer passt
        public Produkt getProdukt(string artikelnummer)
        {
            return null;
        }

        // Fügt dem Produktkatalog eine Tapete hinzu
        public Boolean addTapete(Tapetenrolle tapete)
        {
            Boolean erfolgreich = false;

            try
            {
                if (!artikelnummern.Contains(tapete.artikelnummer))
                {
                    tapeten.Add(tapete);

                    this.artikelnummern.Add(tapete.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception){}


            return erfolgreich;
        }

        // Löscht eine Tapete aus dem Produktkatalog
        public Boolean deleteTapete(Tapetenrolle tapete)
        {
            Boolean erfolgreich = false;

            try
            {
                if (artikelnummern.Contains(tapete.artikelnummer))
                {
                    tapeten.Remove(tapete);

                    artikelnummern.Remove(tapete.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception) { }


            return erfolgreich;
        }

        // Fügt dem Produktkatalog ein Fliesenpaket hinzu
        public Boolean addFliese(Fliesenpaket fliese)
        {
            Boolean erfolgreich = false;

            try
            {
                if (!artikelnummern.Contains(fliese.artikelnummer))
                {
                    fliesen.Add(fliese);

                    artikelnummern.Add(fliese.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception) { }


            return erfolgreich;
        }

        // Löscht eie Fliesenpaket aus dem Produktkatalog
        public Boolean deleteFliese(Fliesenpaket fliese)
        {
            Boolean erfolgreich = false;

            try
            {
                if (artikelnummern.Contains(fliese.artikelnummer))
                {
                    fliesen.Remove(fliese);

                    artikelnummern.Remove(fliese.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception) { }


            return erfolgreich;
        }

        // Fügt dem Produktkatalog ein Zusatzprodukt hinzu
        public Boolean addZusatzprodukt(Zusatzprodukt zusatzProdukt)
        {
            Boolean erfolgreich = false;

            try
            {
                if (!artikelnummern.Contains(zusatzProdukt.artikelnummer))
                {
                    zusatzprodukte.Add(zusatzProdukt);

                    artikelnummern.Add(zusatzProdukt.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception) { }


            return erfolgreich;
        }

        // Löscht eie Fliesenpaket aus dem Produktkatalog
        public Boolean deleteZusatzprodukt(Zusatzprodukt zusatzProdukt)
        {
            Boolean erfolgreich = false;

            try
            {
                if (artikelnummern.Contains(zusatzProdukt.artikelnummer))
                {
                    zusatzprodukte.Remove(zusatzProdukt);

                    artikelnummern.Remove(zusatzProdukt.artikelnummer);
                }
                else
                {
                    return erfolgreich;
                }

                erfolgreich = true;
            }
            catch (Exception) { }


            return erfolgreich;
        }

        // Ruft den Ladevorgang für den Produktkatalog auf
        public void ladeProduktdaten()
        {
            this.datenhandler.fuelleProduktkatalog(this);
        }

        // Ruft den Speichervorgang für den Produktkatalog auf
        public void sichereProduktdaten()
        {
            this.datenhandler.sichereProduktkatalog(this);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("Ich bin die Klasse " + this.GetType() + "\n");
            sb.Append("Anzahl Tapeten: " + this.tapeten.Count + "\n");
            sb.Append("Anzahl Fliesen: " + this.fliesen.Count + "\n");
            sb.Append("Anzahl Zusatzprodukte: " + this.zusatzprodukte.Count + "\n");

            return sb.ToString();
        }
    }
}
