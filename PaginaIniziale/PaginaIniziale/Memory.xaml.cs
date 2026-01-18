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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;

namespace PaginaIniziale
{
    public partial class Memory : Window
    {
        private DispatcherTimer timer;
        private int secondi = 0;
        private List<string> immagini;
        private Button primaCarta = null;
        private Button secondaCarta = null;
        private int coppieTrovate = 0;
        private bool bloccoClick = false;

        private string pathBestTime = "best_time.txt";
        private int bestTime = int.MaxValue;

        public Memory()
        {
            InitializeComponent();
            CaricaBestTime();
            InizializzaTimer();
            InizializzaGioco();
        }

        private void InizializzaGioco()
        {
            CardGrid.Children.Clear();
            coppieTrovate = 0;
            secondi = 0;

            immagini = new List<string>{
                "alieno.png","astronauta.png","luna.png","navicella.png",
                "razzo.png","satellite.png","sole.png","terra.png"
            };

            immagini = immagini.Concat(immagini).ToList();

            Random rnd = new Random();
            immagini = immagini.OrderBy(x => rnd.Next()).ToList();

            foreach (var img in immagini)
            {
                Button btn = new Button();
                btn.Tag = img;
                btn.Click += Carta_Click;
                btn.Content = CreaImmagine("carta_coperta.png");
                CardGrid.Children.Add(btn);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            secondi = 0;
            lblTimer.Content = "Tempo: 0 s";
            timer.Stop();
            InizializzaGioco();
        }

        private void InizializzaTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondi++;
            lblTimer.Content = $"Tempo: {secondi} s";
        }

        private Image CreaImmagine(string nome)
        {
            return new Image
            {
                Source = new BitmapImage(new Uri($"Immagini_memory/{nome}", UriKind.Relative)),
                Stretch = Stretch.Uniform
            };
        }

        private async void Carta_Click(object sender, RoutedEventArgs e)
        {
            if (bloccoClick) return;

            Button carta = sender as Button;

            if (carta.Content is Image img && img.Source.ToString().Contains(carta.Tag.ToString()))
                return;

            carta.Content = CreaImmagine(carta.Tag.ToString());

            if (primaCarta == null)
            {
                primaCarta = carta;
                return;
            }

            secondaCarta = carta;
            bloccoClick = true;

            if (primaCarta.Tag.ToString() == secondaCarta.Tag.ToString())
            {
                coppieTrovate++;
                primaCarta = null;
                secondaCarta = null;
                bloccoClick = false;

                if (coppieTrovate == 8)
                {
                    timer.Stop();

                    if (secondi < bestTime)
                    {
                        bestTime = secondi;
                        File.WriteAllText(pathBestTime, bestTime.ToString());
                        lblBest.Content = $"Record: {bestTime} s";
                        MessageBox.Show($"Nuovo record! {secondi} secondi!");
                    }
                    else
                    {
                        MessageBox.Show($"Hai vinto in {secondi} secondi!\nRecord attuale: {bestTime} secondi");
                    }
                }
            }
            else
            {
                await Task.Delay(800);

                primaCarta.Content = CreaImmagine("carta_coperta.png");
                secondaCarta.Content = CreaImmagine("carta_coperta.png");

                primaCarta = null;
                secondaCarta = null;
                bloccoClick = false;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            secondi = 0;
            lblTimer.Content = "Tempo: 0 s";
            timer.Stop();
            timer.Start();
            InizializzaGioco();
        }

        private void CaricaBestTime()
        {
            if (File.Exists(pathBestTime))
            {
                string contenuto = File.ReadAllText(pathBestTime);
                if (int.TryParse(contenuto, out int tempo))
                {
                    bestTime = tempo;
                }
            }

            lblBest.Content = bestTime == int.MaxValue
                ? "Record: -"
                : $"Record: {bestTime} s";
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            // Metti in pausa
            timer.Stop();
            bloccoClick = true;

            MessageBoxResult result = MessageBox.Show(
                "Gioco in pausa.\nVuoi continuare?",
                "Pausa",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            // Riprendi il gioco
            bloccoClick = false;
            timer.Start();
        }


    }
}
