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
using tfmarkt.Produktklassen;
using tfmarkt.Katalog;

namespace tfmarkt.Kalkulation
{
    /// <summary>
    /// Interaktionslogik für KalkulationWindow.xaml
    /// </summary>
    public partial class KalkulationWindow : Window
    {
        private Produktkatalog katalog;
        public KalkulationWindow(Produktkatalog katalog)
        {
            InitializeComponent();
            this.katalog = katalog;
        }

        private void ShowDetail(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
            List<Wand> waende = new List<Wand>();
            for (int i = 4; i > 0; i--)
            {
                Wand wand = new Wand(250, 400);
                wand.tapete = (Tapetenrolle)katalog.getProdukt("T001");
                waende.Add(wand);
            }


            List<Boden> boeden = new List<Boden>();

            Boden boden = new Boden(500, 400);
            boden.fliesen = (Fliesenpaket)katalog.getProdukt("F003");
            boeden.Add(boden);

            Boden boden1 = new Boden(500, 500);
            boden1.fliesen = (Fliesenpaket)katalog.getProdukt("F003");
            boeden.Add(boden1);

            lbRaeume.Items.Add(new Raum("erster Raum", waende, boeden));

        }
    }
}
