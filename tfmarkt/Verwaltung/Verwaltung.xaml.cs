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
        private Produktkatalog meinKatalog;

        // Konstruktor der Klasse Verwaltung
        public Verwaltung(Produktkatalog katalog)
        {
           InitializeComponent();

           // Member der Klasse Verwaltung
           this.meinKatalog = katalog;
        }

        // Aktion beim Drücken des Button Zusatzprodukte
        private void btZusatzprodukte_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.fuelleDataGrid(new List<Produkt>(this.meinKatalog.zusatzprodukte));
            //this.dataGrid.ItemsSource = this.meinKatalog.zusatzprodukte;
            this.Title = "Verwaltung: Zusatzprodukte";
        }

        // Aktion beim Drücken des Button Fliesen
        private void btFliesen_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.fuelleDataGrid(new List<Produkt>(this.meinKatalog.fliesen));
            //this.dataGrid.ItemsSource = this.meinKatalog.fliesen;
            this.Title = "Verwaltung: Fliesen";
        }

        // Aktion beim Drücken des Button Tapeten
        private void btTapeten_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = null;
            this.dataGrid.Items.Clear();

            this.fuelleDataGrid(new List<Produkt>(this.meinKatalog.tapeten));

            this.Title = "Verwaltung: Tapeten";
        }

        // Füllt das DataGrid mit Produkten
        private void fuelleDataGrid(List<Produkt> quelle)
        {
            List<GridProdukt> anzeigendeProdukte = new List<GridProdukt>();

            foreach (Produkt p in quelle)
            {
                GridProdukt tmp = new GridProdukt();
                tmp.artikelnummer = p.artikelnummer;
                tmp.titel = p.titel;
                tmp.preis = p.preis;

                anzeigendeProdukte.Add(tmp);
            }

            // Zuweisen der Daten zum DataGrid
            this.dataGrid.ItemsSource = anzeigendeProdukte;

            this.dataGrid.Columns[3].Visibility = Visibility.Hidden;
            this.dataGrid.Columns[1].CanUserSort = false;
        }
    }

    // Klasse die nur dazu dient, die Anzeige im DataGrid zu regeln
    internal class GridProdukt : ObservableCollection<Produkt> 
    {
        public string artikelnummer { get; set; }
        public string titel { get; set; }
        public decimal preis { get; set; }
    }
}
