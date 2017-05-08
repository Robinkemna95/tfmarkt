using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tfmarkt.Kalkulation
{
    class Raum
    {
        public List<Boden> boeden { get; set; }
        public List<Wand> waende { get; set; }
        public string name { get; set; }

        public Raum(string name = "")
        {
            this.name = name;
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
            catch(Exception)
            {

            }
            return geloescht;
        }
    }
}
