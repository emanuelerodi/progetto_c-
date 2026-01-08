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

namespace PaginaIniziale
{
    public partial class IndovinaNumero : Window
    {
        private int numeroSegreto;
        private Random rnd = new Random();
        private int tentativi = 0;

        // Frasi dinamiche
        private string[] troppoAlto = new string[]
        {
            "il numero è troppo alto!" + "\n" + "ritenta e sarai più fortunato"
        };

        private string[] troppoBasso = new string[]
        {
            "il numero è troppo basso!" + "\n" + "ritenta e sarai più fortunato"
        };

        private string[] vittoria = new string[]
        {
            "Hai indovinato!",
        };

        public IndovinaNumero()
        {
            InitializeComponent();
            IniziaNuovaPartita();
        }

        private void IniziaNuovaPartita()
        {
            numeroSegreto = rnd.Next(1, 101); // Numero tra 1 e 100
            tentativi = 0;
            TxtMessaggi.Text = "Ho pensato a un numero tra 1 e 100. Prova a indovinare!";
            TxtMessaggi.Foreground = Brushes.White;
            InputNumero.Clear();
        }

        private void BtnConferma_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(InputNumero.Text, out int numeroUtente))
            {
                TxtMessaggi.Text = "Inserisci un numero valido!";
                TxtMessaggi.Foreground = Brushes.OrangeRed;
                return;
            }

            tentativi++;

            if (numeroUtente == numeroSegreto)
            {
                TxtMessaggi.Text = vittoria[rnd.Next(vittoria.Length)] +
                                   $"\nHai indovinato in {tentativi} tentativi!";
                TxtMessaggi.Foreground = Brushes.Black;
            }
            else if (numeroUtente > numeroSegreto)
            {
                TxtMessaggi.Text = troppoAlto[rnd.Next(troppoAlto.Length)];
                TxtMessaggi.Foreground = Brushes.Black;
            }
            else
            {
                TxtMessaggi.Text = troppoBasso[rnd.Next(troppoBasso.Length)];
                TxtMessaggi.Foreground = Brushes.Black;
            }

            InputNumero.Clear();
        }

        private void BtnRicomincia_Click(object sender, RoutedEventArgs e)
        {
            IniziaNuovaPartita();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }
    }
}
