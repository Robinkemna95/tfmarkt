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
using System.Windows.Navigation;
using System.Windows.Shapes;
using tfmarkt.Produktklassen;
using tfmarkt.Verwaltung;
using tfmarkt.Daten;
using tfmarkt.Katalog;
using tfmarkt.Kalkulation;
using System.IO;

namespace tfmarkt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string verwaltungImage = Convert.ToString(System.IO.Path.GetFullPath("images/Verwaltung.png"));

        private string kalkulationImage = Convert.ToString(System.IO.Path.GetFullPath("images/Kalkulieren.png"));

        private string uebersichtImage = Convert.ToString(System.IO.Path.GetFullPath("images/Uebersicht.png"));

        public Produktkatalog meinKatalog { get; set; }

        public Boolean loginPruefen { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            lblVerwaltung.Background = new ImageBrush(new BitmapImage(new Uri(this.verwaltungImage)));
            lblUebersicht.Background = new ImageBrush(new BitmapImage(new Uri(this.uebersichtImage)));
            lblKalkulation.Background = new ImageBrush(new BitmapImage(new Uri(this.kalkulationImage)));

            // Produkte TEST
            this.meinKatalog = new Produktkatalog(new XmlDatei());
            //Tapetenrolle t1 = new Tapetenrolle("001", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            Tapetenrolle t12 = new Tapetenrolle("002", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            //Fliesenpaket t2 = new Fliesenpaket("003", "Steinfliese", "Halt Steinfliese", 18m, 80, 80, 10);

            // Zusatzprodukte TEST
            //Fugenmoertel tp = new Fugenmoertel("004", "Mörtel", "Harter Mörtel", 18m, 0.2, 20, 40, false);
            //Fliesenkleber fk = new Fliesenkleber("005", "Kleber", "Super Kleber", 18m, 0.2, 20, 40, false);
            //Tapetenkleister fm = new Tapetenkleister("006", "Kleister", "Harter Mörtel", 18m, 200, 20, false);

            // Produktkatalog laden TEST
            //meinKatalog.datenhandler.fuelleProduktkatalog(meinKatalog);

            //Tapetenrolle t3 = (Tapetenrolle)t1.Clone();

            //meinKatalog.addFliese(t2);

            //meinKatalog.addTapete(t1);
            //meinKatalog.addTapete(t12);

            //meinKatalog.addZusatzprodukt(tp);
            //meinKatalog.addZusatzprodukt(fk);
            //meinKatalog.addZusatzprodukt(fm);

            //Object t4 = t2.Clone();

            //System.Type t = t4.GetType();

            //Boolean type = tp.GetType() == t;
            XmlDatei xml = new XmlDatei();

            //lb1.Text = string.Format("Preis der/des {0} {1:C}\n", tp.produktName(), tp.preis);
            lb1.AppendText(string.Format("{0}\n", meinKatalog));
            lb1.AppendText(string.Format("Hinzufügen neuer Tapete erfolgreich? {0}\n", meinKatalog.addTapete(t12)));

            lb1.AppendText(string.Format("{0}\n", "@" + xml.xmlVerzeichnis + "\\" + xml.dateiTapeten));
            //meinKatalog.datenhandler.sichereProduktkatalog(meinKatalog);
        }

        private void Label_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            VerwaltungLogin login;
            Verwaltung.Verwaltung verwaltung;
            Nullable<bool> returnValue;
            bool result;
            
            login = new VerwaltungLogin();
            login.Owner = this;

            returnValue = login.ShowDialog();
            this.loginPruefen = login.loginPruefen;

            lb1.AppendText("returnValue: " + returnValue + "\n");

            if (!this.loginPruefen)
            {
                return;
            }

            result = returnValue.Value;

            lb1.AppendText(String.Format("Login erfolgreich? {0}\n", result));
            lb1.ScrollToEnd();

            if (result)
            {
                verwaltung = new Verwaltung.Verwaltung(this.meinKatalog);
                verwaltung.Owner = this;

                verwaltung.ShowDialog();
            }
            else
            {
                MessageBox.Show(this, "Das eingegebene Passwort für die Verwaltung war falsch!!!", "Falsches Passwort");
            }
        }

        private void Label_Click_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            Produktuebersicht uebersicht;

            lb1.AppendText("Anzeigen der Produktübersicht");
            lb1.ScrollToEnd();

            uebersicht = new Produktuebersicht(this.meinKatalog);
            uebersicht.Owner = this;

            uebersicht.ShowDialog();            
        }

        private void Label_Click_2(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            KalkulationWindow window = new KalkulationWindow(this.meinKatalog);
            window.Owner = this;
            window.ShowDialog();
        }

        private void lbl_MouseEnter_Opacity(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Background.Opacity = 0.5;
        }

        private void lbl_MouseLeave_Opacity(object sender, MouseEventArgs e)
        {
            Label tmp = (Label)sender;

            tmp.Background.Opacity = 1;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key == Key.D && Keyboard.IsKeyDown(Key.LeftCtrl)))
            {
                return;
            }

            if (lb1.Visibility != Visibility.Visible)
            {
                lb1.Visibility = Visibility.Visible;
                Grid.SetRowSpan(gridTFMarkt, 1);
            }
            else 
            {
                lb1.Visibility = Visibility.Hidden;
                Grid.SetRowSpan(gridTFMarkt, 2);
            }
            
        }
    }
}
