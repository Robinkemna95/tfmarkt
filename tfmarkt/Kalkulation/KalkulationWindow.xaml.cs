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
        //public ImageBrush roomIcon = new ImageBrush(new BitmapImage(new Uri(Convert.ToString(System.IO.Path.GetFullPath("images/RoomIcon.png")))));
        //public ImageBrush addIcon = new ImageBrush(new BitmapImage(new Uri(Convert.ToString(System.IO.Path.GetFullPath("images/add.png")))));

        public Kalkulation kalkulation { get; set; }
        private Produktkatalog katalog;
        
        public KalkulationWindow(Produktkatalog katalog, Kalkulation kalkulation)
        {
            InitializeComponent();
            this.kalkulation = kalkulation;
            this.katalog = katalog;

            if (kalkulation.raeume.Count > 0)
            {
                foreach (Raum r in kalkulation.raeume)
	            {
                    lbRaeume.Items.Add(r);
	            }
            }
            /*
            this.roomIcon.Stretch = Stretch.None;
            this.roomIcon.Opacity = 0.5;
            lbRaeume.Background = this.roomIcon;

            this.addIcon.Stretch = Stretch.None;
            this.addIcon.AlignmentX = AlignmentX.Right;
            this.addIcon.AlignmentY = AlignmentY.Center;
            AddBoden.Background = this.addIcon;
            AddWand.Background = this.addIcon;
            */
            
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

            if (this.lbRaeume.Items.Count > 0)
            {
               MessageBox.Show(this, "Ihre Kalkulation bleibt zur weiteren Verarbeitung bestehen.", "Information");
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

        private void Kalkuliere(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }
            if (lbRaeume.Items.Count == 0)
            {
                MessageBox.Show(this, "Bitte zuerst mindestens einen Raum anlegen", "Raum anlegen");
                return;
            }

            MessageBoxResult result = MessageBox.Show(this, "Wollen sie Zusatzprodukte mitberechnen lassen?", "Mit Zusatzprodukten", MessageBoxButton.YesNo);

            this.kalkulation.kalkuliere();
            this.kalkulation.zusatzprodukte.Clear();

            if (result == MessageBoxResult.Yes)
            {
                foreach (Zusatzprodukt zusatzprodukt in this.katalog.zusatzprodukte)
                {
                    if(zusatzprodukt.GetType() ==  typeof(Tapetenkleister))
                    {
                        if (this.kalkulation.produktlisteTapetenrollen.Count > 0)
                        {
                            this.kalkulation.zusatzprodukte.Add(zusatzprodukt);
                        }
                    }
                    else if (this.kalkulation.produktlisteFliesenpakete.Count > 0)
                    {
                        this.kalkulation.zusatzprodukte.Add(zusatzprodukt);
                    }
                }
            }

            this.kalkulation.kalkuliere();
            Ausgabe.Ausgabe ausgabe = new Ausgabe.Ausgabe(this.kalkulation.ergebnisse);
            ausgabe.Owner = this;
            ausgabe.ausgabeFormatieren();
            ausgabe.ShowDialog();
        }

        private void addNewRoom(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton != MouseButton.Left){
                return;
            }
            RaumName raumName = new RaumName(this);
            raumName.Owner = this;
            raumName.ShowDialog();
        }


        private void AddItem(object sender, RoutedEventArgs e)
        {
            if(lbRaeume.Items.Count == 0)
            {
                MessageBox.Show("Bitte erstellen Sie erst einen Raum.");
                return;
            }

            if(lbRaeume.SelectedItem == null){
                MessageBox.Show("Bitte zuerst einen Raum auswählen.");
                return;
            }

            Button addButton = (Button)sender;
            AddItem item = new AddItem(addButton.Name, this.katalog, lbRaeume);
            item.Owner = this;
            item.ShowDialog();
            this.updateGrids();
        }

        private void removeSelectedItem(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }
            if (lbRaeume.Items.Count > 0)
            {
                Raum selectedRaum = (Raum)lbRaeume.SelectedItem;
                if (MessageBoxResult.Yes == MessageBox.Show(this, "Sind Sie sicher dass Sie den ausgewählten Raum \"" + selectedRaum.name + "\" löschen wollen?", "Achtung!", MessageBoxButton.YesNo))
                {
                    lbRaeume.Items.Remove(selectedRaum);
                    if (!this.kalkulation.loescheRaum(selectedRaum))
                    {
                        MessageBox.Show("Es ist ein unerwarteter Fehler aufgetreten und der Raum konnte nicht richtig gelöscht werden. (Dieser Fehler sollte niemals auftreten   (╯°□°）╯︵ ┻━┻     )");
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Es gibt keine Räume die gelöscht werden können.");
            }
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            Raum selectedRaum = (Raum)listBox.SelectedItem;

            if (selectedRaum != null)
            {
                RaumUeberschrift.Content = "Sie haben \"" + selectedRaum.name + "\" ausgewählt";
                WaendeGrid.ItemsSource = selectedRaum.waende;
                BoedenGrid.ItemsSource = selectedRaum.boeden;
            }
            this.updateGrids();
        }

        public void updateGrids()
        {
            if(lbRaeume.Items.Count > 0 && lbRaeume.SelectedItem != null)
            {
                BoedenGrid.ItemsSource = null;
                WaendeGrid.ItemsSource = null;
                BoedenGrid.ItemsSource = ((Raum)lbRaeume.SelectedItem).boeden;
                WaendeGrid.ItemsSource = ((Raum)lbRaeume.SelectedItem).waende;
            }
            else
            {
                BoedenGrid.ItemsSource = null;
                WaendeGrid.ItemsSource = null;
            }
        }

        // DEPRECATED -- Sobald die Kalkulation geöffnet wird, werden alle bereits angelegten Räume angezeigt
        private void deleteKalkulation(object sender, EventArgs e)
        {
            ((MainWindow)this.Owner).resetKalkulation();
        }

        private void deleteWand(object sender, KeyEventArgs e)
        {
            
            if(e.Key == Key.Delete)
            {
                if(MessageBoxResult.Yes == MessageBox.Show(this, "Sind Sie sicher dass Sie die ausgewählte Wand löschen wollen?", "Achtung!", MessageBoxButton.YesNo))
                {
                    foreach(Wand wand in WaendeGrid.SelectedItems)
                    {
                        ((Raum)lbRaeume.SelectedItem).loescheWand(wand);
                    }
                    this.updateGrids();
                }
            }
        }

        private void deleteBoden(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (MessageBoxResult.Yes == MessageBox.Show(this, "Sind Sie sicher dass Sie den ausgewählten Boden löschen wollen?", "Achtung!", MessageBoxButton.YesNo))
                {
                    foreach(Boden boden in BoedenGrid.SelectedItems)
                    {
                        ((Raum)lbRaeume.SelectedItem).loescheBoden(boden);
                    }
                    this.updateGrids();
                }   
            }
        }

        private void deleteAll(object sender, MouseButtonEventArgs e)
        {
            if (lbRaeume.Items.Count == 0 || e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            if (MessageBoxResult.Yes != MessageBox.Show(this, "Wollen Sie wirklich die gesamte Kalkulation löschen?", "Wirklich löschen", MessageBoxButton.YesNo))
            {
                return;
            }

            this.kalkulation.raeume.Clear();
            this.lbRaeume.Items.Clear();
            this.WaendeGrid.Items.Clear();
            this.BoedenGrid.Items.Clear();
        }
    }
}
