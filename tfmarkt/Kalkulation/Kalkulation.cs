using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Katalog;
using tfmarkt.Produktklassen;

namespace tfmarkt.Kalkulation
{
    class Kalkulation
    {
        public List<Raum> raeume { get; set; }
        public List<Zusatzprodukt> zusatzprodukte { get; set; }
        public Dictionary<string, int> produktlisteTapetenrollen { get; set; }
        public Dictionary<string, int> produktlisteFliesenpakete { get; set; }
        public List<Ergebnis> ergebnisse { get; set; }
        public Produktkatalog produktkatalog { get; set; }

        public Kalkulation(int anzahlRaeume = 1, Produktkatalog produktkatalog = null)
        {
            if(produktkatalog != null){
                this.produktkatalog = produktkatalog;
            }
            this.raeume.Add(new Raum("erster Raum"));
        }

        public bool loescheRaum(Raum raum)
        {
            bool geloescht = false;

            try
            {
                if (this.raeume.Contains(raum))
                {
                    this.raeume.Remove(raum);
                    geloescht = true;
                }
            }
            catch (Exception)
            {

            }
            return geloescht;
        }

        public void kalkuliere()
        {
            this.updateProduktlisten();
            this.ergebnisse = new List<Ergebnis>();
            
            foreach (string key in produktlisteTapetenrollen.Keys)  // Produkt
            {
                int flaecheWaende = 0;
                foreach (Raum raum in this.raeume)
                {
                    foreach (Wand wand in raum.waende)
                    {
                        if (wand.tapete == this.produktkatalog.getProdukt(key))
                        {
                            flaecheWaende += wand.getFlaeche();
                        }
                    }
                }
                // Hier weiter machen
                /*if(this.produktkatalog.getProdukt(key))
                Ergebnis ergebnis = this.berechneTapete();*/
                
                
            }
            foreach (string key in produktlisteFliesenpakete.Keys)
            {
                int flaecheBoeden = 0;
                foreach (Raum raum in this.raeume)
                {
                    foreach (Boden boden in raum.boeden)
                    {
                        if (boden.fliesen == this.produktkatalog.getProdukt(key))
                        {
                            flaecheBoeden += boden.getFlaeche();
                        }
                    }
                }
                Ergebnis ergebnis = this.berechneFliese(flaecheBoeden, (Fliesenpaket)this.produktkatalog.getProdukt(key));
                if (ergebnis != null)
                {
                    ergebnisse.Add(ergebnis);
                }
            }    
        }

        private void updateProduktlisten()
        {
            foreach(Raum raum in raeume)
            {
                foreach(Wand wand in raum.waende)
                {
                    this.addProduktlisteTapeten(wand.tapete);
                }
                foreach(Boden boden in raum.boeden)
                {
                    this.addProduktlisteFliesen(boden.fliesen);
                }
            }
        }

        private void addProduktlisteTapeten(Tapetenrolle tapetenrolle)
        {
            if (!this.produktlisteTapetenrollen.ContainsKey(tapetenrolle.artikelnummer))
            {
                this.produktlisteTapetenrollen.Add(tapetenrolle.artikelnummer,1);
            }
            else
            {
                this.produktlisteTapetenrollen.Add(tapetenrolle.artikelnummer, this.produktlisteTapetenrollen[tapetenrolle.artikelnummer]++);
            }
        }

        private void addProduktlisteFliesen(Fliesenpaket fliesenpaket)
        {
            if (!this.produktlisteFliesenpakete.ContainsKey(fliesenpaket.artikelnummer))
            {
                this.produktlisteFliesenpakete.Add(fliesenpaket.artikelnummer,1);
            }
            else
            {
                this.produktlisteFliesenpakete.Add(fliesenpaket.artikelnummer, this.produktlisteFliesenpakete[fliesenpaket.artikelnummer]++);
            }
        }

        protected Ergebnis berechneTapete(int flaeche, Tapetenrolle Tapetenrolle)
        {
            Ergebnis ergebnis = null;
            return ergebnis;
        }

        protected Ergebnis berechneTapete(int laenge, int breite, int rapport, Tapetenrolle produkt)
        {
            Ergebnis ergebnis = null;
            return ergebnis;
        }

        protected Ergebnis berechneFliese(int flaeche, Fliesenpaket produkt)
        {
            Ergebnis ergebnis = null;

            int anzahl = (int) Math.Ceiling((flaeche/(int)produkt.getFlaeche())*1.05); // Cast entfernen nach pull
            decimal preis = anzahl*produkt.preis;

            ergebnis = new Ergebnis(flaeche, anzahl, preis, produkt);

            return ergebnis;
        }
    }
}
