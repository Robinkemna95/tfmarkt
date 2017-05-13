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

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für ZusatzproduktAuswahl.xaml
    /// </summary>
    public partial class ZusatzproduktAuswahl : Window
    {
        private Verwaltung verwaltung;

        public ZusatzproduktAuswahl(Verwaltung verwaltung)
        {
            InitializeComponent();

            this.verwaltung = verwaltung;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Content.Equals(typeof(Tapetenkleister).Name))
            {
                this.verwaltung.type = typeof(Tapetenkleister);
            }
            else if (rb.Content.Equals(typeof(Fliesenkleber).Name))
            {
                this.verwaltung.type = typeof(Fliesenkleber);
            }
            else if (rb.Content.Equals(typeof(Fugenmoertel).Name))
            {
                this.verwaltung.type = typeof(Fugenmoertel);
            }

            this.Close();
        }
    }
}
