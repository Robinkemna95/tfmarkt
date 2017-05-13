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

namespace tfmarkt.Verwaltung
{
    /// <summary>
    /// Interaktionslogik für TfMarktVerwaltung.xaml
    /// </summary>
    public partial class VerwaltungLogin : Window
    {
        // Das Passwort für die Verwaltung ist fest verdrahtet im Programmcode hinterlegt
        private const string PASSWORT = "2017";
            
        // Konstruktor um vor betreten der Verwaltung die Zugriffsberechtigung zu prüfen
        public VerwaltungLogin()
        {
            InitializeComponent();
            pbPasswort.Focus();
        }

        // Hier wird geprüft ob der Zugriff erlaubt ist oder nicht
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            //MessageBox.Show(this, String.Format("Eingegebenes Passwort: {0}\n Entspricht dem Verwaltungspasswort: {1}", pbPasswort.Password, PASSWORT.Equals(pbPasswort.Password)));
            DialogResult = PASSWORT.Equals(pbPasswort.Password.Trim());
        }
    }
}
