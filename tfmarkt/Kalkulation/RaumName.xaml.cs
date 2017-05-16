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

namespace tfmarkt.Kalkulation
{
    /// <summary>
    /// Interaktionslogik für RaumName.xaml
    /// </summary>
    public partial class RaumName : Window
    {
        public KalkulationWindow parentWindow { get; set; }
        public RaumName(KalkulationWindow parentWindow)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {         
            this.Close();
        }

        private void ContinueCreation(object sender, RoutedEventArgs e)
        {
            Raum neuerRaum = null;
            if (RaumnameInput.Text != "Raumname")
            {
                neuerRaum = new Raum(RaumnameInput.Text);
                //lbRaeume.Items.Add
            }
            else
            {
                neuerRaum = new Raum("Raum_" +(((ListBox)this.parentWindow.FindName("lbRaeume")).Items.Count+1));
            }

            if (neuerRaum != null)
            {
                ((ListBox)this.parentWindow.FindName("lbRaeume")).Items.Add(neuerRaum);
                this.parentWindow.kalkulation.addRaum(neuerRaum);
            }
            
            this.Close();
        }
    }
}
