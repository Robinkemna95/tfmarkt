using System;
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
using System.Text.RegularExpressions;
using tfmarkt.Produktklassen;
using tfmarkt.Katalog;

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        // Klassen Member der Klasse Details
        public Type produktTyp { get; set; }
        public Produkt produkt { get; set; }
        public Bearbeitung art { get; set; }

        private Verwaltung verwaltung;
        private Produktkatalog meinKatalog;
        private bool datensatzBearbeitet;
        private string alteArtikelnummer;

        // In der Detailansicht gibt es 2 mögliche Fälle was mit
        // einem Produkt passieren soll
        public enum Bearbeitung
        {
            istNeu,
            istBearbeitung
        };

        public Details(Verwaltung verwaltung, Produktkatalog katalog)
        {
            InitializeComponent();
            
            // Member der Klasse Details
            this.produktTyp = produktTyp;
            this.verwaltung = verwaltung;
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
                this.lbVar1.Content = "Laenge (cm)";
                this.lbVar2.Content = "Breite (cm)";
                this.lbVar3.Content = "Rapport (cm)";
            }
            else if (this.produktTyp == typeof(Fliesenpaket))
            {

                this.lbVar1.Content = "Laenge (cm)";
                this.lbVar2.Content = "Breite (cm)";
                this.lbVar3.Content = "Anzahl (stk)";
            }
            else if(this.produktTyp == typeof(Tapetenkleister))
            {
                this.tbVar3.Visibility = Visibility.Hidden;

                this.lbVar1.Content = "Fläche (cm²)";
                this.lbVar2.Content = "Gewicht (g)";
                this.lbVar3.Visibility = Visibility.Hidden;
            }
            else if (this.produktTyp == typeof(Fliesenkleber) || this.produktTyp == typeof(Fugenmoertel))
            {
                this.lbVar1.Content = "min Fläche (cm²)";
                this.lbVar2.Content = "max Fläche (cm²)";
                this.lbVar3.Content = "Gewicht (g)";                   
            }
        }

        // Herstellung der Standardansicht für die Detailansicht
        private void standardAnsicht()
        {
            this.datensatzBearbeitet = false;

            this.tbArtikelnummer.Text = "";
            this.tbTitel.Text = "";
            this.tbPreis.Text = "";
            this.tbBeschreibung.Text = "";
            this.tbVar1.Text = "";
            this.tbVar2.Text = "";
            this.tbVar3.Text = "";

            this.tbVar3.Visibility = Visibility.Visible;

            this.lbVar3.Visibility = Visibility.Visible;
        }

        // Bearbeitung eines Produktes, dazu müssen die Textboxen gefüllt werden
        public void detailsFuellen(Produkt produkt)
        {
            dynamic d = produkt;

            this.tbArtikelnummer.Text = d.artikelnummer;
            this.alteArtikelnummer = d.artikelnummer;
            this.tbTitel.Text = d.titel;
            this.tbPreis.Text = "" + d.preis;
            this.tbBeschreibung.Text = d.beschreibung;

            if (produkt.GetType() == typeof(Tapetenrolle) || produkt.GetType() == typeof(Fliesenpaket))
            {
                this.tbVar1.Text = "" + d.laenge;
                this.tbVar2.Text = "" + d.breite;

                if (produkt.GetType() == typeof(Tapetenrolle))
                    this.tbVar3.Text = "" + d.rapport;
                else
                    this.tbVar3.Text = "" + d.anzahl;
            }
            else 
            {
                if (produkt.GetType() == typeof(Tapetenkleister))
                {
                    this.tbVar1.Text = "" + d.flaeche;
                    this.tbVar2.Text = "" + d.gewicht;
                }
                else
                {
                    this.tbVar1.Text = "" + d.minFlaeche;
                    this.tbVar2.Text = "" + d.maxFlaeche;
                    this.tbVar3.Text = "" + d.gewicht;
                }
            }

            if(this.art == Bearbeitung.istBearbeitung)
            {
                this.datensatzBearbeitet = false;
            }
        }

        // Testen ob zum Speichern oder bearbeiten alle Felder gefüllt sind
        private bool allesGefuellt()
        {
            bool erfolgreich = false;

            IEnumerable<TextBox> meineButtons = this.buttonGrid.Children.OfType<TextBox>();

            foreach (TextBox tb in meineButtons)
            {
                if (tb.IsVisible)
                {
                    if (tb.Text.Length == 0)
                    {
                        erfolgreich = false;
                        break;
                    }
                    else
                    {
                        erfolgreich = true;
                    }
                }
            }

            return erfolgreich;
        }

        // Aktion beim Drücken des Abbrechen Button
        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            if (this.datensatzBearbeitet)
            {
                if (MessageBox.Show(this, "Wollen Sie wirklich abbrechen?", "Abbrechen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Hide();
                }
            }
            else
            {
                this.Hide();
            }
        }

        // Aktion beim Drücken des Speichern Buttons
        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            decimal preis;

            String artikelnummer,
                       titel,
                       beschreibung;
            int tbVar1,
                tbVar2,
                tbVar3;

            if (!this.allesGefuellt())
            {
                MessageBox.Show(this, "Es müssen alle angezeigten Felder ausgefüllt sein!", "Fehler");
                return;
            }
            
            if(this.meinKatalog.artikelnummerVorhanden(this.tbArtikelnummer.Text, this.produktTyp) && this.art == Bearbeitung.istNeu)
            {
                MessageBox.Show(this, "Eingegebene Artikelnummer ist bereits vorhanden in der Kategorie \"" + this.produktTyp.Name + "\".", "Fehler");
                return;
            }

            if (!decimal.TryParse(this.tbPreis.Text, out preis))
            {
                MessageBox.Show(this, "Der eingegebene Geldwert ist ungültig, bitte korrigieren!!!", "Fehler");
                return;
            }
            else if (this.tbPreis.Text.Contains('.'))
            {
                MessageBox.Show(this, "Der eingegebene Geldwert ist ungültig, als Trennzeichen muss ein \",\" verwendet werden!!!", "Fehler");
                return;
            }

            // Wenn ein neues Produkt dem Katalog hinzugefügt werden soll
            if (this.art == Bearbeitung.istNeu)
            {
                bool produktHinzugefuegt = false;

                artikelnummer = this.tbArtikelnummer.Text.Trim();
                titel = this.tbTitel.Text.Trim();
                beschreibung = this.tbBeschreibung.Text.Trim();
                tbVar1 = int.Parse(this.tbVar1.Text);
                tbVar2 = int.Parse(this.tbVar2.Text);
                tbVar3 = this.tbVar3.IsVisible ? int.Parse(this.tbVar3.Text) : 0;

                if (this.produktTyp == typeof(Tapetenrolle))
                {
                   produktHinzugefuegt = this.meinKatalog.addTapete(new Tapetenrolle(artikelnummer, titel, beschreibung, preis, tbVar1, tbVar2, tbVar3));
                }
                else if (this.produktTyp == typeof(Fliesenpaket))
                {
                    produktHinzugefuegt = this.meinKatalog.addFliese(new Fliesenpaket(artikelnummer, titel, beschreibung, preis, tbVar1, tbVar2, tbVar3));
                }
                else if (this.produktTyp == typeof(Tapetenkleister))
                {
                    produktHinzugefuegt = this.meinKatalog.addZusatzprodukt(new Tapetenkleister(artikelnummer, titel, beschreibung, preis, tbVar1, tbVar2, false));
                }
                else if (this.produktTyp == typeof(Fliesenkleber))
                {
                    produktHinzugefuegt = this.meinKatalog.addZusatzprodukt(new Fliesenkleber(artikelnummer, titel, beschreibung, preis, tbVar1, tbVar2, tbVar3, false));
                }
                else if (this.produktTyp == typeof(Fugenmoertel))
                {
                    produktHinzugefuegt = this.meinKatalog.addZusatzprodukt(new Fugenmoertel(artikelnummer, titel, beschreibung, preis, tbVar1, tbVar2, tbVar3, false));
                }

                if (produktHinzugefuegt)
                {
                    this.meinKatalog.sichereProduktdaten();

                    this.verwaltung.aktualisiereDataGrid();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "Produkt konnte dem Katalog nicht hinzugefügt werden!" +
                        "\nHaben Sie vielleicht versucht dem Katalog ein weiteres zu bereits max. 3 erlaubten Zusatzprodukten hinzuzufügen?", "Fehler beim Produkt hinzufügen");
                }
            } // Wenn ein Produkt aus dem Katalog bearbeitet werden soll
            else if (this.art == Bearbeitung.istBearbeitung)
            {
                bool produktBearbeitet = false;
                dynamic d = this.produkt;

                artikelnummer = this.tbArtikelnummer.Text.Trim();
                titel = this.tbTitel.Text.Trim();
                beschreibung = this.tbBeschreibung.Text.Trim();
                tbVar1 = int.Parse(this.tbVar1.Text);
                tbVar2 = int.Parse(this.tbVar2.Text);
                tbVar3 = this.tbVar3.IsVisible ? int.Parse(this.tbVar3.Text) : 0;

                // Prüfung ob die Artikelnummer geändert wurde, und wenn ja ob die neue
                // Artikelnummer bereits vorhanden ist
                if (!artikelnummer.Equals(d.artikelnummer))
                {
                    if (meinKatalog.artikelnummern.Contains(artikelnummer))
                    {
                        MessageBox.Show(this, "Produkt konnte dem Katalog nicht hinzugefügt werden!" +
                        "\nDie Artikelnummer ist bereits vorhanden, bitte eine freie Artikelnummer verwenden", "Artikelnummer vorhanden");
                        return;
                    }
                    else
                    {
                        meinKatalog.artikelnummern.Remove(d.artikelnummer);
                        meinKatalog.artikelnummern.Add(artikelnummer);
                    }
                }

                d.artikelnummer = artikelnummer;
                d.titel = titel;
                d.preis = preis;
                d.beschreibung = beschreibung;

                if (produkt.GetType() == typeof(Tapetenrolle) || produkt.GetType() == typeof(Fliesenpaket))
                {
                    d.laenge = tbVar1;
                    d.breite = tbVar2;

                    if (produkt.GetType() == typeof(Tapetenrolle))
                        d.rapport = tbVar3;
                    else
                        d.anzahl = tbVar3;
                }
                else
                {
                    if (produkt.GetType() == typeof(Tapetenkleister))
                    {
                        d.flaeche = tbVar1;
                        d.gewicht = tbVar2;
                    }
                    else
                    {
                        d.minFlaeche = tbVar1;
                        d.maxFlaeche = tbVar2;
                        d.gewicht = tbVar3;
                    }
                }

                produktBearbeitet = true;

                if (produktBearbeitet)
                {
                    this.meinKatalog.sichereProduktdaten();

                    this.verwaltung.aktualisiereDataGrid();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(this, "Änderungen am Produkt konnten nicht gespeichert werden!" +
                        "\nBitte kontrollieren Sie die Eingaben auf ihre Korrektheit.", "Fehler beim Produkt hinzufügen");
                }
            }
        }

        private void integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            this.tbArtikelnummer.Focus();
        }

        private void textboxBearbeitet_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.datensatzBearbeitet = true;
        }
    }
}
