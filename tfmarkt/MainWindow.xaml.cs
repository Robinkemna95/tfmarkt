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
using tfmarkt.Katalog;
using tfmarkt.Daten;

namespace tfmarkt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Produkte TEST
            Produktkatalog k = new Produktkatalog(new XmlDatei());
            Tapetenrolle t1 = new Tapetenrolle("T002", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            Tapetenrolle t12 = new Tapetenrolle("T003", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            Fliesenpaket t2 = new Fliesenpaket("T001", "Steinfliese", "Halt Steinfliese", 18m, 80, 80, 10);

            // Zusatzprodukte TEST
            Fugenmoertel tp = new Fugenmoertel("T004", "Mörtel", "Harter Mörtel", 18m, 0.2, 20, 40, false);
            Fliesenkleber fk = new Fliesenkleber("T005", "Kleber", "Super Kleber", 18m, 0.2, 20, 40, false);
            Tapetenkleister fm = new Tapetenkleister("T006", "Kleister", "Harter Mörtel", 18m, 0.2, 20, false);

            // Produktkatalog laden TEST
            k.datenhandler.fuelleProduktkatalog(k);

            Testklasse te = new Testklasse();
            // Tapetenrolle t2 = (Tapetenrolle)t1.Clone();

            //k.addFliese(t2);
            
            //k.addTapete(t1);
            //k.addTapete(t12);

            //k.addZusatzprodukt(tp);
            //k.addZusatzprodukt(fk);
            //k.addZusatzprodukt(fm);

            Object t3 = t2.Clone();

            System.Type t = t3.GetType();

            Boolean type = tp.GetType() == t;
            XmlDatei xml = new XmlDatei();

            lb1.Content = string.Format("Preis der/des {0} {1:C}\n", tp.produktName(), tp.preis);
            lb1.Content += string.Format("{0}\n", k);
            lb1.Content += string.Format("Hinzufügen neuer Tapete erfolgreich? {0}\n", k.addTapete(t12));

            lb1.Content += string.Format("{0}", "@" + xml.xmlVerzeichnis + "\\" + xml.dateiTapeten);

            //k.datenhandler.sichereProduktkatalog(k);
          
        }
    }
}
