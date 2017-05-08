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

        public void berechneAlleRaeume()
        {
            foreach(Raum raum in this.raeume)
            {
                this.berechneRaum(raum);
            }
        }

        public void berechneRaum(Raum raum)
        {
            foreach(Wand wand in raum.waende)
            {
                this.berechneWand(wand);
            }
            foreach(Boden boden in raum.boeden)
            {
                this.berechneBoden(boden);
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
