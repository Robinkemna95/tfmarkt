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
        public Dictionary<string, int> produktlisteFliesenpaket { get; set; }

        public Kalkulation(int anzahlRaeume = 1)
        {
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
            foreach (string key in produktlisteTapetenrollen.Keys)
            {
                int flaecheWaende = 0;
                foreach (Raum raum in this.raeume)
                {
                    foreach (Wand wand in raum.waende)
                    {
                        flaecheWaende += wand.getFlaeche();
                    }
                }

                tapetenrolle.getFlaeche();
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
            if (!this.produktlisteTapetenrollen.Contains(tapetenrolle))
            {
                this.produktlisteTapetenrollen.Add(tapetenrolle);
            }
        }

        private void addProduktlisteFliesen(Fliesenpaket fliesenpaket)
        {
            if (!this.produktlisteFliesenpakete.Contains(fliesenpaket))
            {
                this.produktlisteFliesenpakete.Add(fliesenpaket);
            }
        }

        protected void berechneWand(Wand wand)
        {

        }

        protected void berechneBoden(Boden boden)
        {

        }
    }
}
