using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Kalkulation
{
    public class Raum
    {
        public List<Boden> boeden { get; set; }
        public List<Wand> waende { get; set; }
        public string name { get; set; }

        public Raum(string name = "Raum_1") : this(name, null, null) // Ist nur da um zu zeigen dass wir das können! (╯°□°）╯︵ ┻━┻)
        {

        }

        public Raum(string name = "", List<Wand> waende = null, List<Boden> boeden = null)
        {
            this.boeden = new List<Boden>();
            this.waende = new List<Wand>();
            this.name = name;
            if (waende != null)
            {
                this.addWaende(waende);
            }
            if (boeden != null)
            {
                this.addBoeden(boeden);
            }
        }

        private void addWaende(List<Wand> waende)
        {
            foreach (Wand wand in waende)
            {
                this.waende.Add(wand);
            }
        }

        private void addBoeden(List<Boden> boeden)
        {
            foreach (Boden boden in boeden)
            {
                this.boeden.Add(boden);
            }
        }

        public Wand neueWand(int breite, int laenge)
        {
            Wand neueWand = new Wand(breite, laenge);
            this.waende.Add(neueWand);
            return neueWand;
        }

        public bool loescheWand(Wand wand)
        {
            bool geloescht = false;

            try
            {
                if (this.waende.Contains(wand))
                {
                    waende.Remove(wand);
                    geloescht = true;
                }
            }
            catch (Exception)
            {

            }
            return geloescht;
        }

        public Boden neuerBoden(int breite, int laenge)
        {
            Boden neuerBoden = new Boden(breite, laenge);
            this.boeden.Add(neuerBoden);
            return neuerBoden;
        }

        public bool loescheBoden(Boden boden)
        {
            bool geloescht = false;

            try
            {
                if (this.boeden.Contains(boden))
                {
                    this.boeden.Remove(boden);
                    geloescht = true;
                }
            }
            catch (Exception)
            {

            }
            return geloescht;
        }

        override public string ToString()
        {
            return this.name;
        }
    }
}
