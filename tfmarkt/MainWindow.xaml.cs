﻿using System;
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
using tfmarkt.Verwaltung;

namespace tfmarkt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Produktkatalog meinKatalog { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Produkte TEST
            this.meinKatalog = new Produktkatalog(new XmlDatei());
            //Tapetenrolle t1 = new Tapetenrolle("001", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            Tapetenrolle t12 = new Tapetenrolle("002", "Raufaser", "Halt Raufaser", 10.3m, 1200, 80, 60);
            //Fliesenpaket t2 = new Fliesenpaket("003", "Steinfliese", "Halt Steinfliese", 18m, 80, 80, 10);

            // Zusatzprodukte TEST
            //Fugenmoertel tp = new Fugenmoertel("004", "Mörtel", "Harter Mörtel", 18m, 0.2, 20, 40, false);
            //Fliesenkleber fk = new Fliesenkleber("005", "Kleber", "Super Kleber", 18m, 0.2, 20, 40, false);
            //Tapetenkleister fm = new Tapetenkleister("006", "Kleister", "Harter Mörtel", 18m, 0.2, 20, false);

            // Produktkatalog laden TEST
            meinKatalog.datenhandler.fuelleProduktkatalog(meinKatalog);

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
            lb1.Text += string.Format("{0}\n", meinKatalog);
            lb1.Text += string.Format("Hinzufügen neuer Tapete erfolgreich? {0}\n", meinKatalog.addTapete(t12));

            lb1.AppendText(string.Format("{0}\n", "@" + xml.xmlVerzeichnis + "\\" + xml.dateiTapeten));
            //meinKatalog.datenhandler.sichereProduktkatalog(meinKatalog);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            result = new VerwaltungLogin().ShowDialog().Value;

            lb1.AppendText(String.Format("Login erfolgreich? {0}\n", result));
            lb1.ScrollToEnd();

            if (result)
                new Verwaltung.Verwaltung(this.meinKatalog).ShowDialog();
        }
    }
}
