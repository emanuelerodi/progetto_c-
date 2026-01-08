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
        private int errori = 0;
        private List<char> lettereUsate = new List<char>();
        private List<string> listaParole;

        public Impiccato()
        {
            InitializeComponent();
            InizializzaGioco();
        }

        // Carica parole dal file parole.txt
        private List<string> CaricaParoleDaFile()
        {
            try
            {
                string path = "parole.txt";
                return System.IO.File.ReadAllLines(path)
                                     .Select(p => p.Trim().ToUpper())
                                     .Where(p => p.Length > 0)
                                     .ToList();
            }
            catch
            {
                MessageBox.Show("Errore: impossibile leggere parole.txt");
                return new List<string> { "ERRORE" };
            }
        }

        private void InizializzaGioco()
        {
            // Carica parole
            listaParole = CaricaParoleDaFile();

            // Reset
            errori = 0;
            lettereUsate.Clear();
            imgImpiccato.Source = new BitmapImage(new Uri("Immagini_impiccato/stato0.png", UriKind.Relative));

            // Scegli parola casuale
            Random rnd = new Random();
            parolaSegreta = listaParole[rnd.Next(listaParole.Count)];
            statoParola = parolaSegreta.Select(c => '_').ToArray();

            txtParola.Text = string.Join(" ", statoParola);
            txtSbagli.Text = "Sbagliate: ";

            // Genera bottoni A-Z
            GrigliaLettere.Children.Clear();

            for (char c = 'A'; c <= 'Z'; c++)
            {
                Button btn = new Button
                {
                    Content = c.ToString(),
                    Margin = new Thickness(5),
                    FontSize = 18
                };

                btn.Click += Lettera_Click;
                GrigliaLettere.Children.Add(btn);
            }
        }

        private void Lettera_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            char lettera = btn.Content.ToString()[0];

            btn.IsEnabled = false;

            if (lettereUsate.Contains(lettera))
                return;

            lettereUsate.Add(lettera);

            // Se la lettera è nella parola
            if (parolaSegreta.Contains(lettera))
            {
                for (int i = 0; i < parolaSegreta.Length; i++)
                {
                    if (parolaSegreta[i] == lettera)
                        statoParola[i] = lettera;
                }

                txtParola.Text = string.Join(" ", statoParola);

                // Vittoria
                if (!statoParola.Contains('_'))
                {
                    MessageBox.Show("Hai vinto!");
                    DisabilitaTutteLettere();
                }
            }
            else
            {
                // Errore
                errori++;

                txtSbagli.Text = $"Sbagliate: {string.Join(" ", lettereUsate.Where(l => !parolaSegreta.Contains(l)))}";

                imgImpiccato.Source = new BitmapImage(new Uri($"Immagini_impiccato/stato{errori}.png", UriKind.Relative));

                // Sconfitta
                if (errori == 6)
                {
                    MessageBox.Show($"Hai perso! La parola era: {parolaSegreta}");
                    DisabilitaTutteLettere();
                }
            }
        }

        private void DisabilitaTutteLettere()
        {
            foreach (Button b in GrigliaLettere.Children)
                b.IsEnabled = false;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            InizializzaGioco();
        }
    }
}
