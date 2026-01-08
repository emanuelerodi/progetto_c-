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
        private readonly List<char> lettereUsate = new List<char>();
        private List<string> listaParole;
        private static readonly Random rnd = new Random();

        public Impiccato()
        {
            InitializeComponent();

            // MOSTRA IL PERCORSO REALE DOVE WPF CERCA L'IMMAGINE
            MessageBox.Show(System.IO.Path.GetFullPath("Immagini_impiccato/stato0.png"));

            InizializzaGioco();
        }

        private List<string> CaricaParoleDaFile()
        {
            string path = "parole.txt";
            return System.IO.File.ReadAllLines(path)
                                 .Select(p => p.Trim().ToUpper())
                                 .Where(p => p.Length > 0)
                                 .ToList();
        }

        private void InizializzaGioco()
        {
            listaParole = CaricaParoleDaFile();

            errori = 0;
            lettereUsate.Clear();

            imgImpiccato.Source = new BitmapImage(new Uri("Immagini_impiccato/stato0.png", UriKind.Relative));

            parolaSegreta = listaParole[rnd.Next(listaParole.Count)];
            statoParola = parolaSegreta.Select(_ => '_').ToArray();

            txtParola.Text = string.Join(" ", statoParola);
            txtSbagli.Text = "Sbagliate: ";

            GeneraBottoniLettere();
        }

        private void GeneraBottoniLettere()
        {
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

            if (parolaSegreta.Contains(lettera))
            {
                for (int i = 0; i < parolaSegreta.Length; i++)
                {
                    if (parolaSegreta[i] == lettera)
                        statoParola[i] = lettera;
                }

                txtParola.Text = string.Join(" ", statoParola);

                if (!statoParola.Contains('_'))
                {
                    MessageBox.Show("Hai vinto!");
                    DisabilitaTutteLettere();
                }
            }
            else
            {
                errori++;

                txtSbagli.Text = $"Sbagliate: {string.Join(" ", lettereUsate.Where(l => !parolaSegreta.Contains(l)))}";

                imgImpiccato.Source = new BitmapImage(new Uri($"Immagini_impiccato/stato{errori}.png", UriKind.Relative));

                if (errori >= 6)
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
            Close();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            InizializzaGioco();
        }
    }
}
