using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PaginaIniziale
{
    public partial class Impiccato : Window
    {
        private string parolaSegreta;
        private char[] statoParola;
        private int errori;
        private List<string> parole;
        private static readonly Random rnd = new Random();

        public Impiccato()
        {
            InitializeComponent();
            InizializzaGioco();
        }

        private void InizializzaGioco()
        {
            // Carica parole
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parole.txt");
            parole = System.IO.File.ReadAllLines(path).ToList();

            // Reset
            errori = 0;
            parolaSegreta = parole[rnd.Next(parole.Count)];
            statoParola = parolaSegreta.Select(_ => '_').ToArray();
            txtParola.Text = string.Join(" ", statoParola);
            txtSbagli.Text = "Sbagliate: ";

            // Immagine iniziale
            AggiornaImmagine();

            // Bottoni lettere
            GrigliaLettere.Children.Clear();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button b = new Button
                {
                    Content = c.ToString(),
                    Margin = new Thickness(5),
                    FontSize = 18
                };
                b.Click += Lettera_Click;
                GrigliaLettere.Children.Add(b);
            }
        }

        private void Lettera_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            char lettera = btn.Content.ToString()[0];
            btn.IsEnabled = false;

            if (parolaSegreta.Contains(lettera))
            {
                // Aggiorna parola
                for (int i = 0; i < parolaSegreta.Length; i++)
                    if (parolaSegreta[i] == lettera)
                        statoParola[i] = lettera;

                txtParola.Text = string.Join(" ", statoParola);

                if (!statoParola.Contains('_'))
                {
                    MessageBox.Show("Hai vinto!");
                    DisabilitaLettere();
                }
            }
            else
            {
                errori++;
                txtSbagli.Text += lettera + " ";
                AggiornaImmagine();

                if (errori == 6)
                {
                    MessageBox.Show($"Hai perso! La parola era: {parolaSegreta}");
                    DisabilitaLettere();
                }
            }
        }

        private void AggiornaImmagine()
        {
            string img = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Immagini_impiccato", $"stato{errori}.png");
            imgImpiccato.Source = new BitmapImage(new Uri(img));
        }

        private void DisabilitaLettere()
        {
            foreach (Button b in GrigliaLettere.Children)
                b.IsEnabled = false;
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            InizializzaGioco();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow finestraIniziale = new MainWindow();
            finestraIniziale.Show();
            this.Close();
        }
    }
}
