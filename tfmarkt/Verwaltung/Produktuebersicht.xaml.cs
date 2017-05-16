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
using System.Collections.ObjectModel;
using tfmarkt.Produktklassen;
using tfmarkt.Verwaltung;
using tfmarkt.Katalog;

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Produktübersicht.xaml
    /// </summary>
    public partial class Produktuebersicht : Window
    {
        // Klassen Member der Klasse Verwaltung
        private Produktkatalog meinKatalog;
        private DetailsProduktuebersicht detailAnsicht;
        private Label letzteKategorie;
        private List<Produkt> letzteListe;

        public Type type { get; set; }

        // Konstruktor der Klasse Verwaltung
        public Produktuebersicht(Produktkatalog katalog)
        {
            InitializeComponent();
            // Member der Klasse Verwaltung
            this.meinKatalog = katalog;
            this.detailAnsicht = new DetailsProduktuebersicht(this, katalog);
        }

        // Aktion beim Drücken des Button Zusatzprodukte
        private void btZusatzprodukte_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.letzteListe = new List<Produkt>(this.meinKatalog.zusatzprodukte);

            this.fuelleDataGrid();
            this.Title = "Produktübersicht: Zusatzprodukte";
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
            this.Title = "Produktübersicht: Fliesen";
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
            this.Title = "Produktübersicht: Tapeten";
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
            List<GridProduktKatalog> anzeigendeProdukte = new List<GridProduktKatalog>();
            double breite;
            int anzahlSpalten = 4;

            foreach (Produkt p in this.letzteListe)
            {
                GridProduktKatalog tmp = new GridProduktKatalog(p.GetType());
                tmp.Artikelnummer = p.artikelnummer;
                tmp.Titel = p.titel;
                tmp.Preis = p.preis;

                anzeigendeProdukte.Add(tmp);
            }

            // Zuweisen der Daten zum DataGrid
            this.dataGrid.ItemsSource = anzeigendeProdukte;

            this.dataGrid.Columns[4].Visibility = Visibility.Hidden;
            this.dataGrid.Columns[1].CanUserSort = false;

            breite = this.dataGrid.ActualWidth - 15;

            this.dataGrid.Columns[0].Width = breite / anzahlSpalten;
            this.dataGrid.Columns[1].Width = breite / anzahlSpalten;
            this.dataGrid.Columns[2].Width = breite / anzahlSpalten;
            this.dataGrid.Columns[3].Width = breite / anzahlSpalten;
        }

        // Wird ausgelöst wenn ein Produkt gelöscht werden soll
        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            GridProduktKatalog auswahl = (GridProduktKatalog)this.dataGrid.SelectedItem;
            Produkt produkt;

            if (auswahl != null)
            {
                //MessageBox.Show(this, auswahl.ToString());
                produkt = this.meinKatalog.getProdukt(auswahl.Artikelnummer, false);

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

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid tmpGrid = (DataGrid)sender;

            if (tmpGrid.Items.Count == 0 || e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            GridProduktKatalog auswahl = (GridProduktKatalog)this.dataGrid.SelectedItem;
            Produkt produkt;

            if (auswahl != null)
            {
                //MessageBox.Show(this, auswahl.ToString());
                produkt = this.meinKatalog.getProdukt(auswahl.Artikelnummer, false);

                //d = new Details(produkt.GetType(), Details.Bearbeitung.istBearbeitung);
                //d.Owner = this;

                this.detailAnsicht.produkt = produkt;
                this.detailAnsicht.produktTyp = produkt.GetType();

                this.detailAnsicht.Owner = this;
                this.detailAnsicht.anpassungDetailAnsicht();
                this.detailAnsicht.detailsFuellen(produkt);

                this.detailAnsicht.ShowDialog();
            }
            else
                MessageBox.Show(this, "Nichts ausgewählt zum Anzeigen.");
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            this.Close();
        }

        // Hervorheben eines Labels, wenn die Maus darüber ist
        private void label_highlightMouseEnter(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Foreground = Brushes.White;
            tmp.Background = Brushes.Black;
        }

        // Zur+cksetzen eines Labels, wenn die Maus nicht mehr darüber ist
        private void label_highlightMouseLeave(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Background = Brushes.White;
            tmp.Foreground = Brushes.Black;
        }

        // Beim Schließen der Verwaltung müssen noch die Ressourcen für die Details freigegeben werden
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.detailAnsicht.Close();
        }
    }

    // Klasse die nur dazu dient, die Anzeige für das DataGrid bereitzustellen
    internal class GridProduktKatalog : ObservableCollection<Produkt> 
    {
        public string Produkt { get; set; }
        public string Artikelnummer { get; set; }
        public string Titel { get; set; }
        public decimal Preis { get; set; }

        private Type type;

        public GridProduktKatalog(Type type)
        {
            this.type = type;
            this.Produkt = umlauteAnpassen(type.Name);
        }

        private string umlauteAnpassen(string name)
        {
            name = name.Replace("ue", "ü");
            name = name.Replace("ae", "ä");
            name = name.Replace("oe", "ö");
            name = name.Replace("Ue", "Ü");
            name = name.Replace("Ae", "A");
            name = name.Replace("Oe", "Ö");

            return name;
        }

        public override string ToString()
        {
            return String.Format("Hallo ich bin {0} und vom Typ {1}", this.Titel, this.type);
        }
    }
}
