using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.IO;

namespace PaginaIniziale
{
    public partial class Talpa : Window
    {
        private Button[] bottoni;
        private DispatcherTimer timerGioco;
        private DispatcherTimer timerTalpa;

        private int tempo = 30;
        private int punti = 0;
        private Random rnd = new Random();

        private BitmapImage imgTalpa;
        private BitmapImage imgVuoto;

        private bool giocoInPausa = false;

        // ⭐ RECORD
        private string pathBestScore = "File/best_score_talpa.txt";
        private int bestScore = 0;

        public Talpa()
        {
            InitializeComponent();
            CaricaImmagini();
            InizializzaGriglia();
            InizializzaTimer();
            CaricaRecord();
        }

        private void CaricaImmagini()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            imgTalpa = new BitmapImage(new Uri(System.IO.Path.Combine(basePath, "Immagini_Talpa", "talpa.png")));
            imgTalpa.Freeze();

            imgVuoto = new BitmapImage(new Uri(System.IO.Path.Combine(basePath, "Immagini_Talpa", "vuoto.png")));
            imgVuoto.Freeze();
        }

        private void InizializzaGriglia()
        {
            bottoni = new Button[9];
            GrigliaTalpe.Children.Clear();

            for (int i = 0; i < 9; i++)
            {
                Image img = new Image
                {
                    Source = imgVuoto,
                    Stretch = System.Windows.Media.Stretch.Fill
                };

                Button b = new Button
                {
                    Margin = new Thickness(0),
                    Content = img,
                    Tag = "vuoto"
                };

                b.Click += ColpisciTalpa;

                bottoni[i] = b;
                GrigliaTalpe.Children.Add(b);
            }
        }

        private void InizializzaTimer()
        {
            timerGioco = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timerGioco.Tick += TimerGioco_Tick;

            timerTalpa = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(700)
            };
            timerTalpa.Tick += TimerTalpa_Tick;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            tempo = 30;
            punti = 0;

            txtTempo.Text = tempo + "s";
            txtPunti.Text = punti.ToString();

            foreach (Button b in bottoni)
            {
                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }

            timerGioco.Start();
            timerTalpa.Start();
        }

        private void TimerGioco_Tick(object sender, EventArgs e)
        {
            tempo--;
            txtTempo.Text = tempo + "s";

            if (tempo <= 0)
            {
                timerGioco.Stop();
                timerTalpa.Stop();

                foreach (Button b in bottoni)
                {
                    ((Image)b.Content).Source = imgVuoto;
                    b.Tag = "vuoto";
                }

                // ⭐ CONTROLLO RECORD
                if (punti > bestScore)
                {

                    bestScore = punti;

                    File.WriteAllText(pathBestScore, bestScore.ToString());
                    txtRecord.Text = bestScore.ToString();
                    MessageBox.Show($"Tempo scaduto! Hai fatto {punti} punti.\nNuovo record!");
                }
                else
                {
                    MessageBox.Show($"Tempo scaduto! Hai fatto {punti} punti.\nRecord attuale: {bestScore}");
                }
            }
        }

        private void TimerTalpa_Tick(object sender, EventArgs e)
        {
            foreach (Button b in bottoni)
            {
                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }

            int index = rnd.Next(0, bottoni.Length);

            ((Image)bottoni[index].Content).Source = imgTalpa;
            bottoni[index].Tag = "talpa";
        }

        private void ColpisciTalpa(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            if (b.Tag.ToString() == "talpa")
            {
                punti++;
                txtPunti.Text = punti.ToString();

                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            timerGioco.Stop();
            timerTalpa.Stop();

            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void Ricomincia_Click(object sender, RoutedEventArgs e)
        {
            timerGioco.Stop();
            timerTalpa.Stop();

            tempo = 30;
            punti = 0;

            txtTempo.Text = tempo + "s";
            txtPunti.Text = punti.ToString();

            foreach (Button b in bottoni)
            {
                ((Image)b.Content).Source = imgVuoto;
                b.Tag = "vuoto";
            }

            
        }

        // ⭐ CARICA RECORD
        private void CaricaRecord()
        {
            if (File.Exists(pathBestScore))
            {
                string contenuto = File.ReadAllText(pathBestScore);
                if (int.TryParse(contenuto, out int score))
                {
                    bestScore = score;
                }
            }

            txtRecord.Text = bestScore.ToString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!giocoInPausa)
            {
                
                giocoInPausa = true;
                timerGioco.Stop();
                timerTalpa.Stop();

                MessageBox.Show(
                    "Gioco in pausa.\nPremi OK per continuare.",
                    "Pausa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                
                giocoInPausa = false;
                timerGioco.Start();
                timerTalpa.Start();
            }
        }

    }
}
