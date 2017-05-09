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

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Verwaltung.xaml
    /// </summary>
    public partial class Verwaltung : Window
    {
        public Produktkatalog meinKatalog;

        public Verwaltung(Produktkatalog katalog)
        {
           InitializeComponent();

           this.meinKatalog = katalog;
        }

        private void btZusatzprodukte_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = this.meinKatalog.zusatzprodukte;
            this.Title = "Verwaltung: Zusatzprodukte";
        }

        private void btFliesen_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = this.meinKatalog.fliesen;
            this.Title = "Verwaltung: Fliesen";
        }

        private void btTapeten_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.ItemsSource = this.meinKatalog.tapeten;
            this.Title = "Verwaltung: Tapeten";
        }
    }
}
