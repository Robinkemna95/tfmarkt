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
using tfmarkt.Produktklassen;
using tfmarkt.Katalog;

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für DetailsProduktuebersicht.xaml
    /// </summary>
    public partial class DetailsProduktuebersicht : Window
    {
        // Klassen Member der Klasse Details
        public Type produktTyp { get; set; }
        public Produkt produkt { get; set; }

        private Produktuebersicht uebersicht;
        private Produktkatalog meinKatalog;

        public DetailsProduktuebersicht(Produktuebersicht uebersicht, Produktkatalog katalog)
        {
            InitializeComponent();

            // Member der Klasse Details
            this.produktTyp = produktTyp;
            this.uebersicht = uebersicht;
            this.meinKatalog = katalog;

            this.tbArtikelnummer.Focus();
            this.anpassungDetailAnsicht();
        }
        // Festlegen der Labels und der Textbox bezogen auf den Produkttyp
        // Die Textboxen tbVar1-3 können variabel benannt sein, abhängig
        // vom Produkttyp der gerade angezeigt wird
        public void anpassungDetailAnsicht()
        {
            this.standardAnsicht();

            if (this.produktTyp == typeof(Tapetenrolle))
            {
                this.lbVar1.Content = "Laenge (cm) :";
                this.lbVar2.Content = "Breite (cm) :";
                this.lbVar3.Content = "Rapport (cm) :";
            }
            else if (this.produktTyp == typeof(Fliesenpaket))
            {

                this.lbVar1.Content = "Laenge (cm) :";
                this.lbVar2.Content = "Breite (cm) :";
                this.lbVar3.Content = "Anzahl (stk) :";
            }
            else if (this.produktTyp == typeof(Tapetenkleister))
            {
                this.tbVar3.Visibility = Visibility.Hidden;

                this.lbVar1.Content = "Fläche (m²) :";
                this.lbVar2.Content = "Gewicht (g) :";
                this.lbVar3.Visibility = Visibility.Hidden;
            }
            else if (this.produktTyp == typeof(Fliesenkleber) || this.produktTyp == typeof(Fugenmoertel))
            {
                this.lbVar1.Content = "min Fläche (m²) :";
                this.lbVar2.Content = "max Fläche (m²) :";
                this.lbVar3.Content = "Gewicht (g) :";
            }
        }

        // Herstellung der Standardansicht für die Detailansicht
        private void standardAnsicht()
        {
            this.tbArtikelnummer.Content = "";
            this.tbTitel.Content = "";
            this.tbPreis.Content = "";
            this.tbBeschreibung.Content = "";
            this.tbVar1.Content = "";
            this.tbVar2.Content = "";
            this.tbVar3.Content = "";

            this.tbVar3.Visibility = Visibility.Visible;

            this.lbVar3.Visibility = Visibility.Visible;
        }

        // Bearbeitung eines Produktes, dazu müssen die Textboxen gefüllt werden
        public void detailsFuellen(Produkt produkt)
        {
            dynamic d = produkt;

            this.tbArtikelnummer.Content = d.artikelnummer;
            this.tbTitel.Content = d.titel;
            this.tbPreis.Content = "" + d.preis;
            this.tbBeschreibung.Content = d.beschreibung;

            if (produkt.GetType() == typeof(Tapetenrolle) || produkt.GetType() == typeof(Fliesenpaket))
            {
                this.tbVar1.Content = "" + d.laenge;
                this.tbVar2.Content = "" + d.breite;

                if (produkt.GetType() == typeof(Tapetenrolle))
                    this.tbVar3.Content = "" + d.rapport;
                else
                    this.tbVar3.Content = "" + d.anzahl;
            }
            else
            {
                if (produkt.GetType() == typeof(Tapetenkleister))
                {
                    this.tbVar1.Content = "" + d.flaeche;
                    this.tbVar2.Content = "" + d.gewicht;
                }
                else
                {
                    this.tbVar1.Content = "" + d.minFlaeche;
                    this.tbVar2.Content = "" + d.maxFlaeche;
                    this.tbVar3.Content = "" + d.gewicht;
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
