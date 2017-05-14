﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using tfmarkt.Kalkulation;
using tfmarkt.Produktklassen;

namespace tfmarkt.Ausgabe
{
    /// <summary>
    /// Interaktionslogik für Ausgabe.xaml
    /// </summary>
    public partial class Ausgabe : Window
    {
        // Klassen Member der Klasse XmlDatei
        public List<Ergebnis> ergebnisse { get; set; }
        private DateTime erzeugungsDatum;

        // Konstruktor der Klasse Ausgabe
        public Ausgabe(List<Ergebnis> ergebnisse)
        {
            InitializeComponent();

            // Member der Klasse XmlDatei
            this.ergebnisse = ergebnisse;
            this.erzeugungsDatum = DateTime.Now;
        }

        // Erzeugt Leerzeilen zur Formatierung der Ausgabe
        public void leerzeilenEinfuegen(int anzahl)
        {
            for (int i = 0; i < anzahl; i++)
            {
                ListBoxItem leerzeile = new ListBoxItem();
                leerzeile.FontSize = 12;
                leerzeile.Content = "";

                lbAusgabe.Items.Add(leerzeile);
            }
        }

        // Hier wird die eigentliche Ausgabe formatiert und für die
        // Anzeige vorbereitet
        public void ausgabeFormatieren()
        {
            decimal gesamtbetrag = 0;
            int cnt = 1;
            bool produktNameAusgeben = false;
            String kategorie = "",
                   name;
            int anzahl;
            decimal einzelpreisNetto,
                    preisNetto,
                    preisBrutto;


            this.leerzeilenEinfuegen(3);
            this.lbAusgabe.Items.Add(String.Format("Diese Kalkulation wurde für Sie erstellt am: {0:d}", erzeugungsDatum));
            this.leerzeilenEinfuegen(2);
            
            this.kopfZeileErzeugen();
            this.leerzeilenEinfuegen(1);

            foreach (Ergebnis ergebnis in this.ergebnisse)
            {
                if (!kategorie.Equals(ergebnis.produkt.produktName()))
                {
                    if (ergebnis.produkt.GetType() == typeof(Tapetenkleister) 
                        || ergebnis.produkt.GetType() == typeof(Fliesenkleber)
                        || ergebnis.produkt.GetType() == typeof(Fugenmoertel))
                    {
                        if (kategorie.Equals(typeof(Zusatzprodukt).Name))
                        {
                            produktNameAusgeben = false;
                        }
                        else
                        {
                            kategorie = typeof(Zusatzprodukt).Name;
                            produktNameAusgeben = true;
                        }
                    }
                    else
                    {
                        kategorie = ergebnis.produkt.produktName();
                        produktNameAusgeben = true;
                    }
                }
                else
                {
                    produktNameAusgeben = false;
                }

                name = ergebnis.produkt.titel;
                anzahl = ergebnis.anzahlProdukt;
                einzelpreisNetto = Convert.ToDecimal(ergebnis.produkt.preis / 1.19m);
                preisNetto = Convert.ToDecimal(ergebnis.preis / 1.19m);
                preisBrutto = ergebnis.preis;

                lbAusgabe.Items.Add(String.Format("{0,-5}{1,-15}{2,-35}{3,-8}{4,12:C}{5,18:C}{6,15:C}", cnt++, produktNameAusgeben ? kategorie : "", name, anzahl, einzelpreisNetto, preisNetto, preisBrutto));
                gesamtbetrag += ergebnis.preis;
            }

            this.leerzeilenEinfuegen(1);
            this.fussZeileErzeugen(gesamtbetrag);

            this.leerzeilenEinfuegen(3);
            this.lbAusgabe.Items.Add("Danke für Ihr Vertrauen in uns...Ihr tfMarkt Team!!! ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°) ( ͡° ͜ʖ ͡°)");
        }

        // Die statische Kopfzeile wird generiert mit den Überschriften
        // zur Ausgabe der einzelnen Positionen
        private void kopfZeileErzeugen()
        {
            ListBoxItem lbiKopfzeile = new ListBoxItem();

            String kopfzeile = String.Format("{0,-5}{1,-15}{2,-34}{3,-8}{4,15}{5,15}{6,15}", "Pos.", "Kategorie", "Name", "Menge", "Einzelpr. (Netto)", "Preis (Netto)", "Preis (Brutto)");
            String unterstrich = new String('-', 110);

            lbiKopfzeile.FontStyle = FontStyles.Oblique;
            lbiKopfzeile.Content = kopfzeile;
            
            this.lbAusgabe.Items.Add(lbiKopfzeile);
            this.lbAusgabe.Items.Add(unterstrich);
        }

        // Die statische Fusszeile wird generiert mit Ausgabe 
        // des Gesamtbetrags
        private void fussZeileErzeugen(decimal gesamtbetrag)
        {
            String fusszeile = String.Format("{0,94}{1,14:C}", "Gesamtbetrag: ", gesamtbetrag);
            String unterstrich = new String('-', 110);

            this.lbAusgabe.Items.Add(unterstrich);
            this.lbAusgabe.Items.Add(fusszeile);

        }
    }
}
