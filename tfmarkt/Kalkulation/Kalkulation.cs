using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tfmarkt.Verwaltung;
using tfmarkt.Produktklassen;
using tfmarkt.Katalog;

namespace tfmarkt.Kalkulation
{
    class Kalkulation
    {
        public int flaecheTapeten { get; set; }
        public int flaecheFliesen { get; set; }
        public List<Raum> raeume { get; set; }
        public List<Zusatzprodukt> zusatzprodukte { get; set; }
        public Dictionary<string, int> produktlisteTapetenrollen { get; set; }
        public Dictionary<string, int> produktlisteFliesenpakete { get; set; }
        public List<Ergebnis> ergebnisse { get; set; }
        public Produktkatalog produktkatalog { get; set; }

        public Kalkulation(int anzahlRaeume = 1, Produktkatalog produktkatalog = null)
        {
            this.produktlisteFliesenpakete = new Dictionary<string, int>();
            this.produktlisteTapetenrollen = new Dictionary<string, int>();
            this.zusatzprodukte = new List<Zusatzprodukt>();
            this.raeume = new List<Raum>();
            if (produktkatalog != null){
                this.produktkatalog = produktkatalog;
            }

            /* Testdaten */
            List<Wand> waende = new List<Wand>();
            for (int i= 4; i > 0; i--)
            {
                Wand wand = new Wand(250, 400);
                wand.tapete = (Tapetenrolle)produktkatalog.getProdukt("T001");
                waende.Add(wand);
            }
            for (int i = 4; i > 0; i--)
            {
                Wand wand = new Wand(250, 400);
                wand.tapete = (Tapetenrolle)produktkatalog.getProdukt("T003");
                waende.Add(wand);
            }
            for (int i = 8; i > 0; i--)
            {
                Wand wand = new Wand(500, 250);
                wand.tapete = (Tapetenrolle)produktkatalog.getProdukt("T004");
                waende.Add(wand);
            }
            

            List<Boden> boeden = new List<Boden>();

            Boden boden = new Boden(500, 400);
            boden.fliesen = (Fliesenpaket)produktkatalog.getProdukt("F003");
            boeden.Add(boden);

            Boden boden1 = new Boden(500, 500);
            boden1.fliesen = (Fliesenpaket)produktkatalog.getProdukt("F003");
            boeden.Add(boden1);

            boden1 = new Boden(250, 380);
            boden1.fliesen = (Fliesenpaket)produktkatalog.getProdukt("F004");
            boeden.Add(boden1);

            this.raeume.Add(new Raum("erster Raum", waende, boeden));

            this.zusatzprodukte.Add(produktkatalog.getZusatzprodukt(0));
            this.zusatzprodukte.Add(produktkatalog.getZusatzprodukt(1));
            this.zusatzprodukte.Add(produktkatalog.getZusatzprodukt(2));
            
            /* Testdaten Ende */
        }

        public void addRaum(Raum raum)
        {
            this.raeume.Add(raum);
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
                Ergebnis ergebnis = null;
                List<Ergebnis> zwischenergebnisse = new List<Ergebnis>();
                foreach (Raum raum in this.raeume)
                {
                    foreach (Wand wand in raum.waende)
                    {
                        if (wand.tapete.artikelnummer == this.produktkatalog.getProdukt(key).artikelnummer)
                        {
                            if (((Tapetenrolle)this.produktkatalog.getProdukt(key)).rapport > 0)
                            {
                                zwischenergebnisse.Add(this.berechneTapete(wand.laenge, wand.breite, (Tapetenrolle)this.produktkatalog.getProdukt(key)));
                            }
                            else
                            {
                                flaecheWaende += wand.getFlaeche();
                            }
                        }
                    }
                }
                if (((Tapetenrolle)this.produktkatalog.getProdukt(key)).rapport == 0)
                {
                    ergebnis = this.berechneTapete(flaecheWaende, (Tapetenrolle)this.produktkatalog.getProdukt(key));
                }
                else
                {
                    int anzahl = 0;
                    int flaeche = 0;
                    decimal preis = 0;

                    foreach (Ergebnis zwischenergebnis in zwischenergebnisse)
                    {
                        anzahl += zwischenergebnis.anzahlProdukt;
                        flaeche += zwischenergebnis.flaecheGesamt;
                        preis += zwischenergebnis.preis;
                    }
                    ergebnis = new Ergebnis(flaeche, anzahl, preis, (Tapetenrolle)this.produktkatalog.getProdukt(key));
                }
                if(ergebnis != null)
                {
                    ergebnisse.Add(ergebnis);
                }
            }
            foreach (string key in produktlisteFliesenpakete.Keys)
            {
                int flaecheBoeden = 0;
                foreach (Raum raum in this.raeume)
                {
                    foreach (Boden boden in raum.boeden)
                    {
                        if (boden.fliesen.artikelnummer == this.produktkatalog.getProdukt(key).artikelnummer)
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
            this.berechneZusatzprodukte();
        }

        private void updateProduktlisten()
        {
            this.produktlisteFliesenpakete.Clear();
            this.produktlisteTapetenrollen.Clear();
            foreach(Raum raum in raeume)
            {
                foreach(Wand wand in raum.waende)
                {
                    if(wand.tapete != null)
                    {
                        this.addProduktlisteTapeten(wand.tapete);
                    }
                }
                foreach(Boden boden in raum.boeden)
                {
                    if(boden.fliesen != null)
                    {
                        this.addProduktlisteFliesen(boden.fliesen);
                    }
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
                this.produktlisteTapetenrollen[tapetenrolle.artikelnummer] += 1;
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
                this.produktlisteFliesenpakete[fliesenpaket.artikelnummer] += 1;
            }
        }

        protected Ergebnis berechneTapete(int flaeche, Tapetenrolle produkt)
        {
            Ergebnis ergebnis = null;
            int anzahl = (int)Math.Ceiling(1f * flaeche/(produkt.getFlaeche())); ///10000f));
            decimal preis = anzahl * produkt.preis;

            ergebnis = new Ergebnis(flaeche, anzahl, preis, produkt);
            return ergebnis;
        }

        protected Ergebnis berechneTapete(int laenge, int breite, Tapetenrolle produkt)
        {   
            Ergebnis ergebnis = null;
            
            int BahnenanzahlGesamt = Convert.ToInt32(Math.Ceiling((breite *1f / produkt.breite)));
            int LaengeEinerBahnSchritt1 = Convert.ToInt32(Math.Ceiling((laenge * 1f / produkt.rapport)));
            int LaengeEinerBahnSchritt2 = Convert.ToInt32(Math.Ceiling((produkt.rapport * 1f * LaengeEinerBahnSchritt1)));
            int anzahlBahnenProRolle = Convert.ToInt32(Math.Floor((produkt.laenge * 1f / LaengeEinerBahnSchritt2)));
            int anzahlRollen = Convert.ToInt32(Math.Ceiling((BahnenanzahlGesamt * 1f / anzahlBahnenProRolle)));

            decimal preis = anzahlRollen * produkt.preis;

            ergebnis = new Ergebnis(laenge * breite, anzahlRollen, preis, produkt);
            
            return ergebnis;
        }

        protected Ergebnis berechneFliese(int flaeche, Fliesenpaket produkt)
        {
            Ergebnis ergebnis = null;

            int anzahl = (int) Math.Ceiling((flaeche * 1f /produkt.getFlaeche())*1.05);
            decimal preis = anzahl*produkt.preis;

            ergebnis = new Ergebnis(flaeche, anzahl, preis, produkt);

            return ergebnis;
        }

        public void berechneZusatzprodukte()
        {
            this.flaecheTapeten = 0;
            this.flaecheFliesen = 0;
            foreach (Ergebnis ergebnis in this.ergebnisse)
            {
                switch (ergebnis.produkt.GetType().Name)
                {
                    case "Fliesenpaket":
                        this.flaecheFliesen += ergebnis.flaecheGesamt;
                        break;
                    case "Tapetenrolle":
                        this.flaecheTapeten += ergebnis.flaecheGesamt;
                        break;
                }
            }
            foreach(Zusatzprodukt zusatzprodukt in this.zusatzprodukte)
            {
                Ergebnis ergebnis = new Ergebnis();
                if(zusatzprodukt.GetType() == typeof(Tapetenkleister))
                {
                    ergebnis.flaecheGesamt = this.flaecheTapeten;
                    ergebnis.anzahlProdukt = Convert.ToInt32(Math.Ceiling((this.flaecheTapeten * 1f)/10000 / zusatzprodukt.getFlaeche()));
                    ergebnis.produkt = zusatzprodukt;
                    ergebnis.preis = ergebnis.anzahlProdukt * zusatzprodukt.preis;
                }
                if (zusatzprodukt.GetType() == typeof(Fliesenkleber))
                {
                    ergebnis.flaecheGesamt = this.flaecheFliesen;
                    ergebnis.anzahlProdukt = Convert.ToInt32(Math.Ceiling((this.flaecheFliesen * 1f) / 10000 / zusatzprodukt.getFlaeche()));
                }
                if (zusatzprodukt.GetType() == typeof(Fugenmoertel))
                {
                    ergebnis.flaecheGesamt = this.flaecheFliesen;
                    ergebnis.anzahlProdukt = Convert.ToInt32(Math.Ceiling((this.flaecheFliesen * 1f ) / 10000 / zusatzprodukt.getFlaeche()));
                }
                ergebnis.produkt = zusatzprodukt;
                ergebnis.preis = ergebnis.anzahlProdukt * zusatzprodukt.preis;

                this.ergebnisse.Add(ergebnis);
            }
        }

    }
}
