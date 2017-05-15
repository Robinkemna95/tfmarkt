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
    /// Interaktionslogik für AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem(string type)
        {
            Raum selectedRaum = (Raum)((ListBox)this.Owner.FindName("lbRaeume")).SelectedItem;
            switch (type)
            {
                case "AddBoden":
                    break;
                case "AddWand":
                    break;
            }
        }

        private void initBodenForm()
        {

        }

        private void initWandForm()
        {

        }
    }
}
