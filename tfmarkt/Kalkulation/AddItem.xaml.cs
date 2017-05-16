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
using tfmarkt.Katalog;
using tfmarkt.Produktklassen;
using System.Text.RegularExpressions;

namespace tfmarkt.Kalkulation
{
    /// <summary>
    /// Interaktionslogik für AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public Produktkatalog katalog { get; set; }
        private Raum selectedRaum;
        public AddItem(string type, Produktkatalog katalog, ListBox aktuelleListe)
        {
            InitializeComponent();
            this.katalog = katalog;
            this.selectedRaum = (Raum)aktuelleListe.SelectedItem;
            switch (type)
            {
                case "AddBoden":
                    this.initBodenForm();
                    break;
                case "AddWand":
                    this.initWandForm();
                    break;
            }
        }

        private void initBodenForm()
        {
            List<Fliesenpaket> fliesen = this.katalog.fliesen;
            ComboBoxProdukte.ItemsSource = fliesen;
            ComboBoxProdukte.SelectedIndex = 1;
            buttonCreateItem.Click += createBoden;
        }

        private void initWandForm()
        {
            List<Tapetenrolle> tapeten = this.katalog.tapeten;
            ComboBoxProdukte.ItemsSource = tapeten;
            ComboBoxProdukte.SelectedIndex = 1;
            buttonCreateItem.Click += createWand;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void createBoden(object sender, RoutedEventArgs e)
        {
            int laenge = Convert.ToInt32(Laenge.Text);
            int breite = Convert.ToInt32(Breite.Text);
            Fliesenpaket fliese = (Fliesenpaket)ComboBoxProdukte.SelectedItem;

            selectedRaum.neuerBoden(breite, laenge).fliesen = fliese;
            
            this.Close();
        }
        private void createWand(object sender, RoutedEventArgs e)
        {
            int laenge = Convert.ToInt32(Laenge.Text);
            int breite = Convert.ToInt32(Breite.Text);
            Tapetenrolle tapete = (Tapetenrolle)ComboBoxProdukte.SelectedItem;

            selectedRaum.neueWand(breite, laenge).tapete = tapete;
            ((KalkulationWindow)this.Owner).updateGrids();
            this.Close();
        }

        private void validateInt(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]{1,8}");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
