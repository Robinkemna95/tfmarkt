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
        public ImageBrush roomIcon = new ImageBrush(new BitmapImage(new Uri(Convert.ToString(System.IO.Path.GetFullPath("../../images/RoomIcon.png")))));
        public ImageBrush addIcon = new ImageBrush(new BitmapImage(new Uri(Convert.ToString(System.IO.Path.GetFullPath("../../images/add.png")))));

        private Produktkatalog katalog;
        public KalkulationWindow(Produktkatalog katalog)
        {
            InitializeComponent();
            this.katalog = katalog;
            this.roomIcon.Stretch = Stretch.None;
            this.roomIcon.Opacity = 0.5;
            lbRaeume.Background = this.roomIcon;

            this.addIcon.Stretch = Stretch.None;
            this.addIcon.AlignmentX = AlignmentX.Right;
            this.addIcon.AlignmentY = AlignmentY.Center;
            AddBoden.Background = this.addIcon;
            AddWand.Background = this.addIcon;
            
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

        

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            this.Close();
        }

        private void Label_HoverIn(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Foreground = Brushes.White;
            tmp.Background = Brushes.Black;
        }

        private void Label_HoverOut(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Background = Brushes.White;
            tmp.Foreground = Brushes.Black;
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            // Hie wird eine Kalkulation vorbereitet und die Ausgabe formatiert und angezeigt
            Kalkulation testKalkulation = new Kalkulation(0, katalog);
            testKalkulation.kalkuliere();

            Ausgabe.Ausgabe ausgabe = new Ausgabe.Ausgabe(testKalkulation.ergebnisse);
            ausgabe.Owner = this;
            ausgabe.ausgabeFormatieren();
            ausgabe.ShowDialog();
        }

        private void addNewRoom(object sender, MouseButtonEventArgs e)
        {
            RaumName raumName = new RaumName(this);
            raumName.Owner = this;
            raumName.ShowDialog();
        }

        private void selectionChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            Raum selectedRaum = (Raum)listBox.SelectedItem;

            if (selectedRaum != null)
            {
                RaumUeberschrift.Content = "Sie haben \""+selectedRaum.name+"\" ausgewählt";
                WaendeGrid.ItemsSource = selectedRaum.waende;
                BoedenGrid.ItemsSource = selectedRaum.boeden;
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Button addButton = (Button)sender;
            AddItem item = new AddItem(addButton.Name);
            item.Owner = this;
            item.ShowDialog();
        }
    }
}
