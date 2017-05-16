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

namespace tfmarkt.Kalkulation
{
    /// <summary>
    /// Interaktionslogik für AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public Produktkatalog katalog { get; set; }
        public AddItem(string type, Produktkatalog katalog, ListBox aktuelleListe)
        {
            InitializeComponent();
            this.katalog = katalog;
            Raum selectedRaum = (Raum)aktuelleListe.SelectedItem;
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

            ComboBox fliesenListe= new ComboBox(); 
            ComboBoxProdukte.ItemsSource = fliesen;
        }

        private void initWandForm()
        {

        }
    }
}
