using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using tfmarkt.Katalog;
using tfmarkt.Produktklassen;

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Verwaltung.xaml
    /// </summary>
    public partial class Verwaltung : Window
    {
        // Klassen Member der Klasse Verwaltung
        private Produktkatalog meinKatalog;
        private Details detailAnsicht;
        private Label letzteKategorie;
        private List<Produkt> letzteListe;

        public Type type { get; set; }

        // Konstruktor der Klasse Verwaltung
        public Verwaltung(Produktkatalog katalog)
        {
           InitializeComponent();

           // Member der Klasse Verwaltung
           this.meinKatalog = katalog;
           this.detailAnsicht = new Details(this, katalog);
        }

        // Aktion beim Drücken des Button Zusatzprodukte
        private void btZusatzprodukte_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.letzteListe = new List<Produkt>(this.meinKatalog.zusatzprodukte);

            this.fuelleDataGrid();
            this.Title = "Verwaltung: Zusatzprodukte";
            this.hervorhebenKategorie(btnZusatzprodukte.Content.ToString());

            this.letzteKategorie = (Label)sender;
        }

        // Aktion beim Drücken des Button Fliesen
        private void btFliesen_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.letzteListe = new List<Produkt>(this.meinKatalog.fliesen);

            this.fuelleDataGrid();
            this.Title = "Verwaltung: Fliesen";
            this.hervorhebenKategorie(btnFliesen.Content.ToString());

            this.letzteKategorie = (Label)sender;
        }

        // Aktion beim Drücken des Button Tapeten
        private void btTapeten_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.letzteListe = new List<Produkt>(this.meinKatalog.tapeten);

            this.fuelleDataGrid();
            this.Title = "Verwaltung: Tapeten";
            this.hervorhebenKategorie(btnTapeten.Content.ToString());

            this.letzteKategorie = (Label)sender;
        }

        // Hebt die ausgewählte Kategorie hervor
        private void hervorhebenKategorie(String kategorie)
        {
            switch (kategorie)
            {
                case "Tapeten":
                    lbTapeten.Background = Brushes.Black;
                    lbFliesen.Background = Brushes.White;
                    lbZusatzprodukt.Background = Brushes.White;
                    break;
                case "Fliesen":
                    lbTapeten.Background = Brushes.White;
                    lbFliesen.Background = Brushes.Black;
                    lbZusatzprodukt.Background = Brushes.White;
                    break;
                case "Zusatzprodukte":
                    lbTapeten.Background = Brushes.White;
                    lbFliesen.Background = Brushes.White;
                    lbZusatzprodukt.Background = Brushes.Black;
                    break;
                default:
                    break;
            }
        }

        // Das DataGrid wird über das Auslösen eines Events aktualisiert, wenn z.B. Änderungen
        // am Produktkatalog vorgenommen wurden
        public void aktualisiereDataGrid()
        {
            this.letzteKategorie.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
            {
                RoutedEvent = Mouse.MouseDownEvent,
                Source = this,
            });
        }

        // Füllt das DataGrid mit Produkten
        private void fuelleDataGrid()
        {
            List<GridProdukt> anzeigendeProdukte = new List<GridProdukt>();
            double breite;

            foreach (Produkt p in this.letzteListe)
            {
                GridProdukt tmp = new GridProdukt(p.GetType());
                tmp.artikelnummer = p.artikelnummer;
                tmp.titel = p.titel;
                tmp.preis = p.preis;

                anzeigendeProdukte.Add(tmp);
            }

            // Zuweisen der Daten zum DataGrid
            this.dataGrid.ItemsSource = anzeigendeProdukte;

            this.dataGrid.Columns[3].Visibility = Visibility.Hidden;
            this.dataGrid.Columns[1].CanUserSort = false;

            breite = this.dataGrid.ActualWidth-15;

            this.dataGrid.Columns[0].Width = breite / 3;
            this.dataGrid.Columns[1].Width = breite / 3;
            this.dataGrid.Columns[2].Width = breite / 3;
        }

        // Hinzufügen eines neuen Produktes zum Produktkatalog
        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            if(this.Title.Equals("Verwaltung"))
                return;

            switch (this.Title)
            {
                case "Verwaltung: Tapeten":
                    type = typeof(Tapetenrolle);
                    break;
                case "Verwaltung: Fliesen":
                    type = typeof(Fliesenpaket);
                    break;
                case "Verwaltung: Zusatzprodukte":
                    ZusatzproduktAuswahl za = new ZusatzproduktAuswahl(this);
                    za.Owner = this;
                    za.ShowDialog();
                    break;
            }

            this.detailAnsicht.produktTyp = this.type;
            this.detailAnsicht.art = Details.Bearbeitung.istNeu;

            this.detailAnsicht.Owner = this;
            this.detailAnsicht.anpassungDetailAnsicht();
            this.detailAnsicht.ShowDialog();
        }

        // Bearbeiten des ausgewählten Produktes im Produktkatalog
        private void btnBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            GridProdukt auswahl = (GridProdukt)this.dataGrid.SelectedItem;
            Produkt produkt;

            if (auswahl != null)
            {
                //MessageBox.Show(this, auswahl.ToString());
                produkt = this.meinKatalog.getProdukt(auswahl.artikelnummer, false);

                //d = new Details(produkt.GetType(), Details.Bearbeitung.istBearbeitung);
                //d.Owner = this;

                this.detailAnsicht.produkt = produkt;
                this.detailAnsicht.produktTyp = produkt.GetType();
                this.detailAnsicht.art = Details.Bearbeitung.istBearbeitung;

                this.detailAnsicht.Owner = this;
                this.detailAnsicht.anpassungDetailAnsicht();
                this.detailAnsicht.detailsFuellen(produkt);

                this.detailAnsicht.ShowDialog();
            }
            else
                MessageBox.Show(this, "Nichts ausgewählt zum Bearbeiten.");
        }

        // Beim Schließen der Verwaltung müssen noch die Ressourcen für die Details freigegeben werden
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.detailAnsicht.Close();
        }

        // Wird ausgelöst wenn ein Produkt gelöscht werden soll
        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            GridProdukt auswahl = (GridProdukt)this.dataGrid.SelectedItem;
            Produkt produkt;

            if (auswahl != null)
            {
                //MessageBox.Show(this, auswahl.ToString());
                produkt = this.meinKatalog.getProdukt(auswahl.artikelnummer, false);

                if (!(MessageBox.Show(this, "Wollen Sie das ausgewählte Produkt wirklich löschen?", produkt.GetType().Name + ": Produkt löschen", MessageBoxButton.OKCancel) == MessageBoxResult.OK))
                {
                    return;
                }

                if (produkt.GetType() == typeof(Tapetenrolle))
                {
                    this.meinKatalog.deleteTapete((Tapetenrolle)produkt);
                }
                else if (produkt.GetType() == typeof(Fliesenpaket))
                {
                    this.meinKatalog.deleteFliese((Fliesenpaket)produkt);
                }
                else
                {
                    this.meinKatalog.deleteZusatzprodukt((Zusatzprodukt)produkt);
                }

                this.meinKatalog.sichereProduktdaten();

                this.aktualisiereDataGrid();
            }
            else
                MessageBox.Show(this, "Nichts ausgewählt zum Löschen.");
        }
    }

    // Klasse die nur dazu dient, die Anzeige für das DataGrid bereitzustellen
    internal class GridProdukt : ObservableCollection<Produkt> 
    {
        public string artikelnummer { get; set; }
        public string titel { get; set; }
        public decimal preis { get; set; }

        private Type type;

        public GridProdukt(Type type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return String.Format("Hallo ich bin {0} und vom Typ {1}", this.titel, this.type);
        }
    }
}
